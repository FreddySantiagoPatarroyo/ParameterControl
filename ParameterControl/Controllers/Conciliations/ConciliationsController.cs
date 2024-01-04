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
        public async Task<ActionResult> Conciliations(int id)
        {
            _logger.LogInformation("Hola");

            _logger.LogInformation(id.ToString());


            if (id > 0)
            {
                _logger.LogInformation("No Es nulo");
                TableConciliations.Data = new List<Conciliation>();
            }
            else
            {
                TableConciliations.Data = await conciliationsServices.GetConciliations();
            }

            TableConciliations.Rows = rows.RowsConciliations();

            TableConciliations.IsCreate = false;
            TableConciliations.IsActivate = true;
            TableConciliations.IsEdit = true;
            TableConciliations.IsInactivate = true;

            return View("Conciliations", TableConciliations);
        }

        [HttpGet]
        public async Task<ActionResult> Desactive(string id)
        {
            Conciliation conciliation = await conciliationsServices.GetConciliationsById(id);

            return View("Actions/DesactiveConciliation", conciliation);
        }

        [HttpGet]
        public async Task<ActionResult> Create(string id)
        {
            Conciliation conciliation = await conciliationsServices.GetConciliationsById(id);

            return View("Actions/CreateConciliation", conciliation);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            Conciliation conciliation = await conciliationsServices.GetConciliationsById(id);

            return View("Actions/EditConciliation", conciliation);
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

            ConciliationCreateViewModel model = new ConciliationCreateViewModel()
            {
                PoliciesOption = PoliciesOptionsList
            };

            return View("Actions/EditConciliation", model);
        }
    }
}
