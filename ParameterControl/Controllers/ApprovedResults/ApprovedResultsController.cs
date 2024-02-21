using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ParameterControl.Models.Filter;
using ParameterControl.Models.ApprovedResult;
using ParameterControl.Services.ApprovedResults;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.ApprovedResults;
using ParameterControl.Services.Rows;

using modApprovedResult = ParameterControl.Models.ApprovedResult;
using ParameterControl.Models.ApprovedResult;

namespace ParameterControl.Controllers.ApprovedResults
{
    public class ApprovedResultsController : Controller
    {
        public TableApprovedResultViewModel TableApprovedResults = new TableApprovedResultViewModel();
        private readonly ILogger<HomeController> _logger;
        private readonly IApprovedResultsServices approvedResultsServices;
        private readonly Rows rows;
        private readonly AuthenticatedUser authenticatedUser;

        public ApprovedResultsController(
            ILogger<HomeController> logger,
            IApprovedResultsServices approvedResultsServices,
            Rows rows,
            AuthenticatedUser authenticatedUser
        ) 
        {
            this._logger = logger;
            this.approvedResultsServices = approvedResultsServices;
            this.rows = rows;
            this.authenticatedUser = authenticatedUser;
        }

        [HttpGet]
        public async Task<ActionResult> ApprovedResults()
        {
            List<modApprovedResult.ApprovedResult> approvedResults = await approvedResultsServices.GetApprovedResults();

            TableApprovedResults.Data = await approvedResultsServices.GetApprovedResultsFormat(approvedResults);
            TableApprovedResults.Rows = rows.RowsApprovedResults();
            TableApprovedResults.IsCreate = false;
            TableApprovedResults.IsActivate = false;
            TableApprovedResults.IsEdit = false;
            TableApprovedResults.IsView = false;
            TableApprovedResults.IsInactivate = false;
            TableApprovedResults.Filter = true;
            ViewBag.ApplyFilter = false;

            return View("ApprovedResults", TableApprovedResults);
        }


        [HttpGet]
        public async Task<ActionResult> ApprovedResultsFilter(string filterColunm = "", string filterValue = "", string typeRow = "")
        {
            if (filterColunm == null || filterColunm == "" || filterValue == null || filterValue == "")
            {
                return RedirectToAction("ApprovedResults");
            }

            FilterViewModel filter = new FilterViewModel()
            {
                ColumValue = filterColunm,
                ValueFilter = filterValue,
                TypeRow = typeRow
            };

            List<ApprovedResultViewModel> approvedResultsFilter = await approvedResultsServices.GetFilterApprovedResults(filter);

            TableApprovedResults.Data = approvedResultsFilter;
            TableApprovedResults.Rows = rows.RowsApprovedResults();
            TableApprovedResults.IsCreate = false;
            TableApprovedResults.IsActivate = false;
            TableApprovedResults.IsEdit = false;
            TableApprovedResults.IsView = false;
            TableApprovedResults.IsInactivate = false;
            TableApprovedResults.Filter = true;
            ViewBag.ApplyFilter = true;

            return View("ApprovedResultsFilter", TableApprovedResults);
        }

        [HttpGet]
        public async Task<ActionResult> View(string id)
        {
            ApprovedResult approvedResult = await approvedResultsServices.GetApprovedResultsById(id);
            return View("Actions/ViewApprovedResult", approvedResult);
        }

        [HttpGet]
        public ActionResult Filter()
        {
            FilterViewModel model = new FilterViewModel()
            {
                Rows = rows.RowsApprovedResults()

            };

            return View("Actions/Filter", model);
        }

        [HttpPost]
        public async Task<ActionResult> FilterApprovedResults(FilterViewModel filter)
        {
            if (filter.TypeRow == "Select")
            {
                filter.ValueFilter = filter.ValueFilterOptions;
            }
            else if (filter.TypeRow == "Date")
            {
                filter.ValueFilter = filter.ValueFilterDate.ToString("dd/MM/yyyy");
            }

            return RedirectToAction("ApprovedResultsFilter", new
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
