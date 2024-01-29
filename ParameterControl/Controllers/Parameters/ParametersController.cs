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
        public async Task<ActionResult> ParametersFilter(string filterColunm = "", string filterValue = "", string typeRow = "")
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
        public async Task<ActionResult> Edit(int code)
        {
            Parameter parameter = await parametersService.GetParameterByCode(code);

            List<SelectListItem> ParameterTypeList = await parametersService.GetParameterType();
            List<SelectListItem> ParameterList = await GetParameters();


            ParameterCreateViewModel model = await parametersService.GetParameterFormatCreate(parameter);

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
        public async Task<ActionResult> View(int code)
        {
            Parameter parameter = await parametersService.GetParameterByCode(code);

            ParameterViewModel model = await parametersService.GetParameterFormat(parameter);

            return View("Actions/ViewParameter", model);
        }

        [HttpGet]
        public async Task<ActionResult> Active(int code)
        {
            modParameter.Parameter parameter = await parametersService.GetParameterByCode(code);

            return View("Actions/ActiveParameter", parameter);
        }

        [HttpPost]
        public async Task<ActionResult> ActiveParameter([FromBody] int code)
        {
            try
            {
                modParameter.Parameter request = await parametersService.GetParameterByCode(code);

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
        public async Task<ActionResult> Desactive(int code)
        {
            Parameter parameter = await parametersService.GetParameterByCode(code);

            return View("Actions/DesactiveParameter", parameter);
        }

        [HttpPost]
        public async Task<ActionResult> DesactiveParameter([FromBody] int code)
        {
            try
            {
                modParameter.Parameter request = await parametersService.GetParameterByCode(code);

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

        public async Task<List<SelectListItem>> GetParameters()
        {
            List<modParameter.Parameter> parameters = await parametersService.GetListParameter();

            List<ParameterViewModel> parametersModel = await parametersService.GetParametersFormat(parameters);

            return parametersModel.Select(parameter => new SelectListItem(parameter.ParameterFormat, parameter.Code.ToString())).ToList();
        }
    }
}
