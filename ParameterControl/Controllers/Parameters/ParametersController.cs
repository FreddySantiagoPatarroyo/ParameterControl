using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Models.Parameter;
using ParameterControl.Services.Audit;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.Parameters;
using ParameterControl.Services.Rows;
using ParameterControl.Services.Scenarios;
using System.Security.Claims;
using modConciliation = ParameterControl.Models.Conciliation;
using modParameter = ParameterControl.Models.Parameter;
using modScenary = ParameterControl.Models.Scenery;
using modAudit = ParameterControl.Models.Audit;
using ParameterControl.Models.Conciliation;
using System.Reflection.Metadata;

namespace ParameterControl.Controllers.Parameters
{
   
    public class ParametersController : Controller
    {
        public TableParametersViewModel TableParameters = new TableParametersViewModel();
        private readonly ILogger<HomeController> _logger;
        private readonly IParametersService parametersService;
        private readonly IScenariosServices scenariosServices;
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

        public ParametersController(
            ILogger<HomeController> logger,
            IParametersService parametersService,
            IScenariosServices scenariosServices,
            Rows rows,
            AuthenticatedUser authenticatedUser,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccesor,
            IAuditsService auditsService
        )
        {
            this._logger = logger;
            this.parametersService = parametersService;
            this.scenariosServices = scenariosServices;
            this.rows = rows;
            this.authenticatedUser = authenticatedUser;
            _configuration = configuration;
            this.auditsService = auditsService;
            var context = httpContextAccesor.HttpContext;
            _principal = context.User as ClaimsPrincipal;
            var data = _principal.FindFirst(ClaimTypes.Role).Value;
            var section = _configuration.GetSection($"Permisos:{data}:Parameters").GetChildren();
            _isCreate = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnCreate")).FirstOrDefault().Value);
            _isActivate = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnActivate")).FirstOrDefault().Value);
            _isEdit = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnEdit")).FirstOrDefault().Value);
            _isView = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnDetail")).FirstOrDefault().Value);
            _isInactive = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnInactive")).FirstOrDefault().Value);
        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpGet]
        public async Task<ActionResult> Parameters(PaginationViewModel paginationViewModel)
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            try
            {
                List<modParameter.Parameter> parameters = await parametersService.GetParametersPagination(paginationViewModel);
                int TotalParameters = await parametersService.CountParameters();

                TableParameters.Data = await parametersService.GetParametersFormat(parameters);
                TableParameters.Rows = rows.RowsParameters();
                TableParameters.Filter = true;
                TableParameters.IsCreate = _isCreate;
                TableParameters.IsActivate = _isActivate;
                TableParameters.IsEdit = _isEdit;
                TableParameters.IsView = _isView;
                TableParameters.IsInactivate = _isInactive;
                TableParameters.Filter = true;
                ViewBag.ApplyFilter = false;

                var resultViemModel = new PaginationResult<TableParametersViewModel>()
                {
                    Elements = TableParameters,
                    Page = paginationViewModel.Page,
                    RecordsPage = paginationViewModel.RecordsPage,
                    TotalRecords = TotalParameters,
                    BaseUrl = Url.Action() + "?"
                };

                ViewBag.Success = true;
                return View("Parameters", resultViemModel);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("Parameters", null);
            }

        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpGet]
        public async Task<ActionResult> ParametersFilter(PaginationViewModel paginationViewModel, string filterColunm = "", string filterValue = "", string typeRow = "")
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            try
            {
                if (filterColunm == null || filterColunm == "" || filterValue == null || filterValue == "")
                {
                    return RedirectToAction("Parameters");
                }

                FilterViewModel filter = new FilterViewModel()
                {
                    ColumValue = filterColunm,
                    ValueFilter = filterValue,
                    TypeRow = typeRow
                };

                List<ParameterViewModel> parametersFilter = await parametersService.GetFilterParameters(filter);
                int TotalParameters = parametersFilter.Count();

                TableParameters.Data = parametersService.GetFilterPagination(parametersFilter, paginationViewModel, TotalParameters);
                TableParameters.Rows = rows.RowsParameters();
                TableParameters.Filter = true;
                TableParameters.IsCreate = _isCreate;
                TableParameters.IsActivate = _isActivate;
                TableParameters.IsEdit = _isEdit;
                TableParameters.IsView = _isView;
                TableParameters.IsInactivate = _isInactive;

                var resultViemModel = new PaginationResult<TableParametersViewModel>()
                {
                    Elements = TableParameters,
                    Page = paginationViewModel.Page,
                    RecordsPage = paginationViewModel.RecordsPage,
                    TotalRecords = TotalParameters,
                    BaseUrl = Url.Action() + "?filterColunm=" + filterColunm + "&filterValue=" + filterValue + "&typeRow=" + typeRow + "&"
                };

                ViewBag.ApplyFilter = true;
                ViewBag.Success = true;
                return View("ParametersFilter", resultViemModel);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("ParametersFilter", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpGet]
        public async Task<ActionResult> ParametersConciliationFilter(PaginationViewModel paginationViewModel, string conciliation = "")
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            try
            {
                if (conciliation == null)
                {
                    return RedirectToAction("Parameters");
                }

                List<modParameter.Parameter> parametersConciliation = await parametersService.GetParametersByConciliation(conciliation);
                List<ParameterViewModel> parametersFilter = await parametersService.GetParametersFormat(parametersConciliation);
                int TotalParameters = parametersFilter.Count();

                TableParameters.Data = parametersService.GetFilterPagination(parametersFilter, paginationViewModel, TotalParameters);
                TableParameters.Rows = rows.RowsParameters();
                TableParameters.Filter = true;
                TableParameters.IsCreate = _isCreate;
                TableParameters.IsActivate = _isActivate;
                TableParameters.IsEdit = _isEdit;
                TableParameters.IsView = _isView;
                TableParameters.IsInactivate = _isInactive;

                var resultViemModel = new PaginationResult<TableParametersViewModel>()
                {
                    Elements = TableParameters,
                    Page = paginationViewModel.Page,
                    RecordsPage = paginationViewModel.RecordsPage,
                    TotalRecords = TotalParameters,
                    BaseUrl = Url.Action() + "?conciliation=" + conciliation + "&"
                };

                ViewBag.ApplyFilter = true;
                ViewBag.Success = true;
                return View("ParametersFilterConciliation", resultViemModel);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("ParametersFilterConciliation", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            try
            {
                List<SelectListItem> ParameterTypeOptionList = await parametersService.GetParameterType();
                List<SelectListItem> ListScenarios = await GetListValite(string.Empty);

                ParameterCreateViewModel model = new ParameterCreateViewModel()
                {
                    ParameterTypeOption = ParameterTypeOptionList,
                    ListScenarios = ListScenarios
                };

                ViewBag.Success = true;
                return View("Actions/CreateParameter", model);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("Actions/CreateParameter", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] modParameter.Parameter request)
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
                    var responseIn = await parametersService.InsertParameter(request);
                    var audit = new modAudit.Audit()
                    {
                        Action = "Crear Parametro",
                        UserCode = authenticatedUser.GetUserCode(),
                        Component = "Parametros",
                        ModifieldDate = DateTime.Now,
                        BeforeValue = ""
                    };

                    await auditsService.InsertAudit(audit);
                    _logger.LogInformation($"Finaliza método ParametersController.Create {responseIn}");
                    return Ok(new { message = "Se creo el parametro de manera exitosa", state = "Success" });

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método ParametersController.Create : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al crear el parametro", state = "Error" });
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
                modParameter.Parameter parameter = await parametersService.GetParameterByCode(code);
                if (parameter.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/EditParameter", null);
                }
                List<SelectListItem> ParameterTypeList = await parametersService.GetParameterType();
                List<SelectListItem> ListScenarios = await GetListValite(parameter.ParameterType);

                ParameterCreateViewModel model = await parametersService.GetParameterFormatCreate(parameter);
                model.ParameterTypeOption = ParameterTypeList;
                model.ListScenarios = ListScenarios;

                ViewBag.Success = true;
                ViewBag.EntyNull = false;
                return View("Actions/EditParameter", model);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/EditParameter", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] modParameter.Parameter request)
        {
            try
            {
                var parameter = await parametersService.GetParameterByCode(request.Code);

                if (parameter.Code == 0)
                {
                    _logger.LogError($"Error el parametro no existe : {JsonConvert.SerializeObject(request)}");
                    return BadRequest(new { message = "No existe un parametro con el codigo" + parameter.Code, state = "Error" });
                }
                else
                {
                    request.CreationDate = parameter.CreationDate;
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError($"Error en el modelo : {JsonConvert.SerializeObject(request)}");
                    return BadRequest(new { message = "Error en la informacion enviada", state = "Error" });
                }
                else
                {
                    var responseIn = await parametersService.UpdateParameter(request);
                    var audit = new modAudit.Audit()
                    {
                        Action = "Editar Parametro",
                        UserCode = authenticatedUser.GetUserCode(),
                        Component = "Parametros",
                        ModifieldDate = DateTime.Now,
                        BeforeValue = JsonConvert.SerializeObject(parameter).ToString()
                    };

                    await auditsService.InsertAudit(audit);
                    _logger.LogInformation($"Finaliza método ParametersController.Edit {responseIn}");
                    return Ok(new { message = "Se actualizo el parametro de manera exitosa", state = "Success" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método ParametersController.Edit : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al actualizar el parametro", state = "Error" });
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
                modParameter.Parameter parameter = await parametersService.GetParameterByCode(code);
                if (parameter.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/ViewParameter", null);
                }
                ParameterViewModel model = await parametersService.GetParameterFormat(parameter);

                var audit = new modAudit.Audit()
                {
                    Action = "Ver Parametro",
                    UserCode = authenticatedUser.GetUserCode(),
                    Component = "Parametros",
                    ModifieldDate = DateTime.Now,
                    BeforeValue = ""
                };

                await auditsService.InsertAudit(audit);

                ViewBag.Success = true;
                ViewBag.EntyNull = false;
                return View("Actions/ViewParameter", model);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/ViewParameter", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet]
        public async Task<ActionResult> Active(int code)
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            try
            {
                ViewBag.CodeSend = code;
                modParameter.Parameter parameter = await parametersService.GetParameterByCode(code);
                if (parameter.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/ActiveParameter", null);
                }

                ViewBag.Success = true;
                ViewBag.EntyNull = false;
                return View("Actions/ActiveParameter", parameter);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/ActiveParameter", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public async Task<ActionResult> ActiveParameter([FromBody] int code)
        {
            try
            {
                modParameter.Parameter request = await parametersService.GetParameterByCode(code);
                var responseIn = await parametersService.ActiveParameter(request);
                var audit = new modAudit.Audit()
                {
                    Action = "Activar Parametro",
                    UserCode = authenticatedUser.GetUserCode(),
                    Component = "Parametros",
                    ModifieldDate = DateTime.Now,
                    BeforeValue = JsonConvert.SerializeObject(request).ToString()
                };

                await auditsService.InsertAudit(audit);
                _logger.LogInformation($"Finaliza método ParametersController.Active {responseIn}");
                return Ok(new { message = "Se activo el parametro de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método ParametersController.Active : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al activar el parametro", state = "Error" });
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
                modParameter.Parameter parameter = await parametersService.GetParameterByCode(code);
                if (parameter.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/DesactiveParameter", null);
                }

                ViewBag.Success = true;
                ViewBag.EntyNull = false;
                return View("Actions/DesactiveParameter", parameter);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/DesactiveParameter", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public async Task<ActionResult> DesactiveParameter([FromBody] int code)
        {
            try
            {
                modParameter.Parameter request = await parametersService.GetParameterByCode(code);
                var responseIn = await parametersService.DesactiveParameter(request);
                var audit = new modAudit.Audit()
                {
                    Action = "Desactivar Parametro",
                    UserCode = authenticatedUser.GetUserCode(),
                    Component = "Parametros",
                    ModifieldDate = DateTime.Now,
                    BeforeValue = JsonConvert.SerializeObject(request).ToString()
                };

                await auditsService.InsertAudit(audit);
                _logger.LogInformation($"Finaliza método ParametersController.Desactive {responseIn}");
                return Ok(new { message = "Se desactivo el parametro de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método ParametersController.Desactive : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al desactivar el parametro", state = "Error" });
            }
        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpGet]
        public ActionResult Filter()
        {
            FilterViewModel model = new FilterViewModel()
            {
                Rows = rows.RowsParameters()

            };
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            return View("Actions/Filter", model);
        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpPost]
        public async Task<ActionResult> FilterParameters(FilterViewModel filter)
        {
            if (filter.TypeRow == "Select")
            {
                filter.ValueFilter = filter.ValueFilterOptions;
            }
            else if (filter.TypeRow == "Date")
            {
                filter.ValueFilter = filter.ValueFilterDate.ToString("dd/MM/yyyy");
            }

            return RedirectToAction("ParametersFilter", new
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
                case "ParameterType":
                    filter.Options = await parametersService.GetParameterType();
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

        [HttpGet]
        public async Task<ActionResult> FilterConciliation()
        {
            FilterConciliacionViewModel model = new FilterConciliacionViewModel()
            {
                Conciliations = await GetConciliations()

            };
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            return View("Actions/FilterConciliation", model);
        }

        [HttpPost]
        public async Task<ActionResult> FilterConciliationParameters(FilterConciliacionViewModel filter)
        {
            return RedirectToAction("ParametersConciliationFilter", new
            {
                conciliation = filter.Conciliation,
            });
        }

        public async Task<List<SelectListItem>> GetParameters()
        {
            List<modParameter.Parameter> parameters = await parametersService.GetListParameter();
            return parameters.Select(parameter => new SelectListItem(parameter.Parameter_, parameter.Code.ToString())).ToList();
        }

        public async Task<List<SelectListItem>> GetScenarios()
        {
            List<modScenary.Scenery> scenarios = await scenariosServices.GetActiveScenarios();
            return scenarios.Select(scenary => new SelectListItem(scenary.Name, scenary.Code.ToString())).ToList();
        }

        public async Task<List<SelectListItem>> GetConciliations()
        {
            List<modConciliation.Conciliation> conciliations = await parametersService.GetConciliations();
            return conciliations.Select(conciliation => new SelectListItem(conciliation.Name, conciliation.Name)).ToList();
        }

        [HttpPost]
        public async Task<IActionResult> GetList([FromBody] string type)
        {
            var List = await GetListValite(type);
            return Ok(List);
        }

        private async Task<List<SelectListItem>> GetListValite(string type)
        {
            if(type == string.Empty)
            {
                return new List<SelectListItem>();
            }

            switch (type)
            {
                case "ESCENARIO":
                    return await GetScenarios();
                default:
                    return new List<SelectListItem>();
            }
        }
    }
}
