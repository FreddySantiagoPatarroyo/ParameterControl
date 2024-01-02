using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Policy;
using ParameterControl.Models.Rows;
using ParameterControl.Services.Policies;
using ParameterControl.Services.Rows;
using System.Collections.Generic;

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

            TablePolicies.Data = await policiesServices.GetPolicies();
            

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

            if(filterColunm == null || filterColunm == "" || filterValue == null || filterValue =="")
            {
                return RedirectToAction("Policies");
            }

            FilterViewModel filter = new FilterViewModel()
            {
                ColumValue = filterColunm,
                ValueFilter = filterValue
            };

            List<Policy> policiesFilter = await policiesServices.GetFilterPolicies(filter);
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
            Policy policy = await policiesServices.GetPolicyById(id);

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
            Policy policy = await policiesServices.GetPolicyById(id);

            return View("Actions/ActivePolicy", policy);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            Policy policy = await policiesServices.GetPolicyById(id);

            return View("Actions/EditPolicy", policy);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Policy policy)
        {
            Console.WriteLine(policy.Description);
            return RedirectToAction("Policies");
        }

        [HttpGet]
        public async Task<ActionResult> View(string id)
        {
            Policy policy = await policiesServices.GetPolicyById(id);

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
                filterValue= filter.ValueFilter
            });
        }
    }
}
