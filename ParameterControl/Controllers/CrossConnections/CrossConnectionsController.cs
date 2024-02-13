using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ParameterControl.Conciliation.Impl;
using ParameterControl.Models.Conciliation;
using ParameterControl.Models.CrossConnection;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.Conciliations;
using ParameterControl.Services.CrossConnections;
using ParameterControl.Services.Rows;

using modCrossConnection = ParameterControl.Models.CrossConnection;

namespace ParameterControl.Controllers.CrossConnections
{
    public class CrossConnectionsController : Controller
    {
        public TableCrossConnectionViewModel TableCrossConnections = new TableCrossConnectionViewModel();
        private readonly Rows rows;
        private readonly ICrossConnectionsService crossConnectionsService;
        private readonly ILogger<HomeController> _logger;
        private readonly AuthenticatedUser authenticatedUser;

        public CrossConnectionsController(
            ILogger<HomeController> logger,
            Rows rows,
            ICrossConnectionsService crossConnectionsService,
            AuthenticatedUser authenticatedUser
        ) {
            this._logger = logger;
            this.rows = rows;
            this.crossConnectionsService = crossConnectionsService;
            this.authenticatedUser = authenticatedUser;
        }

        [HttpGet]
        public async Task<ActionResult> CrossConnections(PaginationViewModel paginationViewModel)
        {
            List<modCrossConnection.CrossConnection> crossConnections = await crossConnectionsService.GetCrossConnectionsPagination(paginationViewModel);
            int TotalCrossConnections = await crossConnectionsService.CountCrossConnections();

            TableCrossConnections.Data = await crossConnectionsService.GetCrossConnectionsFormat(crossConnections);

            TableCrossConnections.Rows = rows.RowsCrossConnection();

            TableCrossConnections.Filter = true;
            TableCrossConnections.IsCreate = false;
            TableCrossConnections.IsActivate = true;
            TableCrossConnections.IsEdit = false;
            TableCrossConnections.IsView = true;
            TableCrossConnections.IsInactivate = true;

            var resultViemModel = new PaginationResult<TableCrossConnectionViewModel>()
            {
                Elements = TableCrossConnections,
                Page = paginationViewModel.Page,
                RecordsPage = paginationViewModel.RecordsPage,
                TotalRecords = TotalCrossConnections,
                BaseUrl = Url.Action() + "?"
            };

            ViewBag.ApplyFilter = false;

            return View("CrossConnections", resultViemModel);
        }

        public async Task<ActionResult> CrossConnectionsFilter(PaginationViewModel paginationViewModel, string filterColunm = "", string filterValue = "", string typeRow = "")
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
            TableCrossConnections.IsCreate = false;
            TableCrossConnections.IsActivate = true;
            TableCrossConnections.IsEdit = false;
            TableCrossConnections.IsView = true;
            TableCrossConnections.IsInactivate = true;

            var resultViemModel = new PaginationResult<TableCrossConnectionViewModel>()
            {
                Elements = TableCrossConnections,
                Page = paginationViewModel.Page,
                RecordsPage = paginationViewModel.RecordsPage,
                TotalRecords = TotalCrossConnections,
                BaseUrl = Url.Action() + "?filterColunm=" + filterColunm + "&filterValue=" + filterValue + "&typeRow=" + typeRow + "&"
            };

            ViewBag.ApplyFilter = true;

            return View("CrossConnectionsFilter", resultViemModel);
        }

        [HttpGet]
        public async Task<ActionResult> View(int code)
        {
            modCrossConnection.CrossConnection crossConnection = await crossConnectionsService.GetCrossConnectionByCode(code);
            CrossConnectionViewModel model = await crossConnectionsService.GetCrossConnectionFormat(crossConnection);

            return View("Actions/ViewCrossConnection", model);
        }

        [HttpGet]
        public async Task<ActionResult> Active(int code)
        {
            modCrossConnection.CrossConnection crossConnection = await crossConnectionsService.GetCrossConnectionByCode(code);

            return View("Actions/ActiveCrossConnection", crossConnection);
        }

        [HttpPost]
        public async Task<ActionResult> ActiveCrossConnection([FromBody] int code)
        {
            try
            {
                modCrossConnection.CrossConnection request = await crossConnectionsService.GetCrossConnectionByCode(code);
                var responseIn = await crossConnectionsService.ActiveCrossConnection(request);
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
            modCrossConnection.CrossConnection crossConnection = await crossConnectionsService.GetCrossConnectionByCode(code);

            return View("Actions/DesactiveCrossConnection", crossConnection);
        }

        [HttpPost]
        public async Task<ActionResult> DesactiveCrossConnection([FromBody] int code)
        {
            try
            {
                var request = await crossConnectionsService.GetCrossConnectionByCode(code);
                _logger.LogInformation($"Inicia método CrossConnectionsController.Desactive {JsonConvert.SerializeObject(request)}");
                var responseIn = await crossConnectionsService.DesactiveCrossConnection(request);
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
