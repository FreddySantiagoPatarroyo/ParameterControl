using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Models.Policy;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.Policies;
using ParameterControl.Services.Rows;
using System.Security.Claims;
using modPolicy = ParameterControl.Models.Policy;
using modAudit = ParameterControl.Models.Audit;
using ParameterControl.Services.Audit;
using ParameterControl.Models.Conciliation;

namespace ParameterControl.Controllers.Policies
{
    [Authorize]
    public class PoliciesController : Controller
    {
        public TablePoliciesViewModel TablePolicies = new TablePoliciesViewModel();
        private readonly ILogger<HomeController> _logger;
        private readonly IPoliciesServices policiesServices;
        private readonly Rows rows;
        private readonly AuthenticatedUser authenticatedUser;
        private readonly IConfiguration _configuration;
        private readonly IAuditsService auditsService;
        private readonly ClaimsPrincipal _principal;
        private readonly bool _isCreate;
        private readonly bool _isActivate;
        private readonly bool _isEdit;
        private readonly bool _isView;
        private readonly bool _isInactive;

        public PoliciesController(
            ILogger<HomeController> logger,
            IPoliciesServices policiesServices,
            Rows rows,
            AuthenticatedUser authenticatedUser,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccesor,
            IAuditsService auditsService
        )
        {
            this._logger = logger;
            this.policiesServices = policiesServices;
            this.rows = rows;
            this.authenticatedUser = authenticatedUser;
            _configuration = configuration;
            this.auditsService = auditsService;
            var context = httpContextAccesor.HttpContext;
            _principal = context.User as ClaimsPrincipal;
            var data = _principal.FindFirst(ClaimTypes.Role).Value;
            var section = _configuration.GetSection($"Permisos:{data}:Policies").GetChildren();
            _isCreate = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnCreate")).FirstOrDefault().Value);
            _isActivate = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnActivate")).FirstOrDefault().Value);
            _isEdit = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnEdit")).FirstOrDefault().Value);
            _isView = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnDetail")).FirstOrDefault().Value);
            _isInactive = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnInactive")).FirstOrDefault().Value);
        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpGet]
        public async Task<ActionResult> Policies(PaginationViewModel paginationViewModel)
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            try
            {
                List<modPolicy.Policy> policies = await policiesServices.GetPoliciesPagination(paginationViewModel);
                int TotalPolicies = await policiesServices.CountPolicies();

                TablePolicies.Data = await policiesServices.GetPolicesFormat(policies);
                TablePolicies.Rows = rows.RowsPolicies();
                TablePolicies.Filter = true;
                TablePolicies.IsCreate = _isCreate;
                TablePolicies.IsActivate = _isActivate;
                TablePolicies.IsEdit = _isEdit;
                TablePolicies.IsView = _isView;
                TablePolicies.IsInactivate = _isInactive;
                ViewBag.ApplyFilter = false;

                var resultViemModel = new PaginationResult<TablePoliciesViewModel>()
                {
                    Elements = TablePolicies,
                    Page = paginationViewModel.Page,
                    RecordsPage = paginationViewModel.RecordsPage,
                    TotalRecords = TotalPolicies,
                    BaseUrl = Url.Action() + "?"
                };

                ViewBag.Success = true;

                return View("Policies", resultViemModel);
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                return View("Policies", null);
            }

        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpGet]
        public async Task<ActionResult> PoliciesFilter(PaginationViewModel paginationViewModel, string filterColunm = "", string filterValue = "", string typeRow = "")
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();

            try
            {
                if (filterColunm == null || filterColunm == "" || filterValue == null || filterValue == "")
                {
                    return RedirectToAction("Policies");
                }

                FilterViewModel filter = new FilterViewModel()
                {
                    ColumValue = filterColunm,
                    ValueFilter = filterValue,
                    TypeRow = typeRow
                };

                List<PolicyViewModel> policiesFilter = await policiesServices.GetFilterPolicies(filter);
                int TotalPolicies = policiesFilter.Count();

                TablePolicies.Data = policiesServices.GetFilterPagination(policiesFilter, paginationViewModel, TotalPolicies);
                TablePolicies.Rows = rows.RowsPolicies();
                TablePolicies.Filter = true;
                TablePolicies.IsCreate = _isCreate;
                TablePolicies.IsActivate = _isActivate;
                TablePolicies.IsEdit = _isEdit;
                TablePolicies.IsView = _isView;
                TablePolicies.IsInactivate = _isInactive;
                ViewBag.ApplyFilter = true;

                var resultViemModel = new PaginationResult<TablePoliciesViewModel>()
                {
                    Elements = TablePolicies,
                    Page = paginationViewModel.Page,
                    RecordsPage = paginationViewModel.RecordsPage,
                    TotalRecords = TotalPolicies,
                    BaseUrl = Url.Action() + "?filterColunm=" + filterColunm + "&filterValue=" + filterValue + "&typeRow=" + typeRow + "&"
                };

                ViewBag.Success = true;

                return View("PoliciesFilter", resultViemModel);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("PoliciesFilter", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            try
            {
                PolicyCreateViewModel model = new PolicyCreateViewModel();

                ViewBag.Success = true;
                return View("Actions/CreatePolicy", model);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("Actions/CreatePolicy", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] modPolicy.Policy request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError($"Error en el modelo : {JsonConvert.SerializeObject(request)}");
                    return BadRequest(new { message = "Error en la informacion enviada", state = "Error" });
                }
                else
                {
                    _logger.LogInformation($"Inicia método PoliciesController.Create {JsonConvert.SerializeObject(request)}");
                    var responseIn = await policiesServices.InsertPolicy(request);
                    var audit = new modAudit.Audit()
                    {
                        Action = "Crear Politica",
                        UserCode = authenticatedUser.GetUserCode(),
                        Component = "Politicas",
                        ModifieldDate = DateTime.Now,
                        BeforeValue = ""
                    };

                    await auditsService.InsertAudit(audit);
                    _logger.LogInformation($"Finaliza método PoliciesController.Create {responseIn}");
                    return Ok(new { message = "Se creo la politica de manera exitosa", state = "Success" });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método PoliciesController.Create : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al crear la politica", state = "Error" });
            }

        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet]
        public async Task<ActionResult> Edit(int code)
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            try
            {
                ViewBag.CodeSend = code;
                modPolicy.Policy policy = await policiesServices.GetPolicyByCode(code);
                if (policy.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    
                    return View("Actions/EditPolicy", null);
                }
                PolicyCreateViewModel model = await policiesServices.GetPolicyFormatCreate(policy);

                ViewBag.Success = true;
                ViewBag.EntyNull = false;


                return View("Actions/EditPolicy", model);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/EditPolicy", null);
            }

        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public async Task<ActionResult> Edit([FromBody] modPolicy.Policy request)
        {
            try
            {
                var policy = await policiesServices.GetPolicyByCode(request.Code);
                if (policy.Code == 0)
                {
                    _logger.LogError($"Error politica no existe : {JsonConvert.SerializeObject(request)}");
                    return BadRequest(new { message = "No existe una politica con el codigo" + policy.Code, state = "Error" });
                }
                else
                {
                    request.CreationDate = policy.CreationDate;
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError($"Error en el modelo : {JsonConvert.SerializeObject(request)}");
                    return BadRequest(new { message = "Error en la informacion enviada", state = "Error" });
                }
                else
                {
                    var responseIn = await policiesServices.UpdatePolicy(request);
                    var audit = new modAudit.Audit()
                    {
                        Action = "Editar Politica",
                        UserCode = authenticatedUser.GetUserCode(),
                        Component = "Politicas",
                        ModifieldDate = DateTime.Now,
                        BeforeValue = JsonConvert.SerializeObject(policy).ToString()
                    };

                    await auditsService.InsertAudit(audit);
                    _logger.LogInformation($"Finaliza método PoliciesController.Edit {responseIn}");
                    return Ok(new { message = "Se actualizo la politica de manera exitosa", state = "Success" });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método PoliciesController.Edit : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al actualizar la politica", state = "Error" });
            }
        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpGet]
        public async Task<ActionResult> View(int code)
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            try
            {
                ViewBag.CodeSend = code;
                modPolicy.Policy policy = await policiesServices.GetPolicyByCode(code);
                if (policy.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/ViewPolicy", null);
                }
                PolicyViewModel model = await policiesServices.GetPolicyFormat(policy);

                ViewBag.Success = true;
                ViewBag.EntyNull = false;

                var audit = new modAudit.Audit()
                {
                    Action = "Ver Politica",
                    UserCode = authenticatedUser.GetUserCode(),
                    Component = "Politicas",
                    ModifieldDate = DateTime.Now,
                    BeforeValue = ""
                };

                await auditsService.InsertAudit(audit);

                return View("Actions/ViewPolicy", model);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/ViewPolicy", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet]
        public async Task<ActionResult> Active(int code)
        {
            try
            {
                ViewBag.CodeSend = code;
                modPolicy.Policy policy = await policiesServices.GetPolicyByCode(code);
                if (policy.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/ActivePolicy", null);
                }
                ViewBag.Success = true;
                ViewBag.EntyNull = false;

                ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
                return View("Actions/ActivePolicy", policy);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/ActivePolicy", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public async Task<ActionResult> ActivePolicy([FromBody] int code)
        {
            try
            {
                modPolicy.Policy request = await policiesServices.GetPolicyByCode(code);
                var responseIn = await policiesServices.ActivePolicy(request);
                var audit = new modAudit.Audit()
                {
                    Action = "Activar Politica",
                    UserCode = authenticatedUser.GetUserCode(),
                    Component = "Politicas",
                    ModifieldDate = DateTime.Now,
                    BeforeValue = JsonConvert.SerializeObject(request).ToString()
                };

                await auditsService.InsertAudit(audit);
                _logger.LogInformation($"Finaliza método PoliciesController.Active {responseIn}");
                return Ok(new { message = "Se activo la politica de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método PoliciesController.Active : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al activar la politica", state = "Error" });
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet]
        public async Task<ActionResult> Desactive(int code)
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            try
            {
                ViewBag.CodeSend = code;
                modPolicy.Policy policy = await policiesServices.GetPolicyByCode(code);
                if (policy.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/DesactivePolicy", null);
                }
                ViewBag.Success = true;
                ViewBag.EntyNull = false;

                return View("Actions/DesactivePolicy", policy);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/DesactivePolicy", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public async Task<ActionResult> DesactivePolicy([FromBody] int code)
        {
            try
            {
                modPolicy.Policy request = await policiesServices.GetPolicyByCode(code);
                var responseIn = await policiesServices.DesactivePolicy(request);
                var audit = new modAudit.Audit()
                {
                    Action = "Desactivar Politica",
                    UserCode = authenticatedUser.GetUserCode(),
                    Component = "Politicas",
                    ModifieldDate = DateTime.Now,
                    BeforeValue = JsonConvert.SerializeObject(request).ToString()
                };

                await auditsService.InsertAudit(audit);
                _logger.LogInformation($"Finaliza método PoliciesController.Desactive {responseIn}");
                return Ok(new { message = "Se desactivo la politica de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método PoliciesController.Desactive : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al desactivar la politica", state = "Error" });
            }
        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpGet]
        public ActionResult Filter()
        {
            FilterViewModel model = new FilterViewModel()
            {
                Rows = rows.RowsPolicies()

            };

            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            return View("Actions/Filter", model);
        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpPost]
        public async Task<ActionResult> FilterPolicies(FilterViewModel filter)
        {
            if (filter.TypeRow == "Select")
            {
                filter.ValueFilter = filter.ValueFilterOptions;
            }
            else if (filter.TypeRow == "Date")
            {
                filter.ValueFilter = filter.ValueFilterDate.ToString("dd/MM/yyyy");
            }

            return RedirectToAction("PoliciesFilter", new
            {
                filterColunm = filter.ColumValue,
                filterValue = filter.ValueFilter,
                typeRow = filter.TypeRow
            });
        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpPost]
        public async Task<IActionResult> GetSecondaryFilter([FromBody] string ColumValue)
        {
            if (string.IsNullOrEmpty(ColumValue))
            {
                return BadRequest("No a seleccionado ninguna opcion");
            }

            FilterViewModel filter = new FilterViewModel()
            {
                ColumValue = ColumValue
            };

            switch (ColumValue)
            {
                case "StateFormat":
                    filter.Options = new List<SelectListItem>().ToList();
                    filter.Options.Add(new SelectListItem("Activo", "Activo"));
                    filter.Options.Add(new SelectListItem("Inactivo", "Inactivo"));
                    filter.TypeRow = "Select";
                    break;
                case "CreationDateFormat":
                    filter.TypeRow = "Date";
                    break;
                case "UpdateDateFormat":
                    filter.TypeRow = "Date";
                    break;
                default:
                    filter.TypeRow = "General";
                    break;
            }

            return Ok(filter);
        }
    }
}
