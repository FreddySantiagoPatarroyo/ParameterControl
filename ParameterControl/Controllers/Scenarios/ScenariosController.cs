using Microsoft.AspNetCore.Mvc;
using ParameterControl.Models.Scenery;
using ParameterControl.Services.Scenarios;
using ParameterControl.Services.Rows;
using ParameterControl.Models.Conciliation;
using ParameterControl.Services.Conciliations;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Policy;
using ParameterControl.Services.Policies;
using modScenery = ParameterControl.Models.Scenery;
using Newtonsoft.Json;


namespace ParameterControl.Controllers.Scenarios
{
    public class ScenariosController : Controller
    {
        public TableScenariosViewModel TableScenarios = new TableScenariosViewModel();
        private readonly ILogger<HomeController> _logger;
        private readonly IScenariosServices scenariosServices;
        private readonly Rows rows;

        public ScenariosController(
        ILogger<HomeController> logger,
            IScenariosServices scenariosServices,
            Rows rows
        )
        {
            this._logger = logger;
            this.scenariosServices = scenariosServices;
            this.rows = rows;
        }

        [HttpGet]
        public async Task<ActionResult> Scenarios()
        {
           
            TableScenarios.Data = await scenariosServices.GetScenarios();

            TableScenarios.Rows = rows.RowsScenarios();

            TableScenarios.IsCreate = false;
            TableScenarios.IsActivate = true;
            TableScenarios.IsEdit = true;
            TableScenarios.IsInactivate = true;

            ViewBag.ApplyFilter = false;

            return View("Scenarios", TableScenarios);
        }

        [HttpGet]
        public async Task<ActionResult> ScenariosFilter(string filterColunm = "", string filterValue = "")
        {
            _logger.LogInformation(filterColunm);
            _logger.LogInformation(filterValue);

            if (filterColunm == null || filterColunm == "" || filterValue == null || filterValue == "")
            {
                return RedirectToAction("Scenarios");
            }

            FilterViewModel filter = new FilterViewModel()
            {
                ColumValue = filterColunm,
                ValueFilter = filterValue
            };

            List<Scenery> scenariosFilter = await scenariosServices.GetFilterScenarios(filter);
            TableScenarios.Data = scenariosFilter;

            TableScenarios.Rows = rows.RowsScenarios();

            TableScenarios.IsCreate = false;
            TableScenarios.IsActivate = true;
            TableScenarios.IsEdit = true;
            TableScenarios.IsInactivate = true;

            ViewBag.ApplyFilter = true;

            return View("Scenarios", TableScenarios);
        }

        [HttpGet]
        public async Task<ActionResult> Desactive(string id)
        {
            modScenery.Scenery scenery = await scenariosServices.GetSceneryById(id);

            return View("Actions/DesactiveScenarios", scenery);
        }
        [HttpPost]
        public async Task<ActionResult> DesactiveScenery([FromBody] string request)
        {
            try
            {
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
        public async Task<ActionResult> Edit(string id)
        {
            modScenery.Scenery scenery = await scenariosServices.GetSceneryById(id);

            List<string> ImpactOptionsList = await scenariosServices.GetImpact();
            List<string> ConciliationOptionsList = await scenariosServices.GetConciliation();

            SceneryCreateViewModel model = new SceneryCreateViewModel()
            {
                Id = scenery.Id,
                Code = scenery.Code,
                Name = scenery.Name,
                Impact = scenery.Impact,
                Conciliation = scenery.Conciliation,
                Query = scenery.Query,
                Parameter = scenery.Parameter,
                State = scenery.State

            };

            model.ImpactOptions = ImpactOptionsList;
            model.ConciliationOptions = ConciliationOptionsList;

            return View("Actions/EditScenarios", model);
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
        public async Task<ActionResult> View(string id)
        {
            Scenery scenery = await scenariosServices.GetSceneryById(id);

            return View("Actions/ViewScenarios", scenery);
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

        [HttpGet]
        public async Task<ActionResult> Active(string id)
        {
            modScenery.Scenery scenery = await scenariosServices.GetSceneryById(id);

            return View("Actions/ActiveScenery", scenery);
        }
        [HttpPost]
        public async Task<ActionResult> ActiveScenery([FromBody] string request)
        {
            try
            {
                _logger.LogInformation($"Inicia método ScenariosController.Active {JsonConvert.SerializeObject(request)}");
                return Ok(new { message = "Se activo el escenario de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método ScenariosController.Active : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al activar el escenario", state = "Error" });
            }
        }



        [HttpPost]
        public async Task<ActionResult> FilterScenarios(FilterViewModel filter)
        {

            Console.WriteLine(filter.ColumValue);

            Console.WriteLine(filter.ValueFilter);

            return RedirectToAction("ScenariosFilter", new
            {
                filterColunm = filter.ColumValue,
                filterValue = filter.ValueFilter
            });
        }
    }
}
