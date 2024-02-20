using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParameterControl.Conciliation.DataAccess;
using ParameterControl.Models.Conciliation;
using ParameterControl.Models.ConciliationExecution;
using ParameterControl.Services.ConciliationExecution;
using System.Reflection;


namespace ParameterControl.Controllers.ConciliationExecution
{
    public class ConciliationExecutionController : Controller
    {
        private readonly IConciliationExecutionService conciliationExecutionService;

        public ConciliationExecutionController(IConciliationExecutionService conciliationExecutionService) 
        {
            this.conciliationExecutionService = conciliationExecutionService;
        }

        public async Task<ActionResult> ConciliationExecution()
        {
            try
            {
                ConciliationExecutionViewModel model = new ConciliationExecutionViewModel();
                model.Conciliations = await GetAllConciliation();
                ViewBag.Success = true;

                return View("ConciliationExecution", model);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("ConciliationExecution", null);
            }
            
        }

        public async Task<ActionResult> RunProcess(int code)
        {
            try
            {
                ViewBag.CodeSend = code;
                var conciliation = await conciliationExecutionService.GetConciliationByCode(code);
                if(conciliation.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/RunProcess", null);
                }
                var model = new ConciliationExecutionViewModel();
                model.conciliation = conciliation;


                ViewBag.Success = true;
                ViewBag.EntyNull = false;
                return View("Actions/RunProcess", model);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/RunProcess", null);
            }
        }

        public async Task<ActionResult> ProgramExecution(int code)
        {
            try
            {
                ViewBag.CodeSend = code;
                var conciliation = await conciliationExecutionService.GetConciliationByCode(code);
                if (conciliation.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/ProgramExecution", null);
                }
                var model = new ConciliationExecutionViewModel();
                model.conciliation = conciliation;


                ViewBag.Success = true;
                ViewBag.EntyNull = false;
                return View("Actions/ProgramExecution", model);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/ProgramExecution", null);
            }
        }

        public async Task<ActionResult> SuccesfulTransaction(int code)
        {
            try
            {
                ViewBag.CodeSend = code;
                var conciliation = await conciliationExecutionService.GetConciliationByCode(code);
                if (conciliation.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/SuccesfulTransaction", null);
                }
                var model = new ConciliationExecutionViewModel();
                model.conciliation = conciliation;


                ViewBag.Success = true;
                ViewBag.EntyNull = false;
                return View("Actions/SuccesfulTransaction", model);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/SuccesfulTransaction", null);
            }
        }

        private async Task<List<SelectListItem>> GetAllConciliation()
        {
            var conciliations = await conciliationExecutionService.GetConciliationsActives();
            return conciliations.Select(conciliation => new SelectListItem(conciliation.Name, conciliation.Code.ToString())).ToList();
        }
    }
}


