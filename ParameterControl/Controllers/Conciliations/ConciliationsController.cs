using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ParameterControl.Models.Conciliation;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Models.Policy;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.Conciliations;
using ParameterControl.Services.Policies;
using ParameterControl.Services.Rows;
using System.Data;
using System.Security.Claims;
using modConciliation = ParameterControl.Models.Conciliation;
using modPolicy = ParameterControl.Models.Policy;

namespace ParameterControl.Controllers.Conciliations
{
    [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
    public class ConciliationsController : Controller
    {
        public TableConciliationViewModel TableConciliations = new TableConciliationViewModel();
        private readonly ILogger<HomeController> _logger;
        private readonly IConciliationsServices conciliationsServices;
        private readonly Rows rows;
        private readonly IPoliciesServices policiesServices;
        private readonly AuthenticatedUser authenticatedUser;
        private readonly IConfiguration _configuration;
        private readonly ClaimsPrincipal _principal;
        private readonly bool _isCreate;
        private readonly bool _isActivate;
        private readonly bool _isEdit;
        private readonly bool _isView;
        private readonly bool _isInactive;

        public ConciliationsController(
            ILogger<HomeController> logger,
            IConciliationsServices conciliationsServices,
            Rows rows,
            IPoliciesServices policiesServices,
            AuthenticatedUser authenticatedUser,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccesor
        )
        {
            this._logger = logger;
            this.conciliationsServices = conciliationsServices;
            this.rows = rows;
            this.policiesServices = policiesServices;
            this.authenticatedUser = authenticatedUser;
            _configuration = configuration;
            var context = httpContextAccesor.HttpContext;
            _principal = context.User as ClaimsPrincipal;
            var data = _principal.FindFirst(ClaimTypes.Role).Value;
            var section = _configuration.GetSection($"Permisos:{data}:Conciliations").GetChildren();
            _isCreate = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnCreate")).FirstOrDefault().Value);
            _isActivate = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnActivate")).FirstOrDefault().Value);
            _isEdit = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnEdit")).FirstOrDefault().Value);
            _isView = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnDetail")).FirstOrDefault().Value);
            _isInactive = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnInactive")).FirstOrDefault().Value);
        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpGet]
        public async Task<ActionResult> Conciliations(PaginationViewModel paginationViewModel)
        {
            try
            {
                List<modConciliation.Conciliation> Conciliations = await conciliationsServices.GetConciliationsPagination(paginationViewModel);
                int TotalConciliations = await conciliationsServices.CountConciliations();
                TableConciliations.Data = await conciliationsServices.GetConciliationsFormat(Conciliations);
                TableConciliations.Rows = rows.RowsConciliations();
                TableConciliations.IsCreate = _isCreate;
                TableConciliations.IsActivate = _isActivate;
                TableConciliations.IsEdit = _isEdit;
                TableConciliations.IsView = _isView;
                TableConciliations.IsInactivate = _isInactive;
                TableConciliations.Filter = true;
                ViewBag.ApplyFilter = false;

                var resultViemModel = new PaginationResult<TableConciliationViewModel>()
                {
                    Elements = TableConciliations,
                    Page = paginationViewModel.Page,
                    RecordsPage = paginationViewModel.RecordsPage,
                    TotalRecords = TotalConciliations,
                    BaseUrl = Url.Action() + "?"
                };

                ViewBag.Success = true;
                ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
                return View("Conciliations", resultViemModel);
            }
            catch (Exception ex)
            {
                ViewBag.Success = false;
                ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
                return View("Conciliations", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpGet]
        public async Task<ActionResult> ConciliationsFilter(PaginationViewModel paginationViewModel, string filterColunm = "", string filterValue = "", string typeRow = "")
        {
            try
            {
                if (filterColunm == null || filterColunm == "" || filterValue == null || filterValue == "")
                {
                    return RedirectToAction("Conciliations");
                }

                FilterViewModel filter = new FilterViewModel()
                {
                    ColumValue = filterColunm,
                    ValueFilter = filterValue,
                    TypeRow = typeRow
                };

                List<ConciliationViewModel> conciliationsFilter = await conciliationsServices.GetFilterConciliations(filter);
                int TotalConciliations = conciliationsFilter.Count();

                TableConciliations.Data = conciliationsServices.GetFilterPagination(conciliationsFilter, paginationViewModel, TotalConciliations);
                TableConciliations.Rows = rows.RowsConciliations();
                TableConciliations.IsCreate = _isCreate;
                TableConciliations.IsActivate = _isActivate;
                TableConciliations.IsEdit = _isEdit;
                TableConciliations.IsView = _isView;
                TableConciliations.IsInactivate = _isInactive;
                TableConciliations.Filter = true;
                ViewBag.ApplyFilter = true;

                var resultViemModel = new PaginationResult<TableConciliationViewModel>()
                {
                    Elements = TableConciliations,
                    Page = paginationViewModel.Page,
                    RecordsPage = paginationViewModel.RecordsPage,
                    TotalRecords = TotalConciliations,
                    BaseUrl = Url.Action() + "?filterColunm=" + filterColunm + "&filterValue=" + filterValue + "&typeRow=" + typeRow + "&"
                };

                ViewBag.Success = true;
                ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
                return View("ConciliationsFilter", resultViemModel);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("ConciliationsFilter", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            try
            {
                List<SelectListItem> PoliciesOptionsList = await GetPolicies();
                List<SelectListItem> RequiredOptionList = await conciliationsServices.GetRequired();
                List<SelectListItem> OperationTypeOptionsList = await conciliationsServices.GetOperationsType();
                List<SelectListItem> EmailsOptionsList = await conciliationsServices.GetEmailUsers();
                List<SelectListItem> SelectDestinationList = await GetDestination();

                ConciliationCreateViewModel model = new ConciliationCreateViewModel()
                {
                    OperationTypeOptions = OperationTypeOptionsList,
                    PoliciesOption = PoliciesOptionsList,
                    RequiredOption = RequiredOptionList,
                    Emails = EmailsOptionsList,
                    SelectDestination = SelectDestinationList
                };

                ViewBag.Success = true;
                ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
                return View("Actions/CreateConciliation", model);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("Actions/CreateConciliation", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] modConciliation.Conciliation request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Error en la informacion enviada", state = "Error" });
                }
                else
                {
                    var responseIn = await conciliationsServices.InsertConciliation(request);
                    _logger.LogInformation($"Finaliza método ConciliationController.Create {responseIn}");
                    return Ok(new { message = "Se creo la conciliación de manera exitosa", state = "Success" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método ConciliationController.Create : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al crear la conciliación", state = "Error" });
            }

        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet]
        public async Task<ActionResult> Edit(int code)
        {
            try
            {
                ViewBag.CodeSend = code;
                modConciliation.Conciliation conciliation = await conciliationsServices.GetConciliationsByCode(code);
                if (conciliation.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/EditConciliation", null);
                }
                List<SelectListItem> PoliciesOptionsList = await GetPolicies();
                List<SelectListItem> RequiredOptionsList = await conciliationsServices.GetRequired();
                List<SelectListItem> OperationTypeOptionsList = await conciliationsServices.GetOperationsType();
                List<SelectListItem> EmailsOptionsList = await conciliationsServices.GetEmailUsers();
                List<SelectListItem> SelectDestinationList = await GetDestination();

                ConciliationCreateViewModel model = await conciliationsServices.GetConciliationFormatCreate(conciliation);
                model.OperationTypeOptions = OperationTypeOptionsList;
                model.PoliciesOption = PoliciesOptionsList;
                model.RequiredOption = RequiredOptionsList;
                model.Emails = EmailsOptionsList;
                model.SelectDestination = SelectDestinationList;


                ViewBag.Success = true;
                ViewBag.EntyNull = false;
                ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
                return View("Actions/EditConciliation", model);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/EditConciliation", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public async Task<ActionResult> Edit([FromBody] modConciliation.Conciliation request)
        {
            try
            {
                var conciliation = await conciliationsServices.GetConciliationsByCode(request.Code);

                if (conciliation.Code == 0)
                {
                    _logger.LogError($"Error la conciliacion no existe : {JsonConvert.SerializeObject(request)}");
                    return BadRequest(new { message = "No existe una conciliacion con el codigo" + conciliation.Code, state = "Error" });
                }
                else
                {
                    request.CreationDate = conciliation.CreationDate;
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Error en la informacion enviada", state = "Error" });
                }
                else
                {
                    var responseIn = await conciliationsServices.UpdateConciliation(request);
                    _logger.LogInformation($"Finaliza método ConciliationController.Edit {responseIn}");
                    return Ok(new { message = "Se actualizo la conciliación de manera exitosa", state = "Success" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método ConciliationsController.Edit : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al actualizar la conciliación", state = "Error" });
            }

        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpGet]
        public async Task<ActionResult> View(int code)
        {
            try
            {
                ViewBag.CodeSend = code;
                modConciliation.Conciliation conciliation = await conciliationsServices.GetConciliationsByCode(code);
                if (conciliation.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/ViewConciliation", null);
                }
                ConciliationViewModel model = await conciliationsServices.GetConciliationFormat(conciliation);

                ViewBag.Success = true;
                ViewBag.EntyNull = false;
                ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
                return View("Actions/ViewConciliation", model);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/ViewConciliation", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpGet]
        public async Task<ActionResult> ViewPolicy(int code)
        {
            try
            {
                ViewBag.CodeSend = code;
                modPolicy.Policy policy = await policiesServices.GetPolicyByCode(code);
                if (policy.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/ViewPolicyConciliations", null);
                }
                PolicyViewModel model = await policiesServices.GetPolicyFormat(policy);

                ViewBag.Success = true;
                ViewBag.EntyNull = false;
                ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
                return View("Actions/ViewPolicyConciliations", model);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/ViewPolicyConciliations", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet]
        public async Task<ActionResult> Active(int code)
        {
            try
            {
                ViewBag.CodeSend = code;
                modConciliation.Conciliation conciliation = await conciliationsServices.GetConciliationsByCode(code);
                if (conciliation.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/ActiveConciliation", null);
                }

                ViewBag.Success = true;
                ViewBag.EntyNull = false;
                ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
                return View("Actions/ActiveConciliation", conciliation);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/ActiveConciliation", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public async Task<ActionResult> ActiveConciliation([FromBody] int code)
        {
            try
            {
                modConciliation.Conciliation request = await conciliationsServices.GetConciliationsByCode(code);
                var responseIn = await conciliationsServices.ActiveConciliation(request);
                _logger.LogInformation($"Finaliza método ConciliationController.Active {responseIn}");
                return Ok(new { message = "Se activo la conciliación de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método ConciliationsController.Active : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al activar la conciliación", state = "Error" });
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet]
        public async Task<ActionResult> Desactive(int code)
        {
            try
            {
                ViewBag.CodeSend = code;
                modConciliation.Conciliation conciliation = await conciliationsServices.GetConciliationsByCode(code);
                if (conciliation.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/DesactiveConciliation", null);
                }

                ViewBag.Success = true;
                ViewBag.EntyNull = false;
                ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
                return View("Actions/DesactiveConciliation", conciliation);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/DesactiveConciliation", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public async Task<ActionResult> DesactiveConciliation([FromBody] int code)
        {
            try
            {
                if (await conciliationsServices.ValidateScenariosActivos(code))
                {
                    _logger.LogError($"Error en el método ConciliationController.Desactive");
                    return BadRequest(new { message = "La conciliacion cuenta con esenarios activos, desactive los escenarios para desactivar la conciliacion", state = "Error" });
                }
                else
                {
                    modConciliation.Conciliation request = await conciliationsServices.GetConciliationsByCode(code);
                    var responseIn = await conciliationsServices.DesactiveConciliation(request);
                    _logger.LogInformation($"Finaliza método ConciliationController.Desactive {responseIn}");
                    return Ok(new { message = "Se desactivo la conciliación de manera exitosa", state = "Success" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método ConciliationController.Desactive : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al desactivar la conciliación", state = "Error" });
            }

        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpGet]
        public ActionResult Filter()
        {
            FilterViewModel model = new FilterViewModel()
            {
                Rows = rows.RowsConciliations()
            };
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            return View("Actions/Filter", model);
        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpPost]
        public async Task<ActionResult> FilterConciliations(FilterViewModel filter)
        {
            if (filter.TypeRow == "Select")
            {
                filter.ValueFilter = filter.ValueFilterOptions;
            }
            else if (filter.TypeRow == "Date")
            {
                filter.ValueFilter = filter.ValueFilterDate.ToString("dd/MM/yyyy");
            }

            return RedirectToAction("ConciliationsFilter", new
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
                case "RequiredFormat":
                    filter.Options = await conciliationsServices.GetRequired();
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

        public async Task<List<SelectListItem>> GetPolicies()
        {
            List<modPolicy.Policy> policies = await conciliationsServices.GetPolicies();
            return policies.Select(policy => new SelectListItem(policy.Name, policy.Code.ToString())).ToList();
        }
        public async Task<List<SelectListItem>> GetDestination()
        {
            List<string> detinations = await conciliationsServices.GetDestinations();
            return detinations.Select(detination => new SelectListItem(detination, detination)).ToList();

        }
    }
}
