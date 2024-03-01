using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Models.Scenery;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.Rows;
using ParameterControl.Services.Scenarios;
using System.Security.Claims;
using modConciliation = ParameterControl.Models.Conciliation;
using modScenery = ParameterControl.Models.Scenery;


namespace ParameterControl.Controllers.Scenarios
{
    [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
    public class ScenariosController : Controller
    {
        public TableScenariosViewModel TableScenarios = new TableScenariosViewModel();
        private readonly ILogger<HomeController> _logger;
        private readonly IScenariosServices scenariosServices;
        private readonly Rows rows;
        private readonly AuthenticatedUser authenticatedUser;
        private readonly IConfiguration _configuration;
        private readonly ClaimsPrincipal _principal;
        private readonly bool _isCreate;
        private readonly bool _isActivate;
        private readonly bool _isEdit;
        private readonly bool _isView;
        private readonly bool _isInactive;

        public ScenariosController(
        ILogger<HomeController> logger,
            IScenariosServices scenariosServices,
            Rows rows,
            AuthenticatedUser authenticatedUser,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccesor
        )
        {
            this._logger = logger;
            this.scenariosServices = scenariosServices;
            this.rows = rows;
            this.authenticatedUser = authenticatedUser;
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

        [HttpGet]
        public async Task<ActionResult> Scenarios(PaginationViewModel paginationViewModel)
        {
            try
            {
                List<modScenery.Scenery> Scenarios = await scenariosServices.GetScenariosPagination(paginationViewModel);
                int TotalScenarios = await scenariosServices.CountScenarios();

                TableScenarios.Data = await scenariosServices.GetScenariosFormat(Scenarios);
                TableScenarios.Rows = rows.RowsScenarios();
                TableScenarios.IsCreate = _isCreate;
                TableScenarios.IsActivate = _isActivate;
                TableScenarios.IsEdit = _isEdit;
                TableScenarios.IsView = _isView;
                TableScenarios.IsInactivate = _isInactive;
                TableScenarios.Filter = true;
                ViewBag.ApplyFilter = false;

                var resultViemModel = new PaginationResult<TableScenariosViewModel>()
                {
                    Elements = TableScenarios,
                    Page = paginationViewModel.Page,
                    RecordsPage = paginationViewModel.RecordsPage,
                    TotalRecords = TotalScenarios,
                    BaseUrl = Url.Action() + "?"
                };

                ViewBag.Success = true;
                return View("Scenarios", resultViemModel);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("Scenarios", null);
            }
        }

        [HttpGet]
        public async Task<ActionResult> ScenariosFilter(PaginationViewModel paginationViewModel, string filterColunm = "", string filterValue = "", string typeRow = "")
        {
            try
            {
                if (filterColunm == null || filterColunm == "" || filterValue == null || filterValue == "")
                {
                    return RedirectToAction("Scenarios");
                }

                FilterViewModel filter = new FilterViewModel()
                {
                    ColumValue = filterColunm,
                    ValueFilter = filterValue,
                    TypeRow = typeRow
                };

                List<SceneryViewModel> scenariosFilter = await scenariosServices.GetFilterScenarios(filter);
                int TotalScenarios = scenariosFilter.Count();

                TableScenarios.Data = scenariosServices.GetFilterPagination(scenariosFilter, paginationViewModel, TotalScenarios);
                TableScenarios.Rows = rows.RowsScenarios();
                TableScenarios.IsCreate = _isCreate;
                TableScenarios.IsActivate = _isActivate;
                TableScenarios.IsEdit = _isEdit;
                TableScenarios.IsView = _isView;
                TableScenarios.IsInactivate = _isInactive;
                TableScenarios.Filter = true;
                ViewBag.ApplyFilter = true;

                var resultViemModel = new PaginationResult<TableScenariosViewModel>()
                {
                    Elements = TableScenarios,
                    Page = paginationViewModel.Page,
                    RecordsPage = paginationViewModel.RecordsPage,
                    TotalRecords = TotalScenarios,
                    BaseUrl = Url.Action() + "?filterColunm=" + filterColunm + "&filterValue=" + filterValue + "&typeRow=" + typeRow + "&"
                };

                ViewBag.Success = true;
                return View("Scenarios", resultViemModel);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("Scenarios", null);
            }
        }


        [HttpGet]
        public async Task<ActionResult> Create()
        {
            try
            {
                List<SelectListItem> ImpactOptionsList = await scenariosServices.GetImpact();
                List<SelectListItem> ConciliationOptionsList = await GetConciliation();

                SceneryCreateViewModel model = new SceneryCreateViewModel()
                {
                    ImpactOptions = ImpactOptionsList,
                    ConciliationOptions = ConciliationOptionsList
                };

                ViewBag.Success = true;
                return View("Actions/CreateScenery", model);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("Actions/CreateScenery", null);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] modScenery.Scenery request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Error en la informacion enviada", state = "Error" });
                }
                else
                {
                    var responseIn = await scenariosServices.InsertScenery(request);
                    _logger.LogInformation($"Finaliza método ScenariosController.Create {responseIn}");
                    return Ok(new { message = "Se creo el escenario de manera exitosa", state = "Success" });

                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método ScenariosController.Create : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al crear el escenario", state = "Error" });
            }

        }

        [HttpGet]
        public async Task<ActionResult> Edit(int code)
        {
            try
            {
                ViewBag.CodeSend = code;
                modScenery.Scenery scenery = await scenariosServices.GetSceneryByCode(code);
                if (scenery.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/EditScenary", null);
                }
                List<SelectListItem> ImpactOptionsList = await scenariosServices.GetImpact();
                List<SelectListItem> ConciliationOptionsList = await GetConciliation();

                SceneryCreateViewModel model = await scenariosServices.GetSceneryFormatCreate(scenery);
                model.ImpactOptions = ImpactOptionsList;
                model.ConciliationOptions = ConciliationOptionsList;

                ViewBag.Success = true;
                ViewBag.EntyNull = false;
                return View("Actions/EditScenary", model);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/EditScenary", null);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromBody] modScenery.Scenery request)
        {
            try
            {
                var scenery = await scenariosServices.GetSceneryByCode(request.Code);

                if (scenery.Code == 0)
                {
                    _logger.LogError($"Error el escenario no existe : {JsonConvert.SerializeObject(request)}");
                    return BadRequest(new { message = "No existe el escenario con el codigo" + scenery.Code, state = "Error" });
                }
                else
                {
                    request.CreationDate = scenery.CreationDate;
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError($"Error en el modelo : {JsonConvert.SerializeObject(request)}");
                    return BadRequest(new { message = "Error en la informacion enviada", state = "Error" });
                }
                else
                {
                    var responseIn = await scenariosServices.UpdateScenery(request);
                    _logger.LogInformation($"Finaliza método ScenariosController.Edit {responseIn}");
                    return Ok(new { message = "Se actualizo el escenario de manera exitosa", state = "Success" });

                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método ScenariosController.Edit : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al actualizar el escenario", state = "Error" });
            }

        }

        [HttpGet]
        public async Task<ActionResult> View(int code)
        {
            try
            {
                ViewBag.CodeSend = code;
                Scenery scenery = await scenariosServices.GetSceneryByCode(code);
                if (scenery.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/ViewScenery", null);
                }
                SceneryViewModel model = await scenariosServices.GetSceneryFormat(scenery);

                ViewBag.Success = true;
                ViewBag.EntyNull = false;
                return View("Actions/ViewScenery", model);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/ViewScenery", null);
            }

        }

        [HttpGet]
        public async Task<ActionResult> Active(int code)
        {
            try
            {
                ViewBag.CodeSend = code;
                modScenery.Scenery scenery = await scenariosServices.GetSceneryByCode(code);
                if (scenery.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/ActiveScenery", null);
                }
                ViewBag.Success = true;
                ViewBag.EntyNull = false;
                return View("Actions/ActiveScenery", scenery);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/ActiveScenery", null);
            }


        }

        [HttpPost]
        public async Task<ActionResult> ActiveScenery([FromBody] int code)
        {
            try
            {
                Scenery request = await scenariosServices.GetSceneryByCode(code);
                var responseIn = await scenariosServices.ActiveScenery(request);
                _logger.LogInformation($"Finaliza método ScenariosController.Active {responseIn}");
                return Ok(new { message = "Se activo el escenario de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método ScenariosController.Active : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al activar el escenario", state = "Error" });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Desactive(int code)
        {
            try
            {
                ViewBag.CodeSend = code;
                modScenery.Scenery scenery = await scenariosServices.GetSceneryByCode(code);
                if (scenery.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/DesactiveScenery", null);
                }
                ViewBag.Success = true;
                ViewBag.EntyNull = false;
                return View("Actions/DesactiveScenery", scenery);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/DesactiveScenery", null);
            }

        }

        [HttpPost]
        public async Task<ActionResult> DesactiveScenery([FromBody] int code)
        {
            try
            {
                Scenery request = await scenariosServices.GetSceneryByCode(code);
                var responseIn = await scenariosServices.DesactiveScenery(request);
                _logger.LogInformation($"Finaliza método ScenariosController.Desactive {responseIn}");
                return Ok(new { message = "Se desactivo el escenario de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método ScenariosController.Desactive : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al desactivar el escenario", state = "Error" });
            }
        }

        [HttpGet]
        public ActionResult Filter()
        {
            FilterViewModel model = new FilterViewModel()
            {
                Rows = rows.RowsScenarios()

            };

            return View("Actions/Filter", model);
        }

        [HttpPost]
        public async Task<ActionResult> FilterScenarios(FilterViewModel filter)
        {
            if (filter.TypeRow == "Select")
            {
                filter.ValueFilter = filter.ValueFilterOptions;
            }
            else if (filter.TypeRow == "Date")
            {
                filter.ValueFilter = filter.ValueFilterDate.ToString("dd/MM/yyyy");
            }

            return RedirectToAction("ScenariosFilter", new
            {
                filterColunm = filter.ColumValue,
                filterValue = filter.ValueFilter,
                typeRow = filter.TypeRow
            });
        }

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
                case "Impact":
                    filter.Options = await scenariosServices.GetImpact();
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

        public async Task<List<SelectListItem>> GetConciliation()
        {
            List<modConciliation.Conciliation> conciliations = await scenariosServices.GetConciliations();
            return conciliations.Select(policy => new SelectListItem(policy.Name, policy.Code.ToString())).ToList();
        }
    }
}
