using Microsoft.AspNetCore.Mvc;
using ParameterControl.Models.Conciliation;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Parameter;
using ParameterControl.Models.Policy;
using ParameterControl.Services.Conciliations;
using ParameterControl.Services.Parameters;
using ParameterControl.Services.Policies;
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
        public async Task<ActionResult> Parameters()
        {

            TableParameters.Data = await parametersService.GetParameters();

            TableParameters.Rows = rows.RowsParameters();

            TableParameters.Filter = true;
            TableParameters.IsCreate = true;
            TableParameters.IsActivate = true;
            TableParameters.IsEdit = true;
            TableParameters.IsInactivate = true;

            ViewBag.ApplyFilter = false;

            return View("Parameters", TableParameters);
        }

        [HttpGet]
        public async Task<ActionResult> ParametersFilter(string filterColunm = "", string filterValue = "")
        {
            _logger.LogInformation(filterColunm);
            _logger.LogInformation(filterValue);

            if (filterColunm == null || filterColunm == "" || filterValue == null || filterValue == "")
            {
                return RedirectToAction("Parameters");
            }

            FilterViewModel filter = new FilterViewModel()
            {
                ColumValue = filterColunm,
                ValueFilter = filterValue
            };

            List<Parameter> parametersFilter = await parametersService.GetFilterParameters(filter);
            TableParameters.Data = parametersFilter;

            TableParameters.Rows = rows.RowsParameters();

            TableParameters.Filter = true;
            TableParameters.IsCreate = true;
            TableParameters.IsActivate = true;
            TableParameters.IsEdit = true;
            TableParameters.IsInactivate = true;

            ViewBag.ApplyFilter = true;

            return View("ParametersFilter", TableParameters);
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
                Parameters_ = parameter.Parameters_,
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
        [HttpGet]
        public async Task<ActionResult> Create()
        {

            List<string> ParameterTypeOptionList = await parametersService.GetParameterType();
            List<string> GetListParameterList = await parametersService.GetListParameter();

            ParameterCreateViewModel model = new ParameterCreateViewModel()
            {
                ParameterTypeOption = ParameterTypeOptionList,
                ListParameter = GetListParameterList
            };

            return View("Actions/CreateParameter", model);
        }

        [HttpGet]
        public ActionResult Filter()
        {
            FilterViewModel model = new FilterViewModel()
            {
                Rows = rows.RowsParameters()

            };

            return View("Actions/Filter", model);
        }

        [HttpPost]
        public async Task<ActionResult> FilterParameters(FilterViewModel filter)
        {

            Console.WriteLine(filter.ColumValue);

            Console.WriteLine(filter.ValueFilter);

            return RedirectToAction("ParametersFilter", new
            {
                filterColunm = filter.ColumValue,
                filterValue = filter.ValueFilter
            });
        }
    }
}
