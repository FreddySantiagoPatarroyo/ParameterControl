using Microsoft.AspNetCore.Mvc;
using ParameterControl.Models.Scenery;
using ParameterControl.Services.Scenarios;
using ParameterControl.Services.Rows;

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
        public async Task<ActionResult> Scenarios(int id)
        {
            _logger.LogInformation("Hola");

            _logger.LogInformation(id.ToString());


            if (id > 0)
            {
                _logger.LogInformation("No Es nulo");
                TableScenarios.Data = new List<Scenery>();
                //TablePolicies.Data = policies;
            }
            else
            {
                TableScenarios.Data = await scenariosServices.GetScenarios();
            }

            TableScenarios.Rows = rows.RowsScenarios();

            TableScenarios.IsCreate = false;
            TableScenarios.IsActivate = true;
            TableScenarios.IsEdit = true;
            TableScenarios.IsInactivate = true;

            return View("Scenarios", TableScenarios);
        }

        [HttpGet]
        public async Task<ActionResult> Desactive(string id)
        {
            Scenery scenery = await scenariosServices.GetSceneryById(id);

            return View("Actions/DesactiveScenarios", scenery);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            Scenery scenery = await scenariosServices.GetSceneryById(id);

            return View("Actions/EditScenarios", scenery);
        }

        [HttpGet]
        public async Task<ActionResult> View(string id)
        {
            Scenery scenery = await scenariosServices.GetSceneryById(id);

            return View("Actions/ViewScenarios", scenery);
        }
    }
}
