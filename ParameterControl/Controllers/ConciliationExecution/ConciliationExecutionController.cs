using Microsoft.AspNetCore.Mvc;


namespace ParameterControl.Controllers.ConciliationExecution
{
    public class ConciliationExecutionController : Controller
    {
        public IActionResult ConciliationExecution()
        {
            return View("ConciliationExecution");
        }
        public async Task<ActionResult> RunProcess(string id)
        {
            return View("Actions/RunProcess");
        }
        public async Task<ActionResult> ProgramExecution(string id)
        {

            return View("Actions/ProgramExecution");
        }
        public async Task<ActionResult> SuccesfulTransaction(string id)
        {

            return View("Actions/SuccesfulTransaction");
        }

    }
}


