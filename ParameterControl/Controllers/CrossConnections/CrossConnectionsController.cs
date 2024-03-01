using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ParameterControl.Models.CrossConnection;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.CrossConnections;
using ParameterControl.Services.Rows;
using System.Security.Claims;
using modCrossConnection = ParameterControl.Models.CrossConnection;

namespace ParameterControl.Controllers.CrossConnections
{
    [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
    public class CrossConnectionsController : Controller
    {
        public TableCrossConnectionViewModel TableCrossConnections = new TableCrossConnectionViewModel();
        private readonly Rows rows;
        private readonly ICrossConnectionsService crossConnectionsService;
        private readonly ILogger<HomeController> _logger;
        private readonly AuthenticatedUser authenticatedUser;
        private readonly IConfiguration _configuration;
        private readonly ClaimsPrincipal _principal;
        private readonly bool _isCreate;
        private readonly bool _isActivate;
        private readonly bool _isEdit;
        private readonly bool _isView;
        private readonly bool _isInactive;

        public CrossConnectionsController(
            ILogger<HomeController> logger,
            Rows rows,
            ICrossConnectionsService crossConnectionsService,
            AuthenticatedUser authenticatedUser,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccesor
        )
        {
            this._logger = logger;
            this.rows = rows;
            this.crossConnectionsService = crossConnectionsService;
            this.authenticatedUser = authenticatedUser;
            _configuration = configuration;
            var context = httpContextAccesor.HttpContext;
            _principal = context.User as ClaimsPrincipal;
            var data = _principal.FindFirst(ClaimTypes.Role).Value;
            var section = _configuration.GetSection($"Permisos:{data}:Conciliacion").GetChildren();
            _isCreate = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnCreate")).FirstOrDefault().Value);
            _isActivate = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnActivate")).FirstOrDefault().Value);
            _isEdit = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnEdit")).FirstOrDefault().Value);
            _isView = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnDetail")).FirstOrDefault().Value);
            _isInactive = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnInactive")).FirstOrDefault().Value);
        }

        [HttpGet]
        public async Task<ActionResult> CrossConnections(PaginationViewModel paginationViewModel)
        {
            try
            {
                List<modCrossConnection.CrossConnection> crossConnections = await crossConnectionsService.GetCrossConnectionsPagination(paginationViewModel);
                int TotalCrossConnections = await crossConnectionsService.CountCrossConnections();

                TableCrossConnections.Data = await crossConnectionsService.GetCrossConnectionsFormat(crossConnections);
                TableCrossConnections.Rows = rows.RowsCrossConnection();
                TableCrossConnections.Filter = true;
                TableCrossConnections.IsCreate = _isCreate;
                TableCrossConnections.IsActivate = _isActivate;
                TableCrossConnections.IsEdit = _isEdit;
                TableCrossConnections.IsView = _isView;
                TableCrossConnections.IsInactivate = _isInactive;
                ViewBag.ApplyFilter = false;

                var resultViemModel = new PaginationResult<TableCrossConnectionViewModel>()
                {
                    Elements = TableCrossConnections,
                    Page = paginationViewModel.Page,
                    RecordsPage = paginationViewModel.RecordsPage,
                    TotalRecords = TotalCrossConnections,
                    BaseUrl = Url.Action() + "?"
                };

                ViewBag.Success = true;
                return View("CrossConnections", resultViemModel);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("CrossConnections", null);
            }
        }

        public async Task<ActionResult> CrossConnectionsFilter(PaginationViewModel paginationViewModel, string filterColunm = "", string filterValue = "", string typeRow = "")
        {
            try
            {
                if (filterColunm == null || filterColunm == "" || filterValue == null || filterValue == "")
                {
                    return RedirectToAction("CrossConnections");
                }

                FilterViewModel filter = new FilterViewModel()
                {
                    ColumValue = filterColunm,
                    ValueFilter = filterValue,
                    TypeRow = typeRow
                };

                List<CrossConnectionViewModel> crossConnectionsFilter = await crossConnectionsService.GetFilterCrossConnections(filter);
                int TotalCrossConnections = crossConnectionsFilter.Count();

                TableCrossConnections.Data = crossConnectionsService.GetFilterPagination(crossConnectionsFilter, paginationViewModel, TotalCrossConnections);
                TableCrossConnections.Rows = rows.RowsCrossConnection();
                TableCrossConnections.Filter = true;
                TableCrossConnections.IsCreate = _isCreate;
                TableCrossConnections.IsActivate = _isActivate;
                TableCrossConnections.IsEdit = _isEdit;
                TableCrossConnections.IsView = _isView;
                TableCrossConnections.IsInactivate = _isInactive;
                ViewBag.ApplyFilter = true;

                var resultViemModel = new PaginationResult<TableCrossConnectionViewModel>()
                {
                    Elements = TableCrossConnections,
                    Page = paginationViewModel.Page,
                    RecordsPage = paginationViewModel.RecordsPage,
                    TotalRecords = TotalCrossConnections,
                    BaseUrl = Url.Action() + "?filterColunm=" + filterColunm + "&filterValue=" + filterValue + "&typeRow=" + typeRow + "&"
                };

                ViewBag.Success = true;
                return View("CrossConnectionsFilter", resultViemModel);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("CrossConnectionsFilter", null);
            }

        }

        [HttpGet]
        public async Task<ActionResult> View(int code)
        {
            try
            {
                ViewBag.CodeSend = code;
                modCrossConnection.CrossConnection crossConnection = await crossConnectionsService.GetCrossConnectionByCode(code);
                if (crossConnection.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/ViewCrossConnection", null);
                }

                CrossConnectionViewModel model = await crossConnectionsService.GetCrossConnectionFormat(crossConnection);

                ViewBag.Success = true;
                ViewBag.EntyNull = false;
                return View("Actions/ViewCrossConnection", model);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/ViewCrossConnection", null);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Active(int code)
        {
            try
            {
                ViewBag.CodeSend = code;
                modCrossConnection.CrossConnection crossConnection = await crossConnectionsService.GetCrossConnectionByCode(code);
                if (crossConnection.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/ActiveCrossConnection", null);
                }

                ViewBag.Success = true;
                ViewBag.EntyNull = false;
                return View("Actions/ActiveCrossConnection", crossConnection);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/ActiveCrossConnection", null);
            }
        }

        [HttpPost]
        public async Task<ActionResult> ActiveCrossConnection([FromBody] int code)
        {
            try
            {
                modCrossConnection.CrossConnection request = await crossConnectionsService.GetCrossConnectionByCode(code);
                var responseIn = await crossConnectionsService.ActiveCrossConnection(request);
                _logger.LogInformation($"Finaliza método CrossConnectionsController.Active {responseIn}");
                return Ok(new { message = "Se activo la toma transversal de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método CrossConnectionsController.Active : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al activar la toma transversal", state = "Error" });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Desactive(int code)
        {
            try
            {
                ViewBag.CodeSend = code;
                modCrossConnection.CrossConnection crossConnection = await crossConnectionsService.GetCrossConnectionByCode(code);
                if (crossConnection.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/DesactiveCrossConnection", null);
                }

                ViewBag.Success = true;
                ViewBag.EntyNull = false;
                return View("Actions/DesactiveCrossConnection", crossConnection);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/DesactiveCrossConnection", null);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DesactiveCrossConnection([FromBody] int code)
        {
            try
            {
                var request = await crossConnectionsService.GetCrossConnectionByCode(code);
                var responseIn = await crossConnectionsService.DesactiveCrossConnection(request);
                _logger.LogInformation($"Finaliza método CrossConnectionsController.Desactive {responseIn}");
                return Ok(new { message = "Se desactivo la toma transversal de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método CrossConnectionsController.Desactive : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al desactivar la toma transversal", state = "Error" });
            }
        }

        [HttpGet]
        public ActionResult Filter()
        {
            FilterViewModel model = new FilterViewModel()
            {
                Rows = rows.RowsCrossConnection()

            };

            return View("Actions/Filter", model);
        }

        [HttpPost]
        public async Task<ActionResult> FilterCrossConnections(FilterViewModel filter)
        {
            if (filter.TypeRow == "Select")
            {
                filter.ValueFilter = filter.ValueFilterOptions;
            }
            else if (filter.TypeRow == "Date")
            {
                filter.ValueFilter = filter.ValueFilterDate.ToString("dd/MM/yyyy");
            }

            return RedirectToAction("CrossConnectionsFilter", new
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
                default:
                    filter.TypeRow = "General";
                    break;
            }

            return Ok(filter);
        }
    }
}
