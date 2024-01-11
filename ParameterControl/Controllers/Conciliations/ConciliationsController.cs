using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Conciliation;
using ParameterControl.Models.Rows;
using ParameterControl.Services.Conciliations;
using ParameterControl.Services.Rows;
using ParameterControl.Models.Policy;
using ParameterControl.Services.Policies;

namespace ParameterControl.Controllers.Conciliations
{
    public class ConciliationsController : Controller
    {
        public TableConciliationViewModel TableConciliations = new TableConciliationViewModel();
        private readonly ILogger<HomeController> _logger;
        private readonly IConciliationsServices conciliationsServices;
        private readonly Rows rows;

        public ConciliationsController(
            ILogger<HomeController> logger,
            IConciliationsServices conciliationsServices,
            Rows rows
        )
        {
            this._logger = logger;
            this.conciliationsServices = conciliationsServices;
            this.rows = rows;
        }

        [HttpGet]
        public async Task<ActionResult> Conciliations()
        {
            
            TableConciliations.Data = await conciliationsServices.GetConciliations();

            TableConciliations.Rows = rows.RowsConciliations();

            TableConciliations.IsCreate = true;
            TableConciliations.IsActivate = true;
            TableConciliations.IsEdit = true;
            TableConciliations.IsInactivate = true;

            ViewBag.ApplyFilter = false;

            return View("Conciliations", TableConciliations);
        }


        [HttpGet]
        public async Task<ActionResult> ConciliationsFilter(string filterColunm = "", string filterValue = "")
        {
            _logger.LogInformation(filterColunm);
            _logger.LogInformation(filterValue);

            if (filterColunm == null || filterColunm == "" || filterValue == null || filterValue == "")
            {
                return RedirectToAction("Conciliations");
            }

            FilterViewModel filter = new FilterViewModel()
            {
                ColumValue = filterColunm,
                ValueFilter = filterValue
            };

            List<Conciliation> conciliationsFilter = await conciliationsServices.GetFilterConciliations(filter);

            TableConciliations.Data = conciliationsFilter;

            TableConciliations.Rows = rows.RowsConciliations();

            TableConciliations.IsCreate = true;
            TableConciliations.IsActivate = true;
            TableConciliations.IsEdit = true;
            TableConciliations.IsInactivate = true;

            ViewBag.ApplyFilter = true;

            return View("ConciliationsFilter", TableConciliations);
        }

        [HttpGet]
        public async Task<ActionResult> Desactive(string id)
        {
            Conciliation conciliation = await conciliationsServices.GetConciliationsById(id);

            return View("Actions/DesactiveConciliation", conciliation);
        }

      

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            Conciliation conciliation = await conciliationsServices.GetConciliationsById(id);

            List<string> PoliciesOptionsList = await conciliationsServices.GetPolicies();
            List<string> RequiredOptionsList = await conciliationsServices.GetRequired();


            ConciliationCreateViewModel model = new ConciliationCreateViewModel()
            {
                Id = conciliation.Id,
                Code = conciliation.Code,
                Name = conciliation.Name,
                Description = conciliation.Description,
                Conciliation_ = conciliation.Conciliation_,
                Package = conciliation.Package,
                Email = conciliation.Email,
                Destination = conciliation.Destination,
                Policies = conciliation.Policies,
                Required = conciliation.Required,
                Result = conciliation.Result,
                State = conciliation.State
            };

            model.PoliciesOption = PoliciesOptionsList;
            model.RequiredOption = RequiredOptionsList;

            return View("Actions/EditConciliation", model);
        }

        [HttpGet]
        public async Task<ActionResult> View(string id)
        {
            Conciliation conciliation = await conciliationsServices.GetConciliationsById(id);

            return View("Actions/ViewConciliation", conciliation);
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {

            List<string> PoliciesOptionsList = await conciliationsServices.GetPolicies();
            List<string> RequiredOptionList = await conciliationsServices.GetRequired();

            ConciliationCreateViewModel model = new ConciliationCreateViewModel()
            {
                PoliciesOption = PoliciesOptionsList,
                RequiredOption = RequiredOptionList
            };

            return View("Actions/CreateConciliation", model);
        }

        [HttpGet]
        public ActionResult Filter()
        {
            FilterViewModel model = new FilterViewModel()
            {
                Rows = rows.RowsConciliations()

            };

            return View("Actions/Filter", model);
        }

        [HttpPost]
        public async Task<ActionResult> FilterConciliations(FilterViewModel filter)
        {

            Console.WriteLine(filter.ColumValue);

            Console.WriteLine(filter.ValueFilter);

            return RedirectToAction("ConciliationsFilter", new
            {
                filterColunm = filter.ColumValue,
                filterValue = filter.ValueFilter
            });
        }

    }
}
