using Microsoft.AspNetCore.Mvc;
using ParameterControl.Models.Policy;
using ParameterControl.Models.Rows;

namespace ParameterControl.Controllers.Policies
{
    public class PoliciesController : Controller
    {
        public TablePoliciesViewModel TablePolicies = new TablePoliciesViewModel();
        public ActionResult Policies()
        {
            TablePolicies.Data = new List<Policy>(){
                new Policy(){
                    Code = "COD_001",
                    Name = "Politica_1",
                    Description = "Descripcion ejemplo",
                    Conciliation = 123,
                    ControlType = "Emial",
                    OperationType = "OperationType_1asdasdasdasdasdadasdasdasdad"
                },
                new Policy(){
                    Code = "COD_006",
                    Name = "Name",
                    Description = "Description",
                    Conciliation = 123,
                    ControlType = "Voz",
                    OperationType = "OperationType"
                }
            };

            TablePolicies.Rows = new List<Row>() {
                new Row()
                {
                    Value = "Code",
                    Name = "Codigo"
                },
                new Row()
                {
                    Value = "Name",
                    Name = "Nombre"
                },
                new Row()
                {
                    Value = "Description",
                    Name = "Descripcion"
                },
                new Row()
                {
                    Value = "Conciliation",
                    Name = "Conciliacion"
                },
                new Row()
                {
                    Value = "ControlType",
                    Name = "Tipo de control"
                },
                new Row()
                {
                    Value = "OperationType",
                    Name = "Tipo de operacion"
                }
            };

            TablePolicies.Filter = true;
            TablePolicies.IsCreate = true;
            TablePolicies.IsActivate = true;
            TablePolicies.IsEdit = true;
            TablePolicies.IsInactivate = true;

            return View("Policies", TablePolicies);
        }
    }
}
