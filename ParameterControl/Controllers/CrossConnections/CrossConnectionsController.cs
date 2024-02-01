using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParameterControl.Models.CrossConnection;
using ParameterControl.Models.Filter;
using ParameterControl.Services.Authenticated;
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
        public async Task<ActionResult> CrossConnections()
        {
            List<modCrossConnection.CrossConnection> crossConnections = await crossConnectionsService.GetCrossConnections();

            TableCrossConnections.Data = await crossConnectionsService.GetCrossConnectionsFormat(crossConnections);

            TableCrossConnections.Rows = rows.RowsCrossConnection();

            TableCrossConnections.Filter = true;
            TableCrossConnections.IsCreate = false;
            TableCrossConnections.IsActivate = false;
            TableCrossConnections.IsEdit = false;
            TableCrossConnections.IsView = false;
            TableCrossConnections.IsInactivate = false;

            ViewBag.ApplyFilter = false;

            return View("CrossConnections", TableCrossConnections);
        }

        public async Task<ActionResult> CrossConnectionsFilter(string filterColunm = "", string filterValue = "", string typeRow = "")
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
            TableCrossConnections.Data = crossConnectionsFilter;

            TableCrossConnections.Rows = rows.RowsCrossConnection();

            TableCrossConnections.Filter = true;
            TableCrossConnections.IsCreate = false;
            TableCrossConnections.IsActivate = false;
            TableCrossConnections.IsEdit = false;
            TableCrossConnections.IsView = false;
            TableCrossConnections.IsInactivate = false;

            ViewBag.ApplyFilter = true;

            return View("CrossConnectionsFilter", TableCrossConnections);
        }

        //[HttpGet]
        //public async Task<ActionResult> View(int code)
        //{
        //    modCrossConnection.CrossConnection crossConnection = await crossConnectionsService.GetCrossConnectionByCode(code);

        //    CrossConnectionViewModel model = await crossConnectionsService.GetCrossConnectionFormat(crossConnection);

        //    return View("Actions/ViewCrossConnection", model);
        //}

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
