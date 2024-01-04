using Microsoft.AspNetCore.Mvc;
using ParameterControl.Models.Conciliation;
using ParameterControl.Models.Parameter;
using ParameterControl.Services.Conciliations;
using ParameterControl.Services.Parameters;
using ParameterControl.Services.Rows;

namespace ParameterControl.Controllers.Parameters
{
    public class ParametersController : Controller
    {
        public TableParametersViewModel TableParameters = new TableParametersViewModel();
        private readonly ILogger<HomeController> _logger;
        private readonly IParametersService parametersService; 
        private readonly Rows rows;

        public ParametersController(
            ILogger<HomeController> logger,
            IParametersService parametersService,
            Rows rows
        )
        {
            this._logger = logger;
            this.parametersService = parametersService;
            this.rows = rows;
        }

        [HttpGet]
        public async Task<ActionResult> Parameters(int id)
        {
            _logger.LogInformation("Hola");

            _logger.LogInformation(id.ToString());


            if (id > 0)
            {
                _logger.LogInformation("No Es nulo");
                TableParameters.Data = new List<Parameter>();
            }
            else
            {
                TableParameters.Data = await parametersService.GetParameters();
            }

            TableParameters.Rows = rows.RowsParameters();

            TableParameters.Filter = true;
            TableParameters.IsCreate = false;
            TableParameters.IsActivate = true;
            TableParameters.IsEdit = true;
            TableParameters.IsInactivate = true;

            return View("Parameters", TableParameters);
        }

        [HttpGet]
        public async Task<ActionResult> Desactive(string id)
        {
            Parameter parameter = await parametersService.GetParameterById(id);

            return View("Actions/DesactiveParameter", parameter);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            Parameter parameter = await parametersService.GetParameterById(id);

            List<string> ParameterTypeList = await parametersService.GetParameterType();
            List<string> ParameterList = await parametersService.GetListParameter();


            ParameterCreateViewModel model = new ParameterCreateViewModel()
            {
                Id = parameter.Id,
                ParameterType = parameter.ParameterType,
                List = parameter.List,
                _Parameters = parameter._Parameters,
                Value = parameter.Value,
                Description = parameter.Description,
                State = parameter.State
            };

            model.ParameterTypeOption = ParameterTypeList;
            model.ListParameter = ParameterList;

            return View("Actions/EditParameter", model);
        }

        [HttpGet]
        public async Task<ActionResult> View(string id)
        {
            Parameter parameter = await parametersService.GetParameterById(id);

            return View("Actions/ViewParameter", parameter);
        }
    }
}
