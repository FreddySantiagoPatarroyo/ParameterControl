using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Policy;
using ParameterControl.Models.Rows;
using ParameterControl.Services.Policies;
using ParameterControl.Services.Rows;
using System.Collections.Generic;
using modPolicy = ParameterControl.Models.Policy;


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
        public async Task<ActionResult> Policies()
        {
            List<modPolicy.Policy> policies = await policiesServices.GetPolicies();

            TablePolicies.Data = policiesServices.GetPolicesFormatTable(policies);

            TablePolicies.Rows = rows.RowsPolicies();

            TablePolicies.Filter = true;
            TablePolicies.IsCreate = true;
            TablePolicies.IsActivate = true;
            TablePolicies.IsEdit = true;
            TablePolicies.IsInactivate = true;

            ViewBag.ApplyFilter = false;

            return View("Policies", TablePolicies);
        }

        public async Task<ActionResult> PoliciesFilter(string filterColunm = "", string filterValue = "")
        {

            _logger.LogInformation(filterColunm);
            _logger.LogInformation(filterValue);

            if (filterColunm == null || filterColunm == "" || filterValue == null || filterValue == "")
            {
                return RedirectToAction("Policies");
            }

            FilterViewModel filter = new FilterViewModel()
            {
                ColumValue = filterColunm,
                ValueFilter = filterValue
            };

            List<PolicyViewModel> policiesFilter = await policiesServices.GetFilterPolicies(filter);
            TablePolicies.Data = policiesFilter;

            TablePolicies.Rows = rows.RowsPolicies();

            TablePolicies.Filter = true;
            TablePolicies.IsCreate = true;
            TablePolicies.IsActivate = true;
            TablePolicies.IsEdit = true;
            TablePolicies.IsInactivate = true;

            ViewBag.ApplyFilter = true;

            return View("PoliciesFilter", TablePolicies);
        }

        [HttpGet]
        public async Task<ActionResult> Desactive(string id)
        {
            modPolicy.Policy policy = await policiesServices.GetPolicyById(id);

            return View("Actions/DesactivePolicy", policy);
        }

        [HttpPost]
        public async Task<ActionResult> DesactivePolicy(string id)
        {
            return RedirectToAction("Policies");
        }

        [HttpGet]
        public async Task<ActionResult> Active(string id)
        {
            modPolicy.Policy policy = await policiesServices.GetPolicyById(id);

            return View("Actions/ActivePolicy", policy);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {

            List<string> OperationTypeOptionsList = await policiesServices.GetOperationsType();

            PolicyCreateViewModel model = new PolicyCreateViewModel()
            {
                OperationTypeOptions = OperationTypeOptionsList
            };

            return View("Actions/CreatePolicy", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] modPolicy.Policy request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Error en la informacion enviada", state = "Error" });
            }
            else
            {
                try
                {
                    _logger.LogInformation($"Inicia método PoliciesController.Create {JsonConvert.SerializeObject(request)}");
                    var responseIn = await policiesServices.InsertPolicy(request);
                    _logger.LogInformation($"Finaliza método PoliciesController.Create {responseIn}");
                    return Ok(new { message = "Se creo la politica de manera exitosa", state = "Success" });
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error en el método PoliciesController.Create : {JsonConvert.SerializeObject(ex.Message)}");
                    return BadRequest(new { message = "Error al crear politica", state = "Error" });
                }
            }
            
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            modPolicy.Policy policy = await policiesServices.GetPolicyById(id);

            return View("Actions/EditPolicy", policy);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(modPolicy.Policy policy)
        {
            Console.WriteLine(policy.Description);
            return RedirectToAction("Policies");
        }

        [HttpGet]
        public async Task<ActionResult> View(string id)
        {
            modPolicy.Policy policy = await policiesServices.GetPolicyById(id);

            return View("Actions/ViewPolicy", policy);
        }

        [HttpGet]
        public ActionResult Filter()
        {
            FilterViewModel model = new FilterViewModel()
            {
                Rows = rows.RowsPolicies()

            };

            return View("Actions/Filter", model);
        }

        [HttpPost]
        public async Task<ActionResult> FilterPolicies(FilterViewModel filter)
        {

            Console.WriteLine(filter.ColumValue);

            Console.WriteLine(filter.ValueFilter);

            return RedirectToAction("PoliciesFilter", new
            {
                filterColunm = filter.ColumValue,
                filterValue = filter.ValueFilter
            });
        }
    }
}
