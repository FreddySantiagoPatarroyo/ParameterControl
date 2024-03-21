using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParameterControl.Models.ConciliationExecution;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.ConciliationExecution;
using System.Security.Claims;
using OdiService;
using Microsoft.AspNetCore.Server.Kestrel;
using ParameterControl.Services.Policies;
using System.Configuration;
using ParameterControl.Models.Policy;
using System.ServiceModel.Channels;
using ParameterControl.Services.Audit;
using modAudit = ParameterControl.Models.Audit;


namespace ParameterControl.Controllers.ConciliationExecution
{
    [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
    public class ConciliationExecutionController : Controller
    {
        private readonly IConciliationExecutionService conciliationExecutionService;
        private readonly IPoliciesServices policiesServices;
        private readonly IConfiguration _configuration;
        private readonly ClaimsPrincipal _principal;
        private readonly AuthenticatedUser authenticatedUser;
        private readonly IAuditsService auditsService;
        private readonly bool _isExecute;
        private readonly bool _isProgram;
        private readonly bool _isAbort;
        private IEnumerable<IConfigurationSection> CredentialsODI;

        public ConciliationExecutionController(
            IConciliationExecutionService conciliationExecutionService,
            IPoliciesServices policiesServices,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccesor,
            AuthenticatedUser authenticatedUser,
            IAuditsService auditsService)
        {
            this.conciliationExecutionService = conciliationExecutionService;
            this.policiesServices = policiesServices;
            _configuration = configuration;
            var context = httpContextAccesor.HttpContext;
            _principal = context.User as ClaimsPrincipal;
            this.authenticatedUser = authenticatedUser;
            this.auditsService = auditsService;
            var data = _principal.FindFirst(ClaimTypes.Role).Value;
            CredentialsODI = _configuration.GetSection("CredentialsODI").GetChildren();
            var section = _configuration.GetSection($"Permisos:{data}:ExecuteConciliation").GetChildren();
            _isExecute = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnExecute")).FirstOrDefault().Value);
            _isProgram = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnProgram")).FirstOrDefault().Value);
            _isAbort = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnAbort")).FirstOrDefault().Value);

        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        public async Task<ActionResult> ConciliationExecution()
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            try
            {
                ConciliationExecutionViewModel model = new ConciliationExecutionViewModel();
                model.Conciliations = await GetAllConciliation();
                model.IsExecution = _isExecute;
                model.IsProgram = _isProgram;
                model.IsAbort = _isAbort;

                ViewBag.Success = true;
                return View("ConciliationExecution", model);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("ConciliationExecution", null);
            }

        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR")]
        public async Task<ActionResult> RunProcess(int code)
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
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
                model.ConciliationCode = code;

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

        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR")]
        public async Task<ActionResult> RunProcessPost([FromBody] int code)
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            try
            {
                ViewBag.CodeSend = code;
                var conciliation = await conciliationExecutionService.GetConciliationByCode(code);
                if (conciliation.Code == 0)
                {
                    return BadRequest(new { message = "No existe esta conciliacion", state = "Error" });
                }
                
                var policy = await policiesServices.GetPolicyByCode(conciliation.PolicyCode);
                if (policy.Code == 0 || policy.Package == string.Empty)
                {
                    return BadRequest(new { message = "No es posible ejecutar la conciliaicon", state = "Error" });
                }

                var audit = new modAudit.Audit()
                {
                    Action = "Ejecutar Conciliacion",
                    UserCode = authenticatedUser.GetUserCode(),
                    Component = "Ejecucion de Conciliaciones",
                    ModifieldDate = DateTime.Now,
                    BeforeValue = ""
                };

                await auditsService.InsertAudit(audit);

                var response = await ExecuteConciliation(policy.Package);

                return Ok(new { message = "Se ejecuto la conciliacion de manera exitosa", state = "Success" });
        }
            catch (Exception)
            {
                return Ok(new { message = "Se ejecuto la conciliacion de manera exitosa", state = "Success" });
            }
        }

        //[Authorize(Roles = "ADMINISTRADOR,EJECUTOR")]
        //public async Task<ActionResult> ProgramExecution(int code)
        //{
        //    ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
        //    try
        //    {
        //        ViewBag.CodeSend = code;
        //        var conciliation = await conciliationExecutionService.GetConciliationByCode(code);
        //        if (conciliation.Code == 0)
        //        {
        //            ViewBag.Success = true;
        //            ViewBag.EntyNull = true;
        //            return View("Actions/ProgramExecution", null);
        //        }
        //        var model = new ConciliationExecutionViewModel();
        //        model.conciliation = conciliation;


        //        ViewBag.Success = true;
        //        ViewBag.EntyNull = false;
        //        return View("Actions/ProgramExecution", model);
        //    }
        //    catch (Exception)
        //    {
        //        ViewBag.Success = false;
        //        ViewBag.EntyNull = false;
        //        return View("Actions/ProgramExecution", null);
        //    }
        //}

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR")]
        [HttpGet]
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

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet]
        public async Task<ActionResult> AbortConciliation(int code)
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            try
            {
                ViewBag.CodeSend = code;
                var conciliation = await conciliationExecutionService.GetConciliationByCode(code);
                if (conciliation.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/AbortConciliation", null);
                }
                var model = new ConciliationExecutionViewModel();
                model.conciliation = conciliation;


                ViewBag.Success = true;
                ViewBag.EntyNull = false;
                return View("Actions/AbortConciliation", model);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/AbortConciliation", null);
            }
        }

        private async Task<List<SelectListItem>> GetAllConciliation()
        {
            var conciliations = await conciliationExecutionService.GetConciliationsActives();
            return conciliations.Select(conciliation => new SelectListItem(conciliation.Name, conciliation.Code.ToString())).ToList();
        }

        private async Task<invokeStartScenResponse> ExecuteConciliation(string Pakage)
        {
            var debugType = new DebugType()
            {
                BreakOnError = false,
                Debug = false,
                DebugDescendants = false,
                SuspendOnFirstStep = true
            };

            var scenarioRequestType = new ScenarioRequestType()
            {
                ScenarioName = "FW_PQ_0552_GAI_EJECUTAR_PAQUETE",
                ScenarioVersion = "-1",
                Context = "CNTX_DESARROLLO",
                Synchronous = true,
                SessionName = "FW_PQ_0552_GAI_EJECUTAR_PAQUETE",
                LogLevel = 6,
                Variables = new VariableType[]
                {
                    new VariableType
                    {
                        Name = "FRAMEWORK.V_0552_CTL_SESION",
                        Value = "1"
                    },
                    new VariableType
                    {
                        Name = "FRAMEWORK.V_0552_CTL_PAQUETE",
                        Value = Pakage
                    }
                }
            };

            var odiCredentialType = new OdiCredentialType()
            {
                OdiUser = (CredentialsODI.Where(x => x.Key.Equals("OdiUser")).FirstOrDefault().Value).ToString(),
                OdiPassword = (CredentialsODI.Where(x => x.Key.Equals("OdiPassword")).FirstOrDefault().Value).ToString(),
                WorkRepository = (CredentialsODI.Where(x => x.Key.Equals("WorkRepository")).FirstOrDefault().Value).ToString()
            };

            var odiStartScenRequest = new OdiStartScenRequest()
            {
                Credentials = odiCredentialType,
                Debug = debugType,
                Request = scenarioRequestType
            };

            var requesClient = new requestPortTypeClient();

            //System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(odiStartScenRequest.GetType());

            //var respuesta = string.Empty;

            //x.Serialize(Console.Out, odiStartScenRequest);

            //Console.WriteLine();
            //Console.ReadLine();

            var result = await requesClient.invokeStartScenAsync(odiStartScenRequest);

            return result;
        }
    }
}


