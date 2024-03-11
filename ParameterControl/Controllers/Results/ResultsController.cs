using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Models.Policy;
using ParameterControl.Models.Result;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.Policies;
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

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpGet]
        public async Task<ActionResult> Results(PaginationViewModel paginationViewModel)
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            try
            {
                List<modResult.Result> results = await resultsServices.GetResultsPagination(paginationViewModel);
                int TotalResults = await resultsServices.CountResults();

                TableResults.Data = await resultsServices.GetResultsFormat(results);
                TableResults.Rows = rows.RowsResults();
                TableResults.IsCreate = _isCreate;
                TableResults.IsActivate = _isActivate;
                TableResults.IsEdit = _isEdit;
                TableResults.IsView = _isView;
                TableResults.IsInactivate = _isInactive;
                TableResults.Filter = true;
                ViewBag.ApplyFilter = false;

                var resultViemModel = new PaginationResult<TableResultViewModel>()
                {
                    Elements = TableResults,
                    Page = paginationViewModel.Page,
                    RecordsPage = paginationViewModel.RecordsPage,
                    TotalRecords = TotalResults,
                    BaseUrl = Url.Action() + "?"
                };

                ViewBag.Success = true;
                return View("Results", resultViemModel);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("Results", null);
            }

        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpGet]
        public async Task<ActionResult> ResultsFilter(PaginationViewModel paginationViewModel, string filterColunm = "", string filterValue = "", string typeRow = "")
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
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
                int TotalResults = resultsFilter.Count();

                TableResults.Data = resultsServices.GetFilterPagination(resultsFilter, paginationViewModel, TotalResults);
                TableResults.Rows = rows.RowsResults();
                TableResults.IsCreate = _isCreate;
                TableResults.IsActivate = _isActivate;
                TableResults.IsEdit = _isEdit;
                TableResults.IsView = _isView;
                TableResults.IsInactivate = _isInactive;
                TableResults.Filter = true;
                ViewBag.ApplyFilter = true;

                var resultViemModel = new PaginationResult<TableResultViewModel>()
                {
                    Elements = TableResults,
                    Page = paginationViewModel.Page,
                    RecordsPage = paginationViewModel.RecordsPage,
                    TotalRecords = TotalResults,
                    BaseUrl = Url.Action() + "?filterColunm=" + filterColunm + "&filterValue=" + filterValue + "&typeRow=" + typeRow + "&"
                };

                ViewBag.Success = true;
                return View("ResultsFilter", resultViemModel);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("ResultsFilter", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet]
        public async Task<ActionResult> Edit()
        {
            Result result = new Result();
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            return View("Actions/EditResult", result);
        }

        //[Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        //[HttpGet]
        //public async Task<ActionResult> View(string id)
        //{
        //    Result result = await resultsServices.GetResultsById(id);
        //    ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
        //    return View("Actions/ViewResult", result);
        //}

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpGet]
        public ActionResult Filter()
        {
            FilterViewModel model = new FilterViewModel()
            {
                Rows = rows.RowsResults()

            };
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            return View("Actions/Filter", model);
        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
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

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
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
