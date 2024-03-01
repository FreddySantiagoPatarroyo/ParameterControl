using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParameterControl.Models.ConciliationExecution;
using ParameterControl.Services.ConciliationExecution;
using System.Security.Claims;

namespace ParameterControl.Controllers.ConciliationExecution
{
    [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
    public class ConciliationExecutionController : Controller
    {
        private readonly IConciliationExecutionService conciliationExecutionService;
        private readonly IConfiguration _configuration;
        private readonly ClaimsPrincipal _principal;
        private readonly bool _isCreate;
        private readonly bool _isActivate;
        private readonly bool _isEdit;
        private readonly bool _isView;
        private readonly bool _isInactive;

        public ConciliationExecutionController(
            IConciliationExecutionService conciliationExecutionService,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccesor)
        {
            this.conciliationExecutionService = conciliationExecutionService;
            _configuration = configuration;
            var context = httpContextAccesor.HttpContext;
            _principal = context.User as ClaimsPrincipal;
            var data = _principal.FindFirst(ClaimTypes.Role).Value;
            var section = _configuration.GetSection($"Permisos:{data}:Conciliacion").GetChildren();
            _isCreate = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnCreate")).FirstOrDefault().Value);
            _isActivate = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnActivate")).FirstOrDefault().Value);
            _isEdit = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnEdit")).FirstOrDefault().Value);
            _isView = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnDetail")).FirstOrDefault().Value);
            _isInactive = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnInactive")).FirstOrDefault().Value);

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
                if (conciliation.Code == 0)
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


