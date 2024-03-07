using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Result;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.Results;
using ParameterControl.Services.Rows;
using System.Security.Claims;
using modResult = ParameterControl.Models.Result;

namespace ParameterControl.Controllers.Results
{
    [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
    public class ResultsController : Controller
    {
        public TableResultViewModel TableResults = new TableResultViewModel();
        private readonly ILogger<HomeController> _logger;
        private readonly IResultsServices resultsServices;
        private readonly Rows rows;
        private readonly AuthenticatedUser authenticatedUser;
        private readonly IConfiguration _configuration;
        private readonly ClaimsPrincipal _principal;
        private readonly bool _isCreate;
        private readonly bool _isActivate;
        private readonly bool _isEdit;
        private readonly bool _isView;
        private readonly bool _isInactive;

        public ResultsController(
            ILogger<HomeController> logger,
            IResultsServices resultsServices,
            Rows rows,
            AuthenticatedUser authenticatedUser,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccesor
        )
        {
            this._logger = logger;
            this.resultsServices = resultsServices;
            this.rows = rows;
            this.authenticatedUser = authenticatedUser;
            _configuration = configuration;
            var context = httpContextAccesor.HttpContext;
            _principal = context.User as ClaimsPrincipal;
            var data = _principal.FindFirst(ClaimTypes.Role).Value;
            var section = _configuration.GetSection($"Permisos:{data}:Results").GetChildren();
            _isCreate = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnCreate")).FirstOrDefault().Value);
            _isActivate = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnActivate")).FirstOrDefault().Value);
            _isEdit = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnEdit")).FirstOrDefault().Value);
            _isView = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnDetail")).FirstOrDefault().Value);
            _isInactive = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnInactive")).FirstOrDefault().Value);
        }

        [HttpGet]
        public async Task<ActionResult> Results()
        {
            try
            {
                List<modResult.Result> results = await resultsServices.GetResults();

                TableResults.Data = await resultsServices.GetResultsFormat(results);
                TableResults.Rows = rows.RowsResults();
                TableResults.IsCreate = _isCreate;
                TableResults.IsActivate = _isActivate;
                TableResults.IsEdit = _isEdit;
                TableResults.IsView = _isView;
                TableResults.IsInactivate = _isInactive;
                TableResults.Filter = true;
                ViewBag.ApplyFilter = false;

                ViewBag.Success = true;
                return View("Results", TableResults);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("Results", null);
            }

        }


        [HttpGet]
        public async Task<ActionResult> ResultsFilter(string filterColunm = "", string filterValue = "", string typeRow = "")
        {
            try
            {
                if (filterColunm == null || filterColunm == "" || filterValue == null || filterValue == "")
                {
                    return RedirectToAction("Results");
                }

                FilterViewModel filter = new FilterViewModel()
                {
                    ColumValue = filterColunm,
                    ValueFilter = filterValue,
                    TypeRow = typeRow
                };

                List<ResultViewModel> resultsFilter = await resultsServices.GetFilterResults(filter);

                TableResults.Data = resultsFilter;
                TableResults.Rows = rows.RowsResults();
                TableResults.IsCreate = _isCreate;
                TableResults.IsActivate = _isActivate;
                TableResults.IsEdit = _isEdit;
                TableResults.IsView = _isView;
                TableResults.IsInactivate = _isInactive;
                TableResults.Filter = true;
                ViewBag.ApplyFilter = true;

                ViewBag.Success = true;
                return View("ResultsFilter", TableResults);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("ResultsFilter", null);
            }
        }
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            Result result = await resultsServices.GetResultsById(id);
            return View("Actions/EditResult", result);
        }

        [HttpGet]
        public async Task<ActionResult> View(string id)
        {
            Result result = await resultsServices.GetResultsById(id);
            return View("Actions/ViewResult", result);
        }

        [HttpGet]
        public async Task<ActionResult> Active(string id)
        {
            modResult.Result result = await resultsServices.GetResultsById(id);
            return View("Actions/ActiveResult", result);
        }

        [HttpPost]
        public async Task<ActionResult> ActiveResult([FromBody] string id)
        {
            try
            {
                modResult.Result request = await resultsServices.GetResultsById(id);
                _logger.LogInformation($"Inicia método ResultController.Active {JsonConvert.SerializeObject(request)}");
                return Ok(new { message = "Se activo el resultado de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método ResultController.Active : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al activar el resultado", state = "Error" });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Desactive(string id)
        {
            Result result = await resultsServices.GetResultsById(id);
            return View("Actions/DesactiveResult", result);
        }

        [HttpPost]
        public async Task<ActionResult> DesactiveResult([FromBody] string id)
        {
            try
            {
                modResult.Result request = await resultsServices.GetResultsById(id);
                _logger.LogInformation($"Inicia método ResultController.Active {JsonConvert.SerializeObject(request)}");
                return Ok(new { message = "Se desactivo el resultado de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método ResultController.Active : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al desactivar el resultado", state = "Error" });
            }
        }

        [HttpGet]
        public ActionResult Filter()
        {
            FilterViewModel model = new FilterViewModel()
            {
                Rows = rows.RowsResults()

            };

            return View("Actions/Filter", model);
        }

        [HttpPost]
        public async Task<ActionResult> FilterResults(FilterViewModel filter)
        {
            if (filter.TypeRow == "Select")
            {
                filter.ValueFilter = filter.ValueFilterOptions;
            }
            else if (filter.TypeRow == "Date")
            {
                filter.ValueFilter = filter.ValueFilterDate.ToString("dd/MM/yyyy");
            }

            return RedirectToAction("ResultsFilter", new
            {
                filterColunm = filter.ColumValue,
                filterValue = filter.ValueFilter,
                typeRow = filter.TypeRow
            });
        }

        [HttpPost]
        public async Task<IActionResult> GetSecondaryFilter([FromBody] string ColumValue)
        {
            if (string.IsNullOrEmpty(ColumValue))
            {
                return BadRequest("No a seleccionado ninguna opcion");
            }

            FilterViewModel filter = new FilterViewModel()
            {
                ColumValue = ColumValue
            };

            switch (ColumValue)
            {
                case "StateFormat":
                    filter.Options = new List<SelectListItem>().ToList();
                    filter.Options.Add(new SelectListItem("Activo", "Activo"));
                    filter.Options.Add(new SelectListItem("Inactivo", "Inactivo"));
                    filter.TypeRow = "Select";
                    break;
                case "CreationDateFormat":
                    filter.TypeRow = "Date";
                    break;
                case "UpdateDateFormat":
                    filter.TypeRow = "Date";
                    break;
                case "StartDateFormat":
                    filter.TypeRow = "Date";
                    break;
                case "EndDateFormat":
                    filter.TypeRow = "Date";
                    break;
                default:
                    filter.TypeRow = "General";
                    break;
            }
            return Ok(filter);
        }
    }
}
