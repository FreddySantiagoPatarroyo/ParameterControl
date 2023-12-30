using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Policy;
using ParameterControl.Models.Rows;
using ParameterControl.Services.Policies;
using ParameterControl.Services.Rows;

namespace ParameterControl.Controllers.Policies
{
    public class PoliciesController : Controller
    {

        public TablePoliciesViewModel TablePolicies = new TablePoliciesViewModel();
        private readonly ILogger<HomeController> _logger;
        private readonly IPoliciesServices policiesServices;
        private readonly Rows rows;

        public PoliciesController(
            ILogger<HomeController> logger,
            IPoliciesServices policiesServices,
            Rows rows
        )
        {
            this._logger = logger;
            this.policiesServices = policiesServices;
            this.rows = rows;
        }

        [HttpGet]
        public async Task<ActionResult> Policies(int id)
        {
            _logger.LogInformation("Hola");
            
            _logger.LogInformation(id.ToString());
            
            
            if (id > 0)
            {
                _logger.LogInformation("No Es nulo");
                TablePolicies.Data = new List<Policy>();
                //TablePolicies.Data = policies;
            }
            else
            {
                TablePolicies.Data = await policiesServices.GetPolicies();
            }

            TablePolicies.Rows = rows.RowsPolicies();

            TablePolicies.Filter = true;
            TablePolicies.IsCreate = false;
            TablePolicies.IsActivate = true;
            TablePolicies.IsEdit = true;
            TablePolicies.IsInactivate = true;

            return View("Policies", TablePolicies);
        }

        [HttpGet]
        public async Task<ActionResult> Desactive(string id)
        {
            Policy policy = await policiesServices.GetPolicyById(id);

            return View("Actions/DesactivePolicy", policy);
        }

        [HttpGet]
        public async Task<ActionResult> Active(string id)
        {
            Policy policy = await policiesServices.GetPolicyById(id);

            return View("Actions/ActivePolicy", policy);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            Policy policy = await policiesServices.GetPolicyById(id);

            return View("Actions/EditPolicy", policy);
        }

        [HttpGet]
        public async Task<ActionResult> View(string id)
        {
            Policy policy = await policiesServices.GetPolicyById(id);

            return View("Actions/ViewPolicy", policy);
        }

        [HttpGet]
        public async Task<ActionResult> FilterPolicies()
        {
            List<Row> Rows = rows.RowsPolicies();

            return View("FilterPolicies", Rows);
        }

        [HttpPost]
        public async Task<ActionResult> FilterTable([FromBody] FilterViewModel dataFilter)
        {
            List<Policy> policiesFilter = await policiesServices.GetFilterPolicies(dataFilter);

            _logger.LogWarning(policiesFilter.Count().ToString());

            TablePolicies.Data = await policiesServices.GetFilterPolicies(dataFilter);

            TablePolicies.Rows = rows.RowsPolicies();

            TablePolicies.Filter = true;
            TablePolicies.IsCreate = false;
            TablePolicies.IsActivate = true;
            TablePolicies.IsEdit = true;
            TablePolicies.IsInactivate = true;

            return RedirectToAction("Policies", new { id=1 });
        }
    }
}
