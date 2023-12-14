using Microsoft.AspNetCore.Mvc;
using ParameterControl.Models.Policy;
using ParameterControl.Models.Rows;
using ParameterControl.Services.Policies;
using ParameterControl.Services.Rows;

namespace ParameterControl.Controllers.Policies
{
    public class PoliciesController : Controller
    {
        public TablePoliciesViewModel TablePolicies = new TablePoliciesViewModel();
        private readonly IPoliciesServices policiesServices;
        private readonly Rows rows;

        public PoliciesController(
            IPoliciesServices policiesServices,
            Rows rows
        )
        {
            this.policiesServices = policiesServices;
            this.rows = rows;
        }
        public ActionResult Policies()
        {
            Console.WriteLine("asdasd");
            TablePolicies.Data = policiesServices.GetPolicies();

            TablePolicies.Rows = rows.RowsPolicies();

            TablePolicies.Filter = true;
            TablePolicies.IsCreate = false;
            TablePolicies.IsActivate = true;
            TablePolicies.IsEdit = true;
            TablePolicies.IsInactivate = true;

            return View("Policies", TablePolicies);
        }
    }
}
