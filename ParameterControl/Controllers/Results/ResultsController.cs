using Microsoft.AspNetCore.Mvc;
using ParameterControl.Models.Result;
using ParameterControl.Models.Filter;
using ParameterControl.Services.Results;
using ParameterControl.Services.Rows;
using modResult = ParameterControl.Models.Result;
using ParameterControl.Services.Users;
using System.Reflection;


namespace ParameterControl.Controllers.Results
{
    public class ResultsController: Controller
    {
        public TableResultViewModel TableResults = new TableResultViewModel();
        private readonly ILogger<HomeController> _logger;
        private readonly IResultsServices resultsServices;
        private readonly Rows rows;

        public ResultsController(
            ILogger<HomeController> logger,
            IResultsServices resultsServices,
            Rows rows
        )
        {
            this._logger = logger;
            this.resultsServices = resultsServices;
            this.rows = rows;
        }

        [HttpGet]
        public async Task<ActionResult> Results()
        {

            TableResults.Data = await resultsServices.GetResults();

            TableResults.Rows = rows.RowsResults();

            TableResults.IsCreate = false;
            TableResults.IsActivate = true;
            TableResults.IsEdit = false;
            TableResults.IsInactivate = true;

            ViewBag.ApplyFilter = false;

            return View("Results", TableResults);
        }


        [HttpGet]
        public async Task<ActionResult> ResultsFilter(string filterColunm = "", string filterValue = "")
        {
            _logger.LogInformation(filterColunm);
            _logger.LogInformation(filterValue);

            if (filterColunm == null || filterColunm == "" || filterValue == null || filterValue == "")
            {
                return RedirectToAction("Results");
            }

            FilterViewModel filter = new FilterViewModel()
            {
                ColumValue = filterColunm,
                ValueFilter = filterValue
            };

            List<Result> resultsFilter = await resultsServices.GetFilterResults(filter);

            TableResults.Data = resultsFilter;

            TableResults.Rows = rows.RowsResults();

            TableResults.IsCreate = true;
            TableResults.IsActivate = true;
            TableResults.IsEdit = true;
            TableResults.IsInactivate = true;

            ViewBag.ApplyFilter = true;

            return View("ResultsFilter", TableResults);
        }

        [HttpGet]
        public async Task<ActionResult> Desactive(string id)
        {
            Result result = await resultsServices.GetResultsById(id);

            return View("Actions/DesactiveResult", result);
        }

        [HttpGet]
        public async Task<ActionResult> Active(string id)
        {
            modResult.Result result = await resultsServices.GetResultsById(id);

            return View("Actions/ActiveResult", result);
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
        public async Task<ActionResult> Create(string id)
        {

            Result result = await resultsServices.GetResultsById(id);


            return View("Actions/CreateResult", result);
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

            Console.WriteLine(filter.ColumValue);

            Console.WriteLine(filter.ValueFilter);

            return RedirectToAction("ResultsFilter", new
            {
                filterColunm = filter.ColumValue,
                filterValue = filter.ValueFilter
            });
        }
    }
}
