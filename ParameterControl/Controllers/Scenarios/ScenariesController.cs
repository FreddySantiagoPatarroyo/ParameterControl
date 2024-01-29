using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Scenery;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.Rows;
using ParameterControl.Services.Scenarios;
using modConciliation = ParameterControl.Models.Conciliation;
using modScenery = ParameterControl.Models.Scenery;


namespace ParameterControl.Controllers.Scenarios
{
    public class ScenariosController : Controller
    {
        public TableScenariosViewModel TableScenarios = new TableScenariosViewModel();
        private readonly ILogger<HomeController> _logger;
        private readonly IScenariosServices scenariosServices;
        private readonly Rows rows;
        private readonly AuthenticatedUser authenticatedUser;

        public ScenariosController(
        ILogger<HomeController> logger,
            IScenariosServices scenariosServices,
            Rows rows,
            AuthenticatedUser authenticatedUser
        )
        {
            this._logger = logger;
            this.scenariosServices = scenariosServices;
            this.rows = rows;
            this.authenticatedUser = authenticatedUser;
        }

        [HttpGet]
        public async Task<ActionResult> Scenarios()
        {
            List<modScenery.Scenery> Scenarios = await scenariosServices.GetScenarios();

            TableScenarios.Data = await scenariosServices.GetScenariosFormat(Scenarios);

            TableScenarios.Rows = rows.RowsScenarios();

            TableScenarios.IsCreate = true;
            TableScenarios.IsActivate = true;
            TableScenarios.IsEdit = true;
            TableScenarios.IsInactivate = true;
            TableScenarios.Filter = true;

            ViewBag.ApplyFilter = false;

            return View("Scenarios", TableScenarios);
        }

        [HttpGet]
        public async Task<ActionResult> ScenariosFilter(string filterColunm = "", string filterValue = "", string typeRow = "")
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
            TableScenarios.Data = scenariosFilter;

            TableScenarios.Rows = rows.RowsScenarios();

            TableScenarios.IsCreate = true;
            TableScenarios.IsActivate = true;
            TableScenarios.IsEdit = true;
            TableScenarios.IsInactivate = true;
            TableScenarios.Filter = true;

            ViewBag.ApplyFilter = true;

            return View("Scenarios", TableScenarios);
        }


        [HttpGet]
        public async Task<ActionResult> Create()
        {

            List<SelectListItem> ImpactOptionsList = await scenariosServices.GetImpact();
            List<SelectListItem> ConciliationOptionsList = await GetConciliation();

            SceneryCreateViewModel model = new SceneryCreateViewModel()
            {
                ImpactOptions = ImpactOptionsList,
                ConciliationOptions = ConciliationOptionsList
            };

            return View("Actions/CreateScenery", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] modScenery.Scenery request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Error en la informacion enviada", state = "Error" });
            }
            else
            {
                try
                {
                    request.UserOwner = authenticatedUser.GetUserOwnerId();
                    request.CreationDate = DateTime.Now;
                    request.UpdateDate = DateTime.Now;
                    _logger.LogInformation($"Inicia método ConciliationController.Create {JsonConvert.SerializeObject(request)}");
                    //var responseIn = await conciliationsServices.InsertConciliation(request);
                    //_logger.LogInformation($"Finaliza método ConciliationController.Create {responseIn}");
                    return Ok(new { message = "Se creo el escenario de manera exitosa", state = "Success" });
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error en el método ConciliationController.Create : {JsonConvert.SerializeObject(ex.Message)}");
                    return BadRequest(new { message = "Error al crear el escenario", state = "Error" });
                }
            }

        }

        [HttpGet]
        public async Task<ActionResult> Edit(int code)
        {
            modScenery.Scenery scenery = await scenariosServices.GetSceneryByCode(code);

            List<SelectListItem> ImpactOptionsList = await scenariosServices.GetImpact();
            List<SelectListItem> ConciliationOptionsList = await GetConciliation();

            SceneryCreateViewModel model = await scenariosServices.GetSceneryFormatCreate(scenery);

            //SceneryCreateViewModel model = new SceneryCreateViewModel()
            //{
            //    Code = scenery.Code,
            //    Name = scenery.Name,
            //    Impact = scenery.Impact,
            //    Conciliation = scenery.Conciliation,
            //    State = scenery.State,
            //    CreationDate = scenery.CreationDate
            //};

            model.ImpactOptions = ImpactOptionsList;
            model.ConciliationOptions = ConciliationOptionsList;

            return View("Actions/EditScenary", model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromBody] modScenery.Scenery request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Error en el modelo : {JsonConvert.SerializeObject(request)}");
                return BadRequest(new { message = "Error en la informacion enviada", state = "Error" });
            }
            else
            {
                try
                {
                    request.UserOwner = authenticatedUser.GetUserOwnerId();
                    request.UpdateDate = DateTime.Now;
                    _logger.LogInformation($"Inicia método ScenariosController.Edit {JsonConvert.SerializeObject(request)}");
                    return Ok(new { message = "Se actualizo el escenario de manera exitosa", state = "Success" });
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error en el método ScenariosController.Edit : {JsonConvert.SerializeObject(ex.Message)}");
                    return BadRequest(new { message = "Error al actualizar el escenario", state = "Error" });
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> View(int code)
        {
            Scenery scenery = await scenariosServices.GetSceneryByCode(code);

            return View("Actions/ViewScenery", scenery);
        }

        [HttpGet]
        public async Task<ActionResult> Active(int code)
        {
            modScenery.Scenery scenery = await scenariosServices.GetSceneryByCode(code);

            return View("Actions/ActiveScenery", scenery);
        }

        [HttpPost]
        public async Task<ActionResult> ActiveScenery([FromBody] int code)
        {
            try
            {
                Scenery request = await scenariosServices.GetSceneryByCode(code);
                request.UserOwner = authenticatedUser.GetUserOwnerId();
                request.UpdateDate = DateTime.Now;
                request.State = true;
                _logger.LogInformation($"Inicia método ScenariosController.Active {JsonConvert.SerializeObject(request)}");
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
            modScenery.Scenery scenery = await scenariosServices.GetSceneryByCode(code);

            return View("Actions/DesactiveScenery", scenery);
        }

        [HttpPost]
        public async Task<ActionResult> DesactiveScenery([FromBody] int code)
        {
            try
            {
                Scenery request = await scenariosServices.GetSceneryByCode(code);
                request.UserOwner = authenticatedUser.GetUserOwnerId();
                request.UpdateDate = DateTime.Now;
                request.State = false;
                _logger.LogInformation($"Inicia método ScenariosController.Desactive {JsonConvert.SerializeObject(request)}");
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
            List<modConciliation.Conciliation> conciliations = await scenariosServices.GetConciliation();
            return conciliations.Select(policy => new SelectListItem(policy.Name, policy.Code.ToString())).ToList();
        }
    }
}
