using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Parameter;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.Parameters;
using ParameterControl.Services.Policies;
using ParameterControl.Services.Rows;
using System.Linq;
using modParameter = ParameterControl.Models.Parameter;


namespace ParameterControl.Controllers.Parameters
{
    public class ParametersController : Controller
    {
        public TableParametersViewModel TableParameters = new TableParametersViewModel();
        private readonly ILogger<HomeController> _logger;
        private readonly IParametersService parametersService; 
        private readonly Rows rows;
        private readonly AuthenticatedUser authenticatedUser;

        public ParametersController(
            ILogger<HomeController> logger,
            IParametersService parametersService,
            Rows rows,
            AuthenticatedUser authenticatedUser
        )
        {
            this._logger = logger;
            this.parametersService = parametersService;
            this.rows = rows;
            this.authenticatedUser = authenticatedUser;
        }

        [HttpGet]
        public async Task<ActionResult> Parameters()
        {
            List<modParameter.Parameter> parameters = await parametersService.GetParameters();

            TableParameters.Data = await parametersService.GetParametersFormat(parameters);

            TableParameters.Rows = rows.RowsParameters();

            TableParameters.Filter = true;
            TableParameters.IsCreate = true;
            TableParameters.IsActivate = true;
            TableParameters.IsEdit = true;
            TableParameters.IsInactivate = true;
            TableParameters.Filter = true;

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

            List<ParameterViewModel> parametersFilter = await parametersService.GetFilterParameters(filter);
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
        public async Task<ActionResult> Create()
        {

            List<SelectListItem> ParameterTypeOptionList = await parametersService.GetParameterType();
            List<SelectListItem> GetListParameterList = await GetParameters();

            ParameterCreateViewModel model = new ParameterCreateViewModel()
            {
                ParameterTypeOption = ParameterTypeOptionList,
                ListParameter = GetListParameterList
            };

            return View("Actions/CreateParameter", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Parameter request)
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
                    request.CreationDate = DateTime.Now;
                    request.UpdateDate = DateTime.Now;
                    _logger.LogInformation($"Inicia método ParametersController.Create {JsonConvert.SerializeObject(request)}");
                    return Ok(new { message = "Se creo el parametro de manera exitosa", state = "Success" });
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error en el método ParametersController.Create : {JsonConvert.SerializeObject(ex.Message)}");
                    return BadRequest(new { message = "Error al crear el parametro", state = "Error" });
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            Parameter parameter = await parametersService.GetParameterById(id);

            List<SelectListItem> ParameterTypeList = await parametersService.GetParameterType();
            List<SelectListItem> ParameterList = await GetParameters();


            ParameterCreateViewModel model = new ParameterCreateViewModel()
            {
                Id = parameter.Id,
                ParameterType = parameter.ParameterType,
                List = parameter.List,
                Code = parameter.Code,
                Value = parameter.Value,
                Description = parameter.Description,
                State = parameter.State,
                CreationDate = parameter.CreationDate
            };

            model.ParameterTypeOption = ParameterTypeList;
            model.ListParameter = ParameterList;

            return View("Actions/EditParameter", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Parameter request)
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
                    _logger.LogInformation($"Inicia método ParametersController.Edit {JsonConvert.SerializeObject(request)}");
                    return Ok(new { message = "Se actualizo el parametro de manera exitosa", state = "Success" });
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error en el método ParametersController.Edit : {JsonConvert.SerializeObject(ex.Message)}");
                    return BadRequest(new { message = "Error al actualizar el parametro", state = "Error" });
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> View(string id)
        {
            Parameter parameter = await parametersService.GetParameterById(id);

            return View("Actions/ViewParameter", parameter);
        }

        [HttpGet]
        public async Task<ActionResult> Active(string id)
        {
            modParameter.Parameter parameter = await parametersService.GetParameterById(id);

            return View("Actions/ActiveParameter", parameter);
        }

        [HttpPost]
        public async Task<ActionResult> ActiveParameter([FromBody] string id)
        {
            try
            {
                modParameter.Parameter request = await parametersService.GetParameterById(id);

                request.UserOwner = authenticatedUser.GetUserOwnerId();
                request.UpdateDate = DateTime.Now;
                request.State = true;
                _logger.LogInformation($"Inicia método ParametersController.Active {JsonConvert.SerializeObject(request)}");
                return Ok(new { message = "Se activo el parametro de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método ParametersController.Active : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al activar el parametro", state = "Error" });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Desactive(string id)
        {
            Parameter parameter = await parametersService.GetParameterById(id);

            return View("Actions/DesactiveParameter", parameter);
        }

        [HttpPost]
        public async Task<ActionResult> DesactiveParameter([FromBody] string id)
        {
            try
            {
                modParameter.Parameter request = await parametersService.GetParameterById(id);

                request.UserOwner = authenticatedUser.GetUserOwnerId();
                request.UpdateDate = DateTime.Now;
                request.State = false;
                _logger.LogInformation($"Inicia método ParametersController.Deactive {JsonConvert.SerializeObject(request)}");
                return Ok(new { message = "Se desactivo el parametro de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método ParametersController.Deactive : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al desactivar el parametro", state = "Error" });
            }
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

        public async Task<List<SelectListItem>> GetParameters()
        {
            List<modParameter.Parameter> parameters = await parametersService.GetListParameter();
            return parameters.Select(policy => new SelectListItem(policy.Code, policy.Id.ToString())).ToList();
        }
    }
}
