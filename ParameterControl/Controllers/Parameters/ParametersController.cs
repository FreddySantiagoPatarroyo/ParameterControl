using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ParameterControl.Conciliation.Impl;
using ParameterControl.Models.Conciliation;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Models.Parameter;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.Conciliations;
using ParameterControl.Services.Parameters;
using ParameterControl.Services.Rows;
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
        public async Task<ActionResult> Parameters(PaginationViewModel paginationViewModel)
        {
            List<modParameter.Parameter> parameters = await parametersService.GetParametersPagination(paginationViewModel);
            int TotalParameters = await parametersService.CountParameters();

            TableParameters.Data = await parametersService.GetParametersFormat(parameters);

            TableParameters.Rows = rows.RowsParameters();

            TableParameters.Filter = true;
            TableParameters.IsCreate = true;
            TableParameters.IsActivate = true;
            TableParameters.IsEdit = true;
            TableParameters.IsView = true;
            TableParameters.IsInactivate = true;
            TableParameters.Filter = true;

            var resultViemModel = new PaginationResult<TableParametersViewModel>()
            {
                Elements = TableParameters,
                Page = paginationViewModel.Page,
                RecordsPage = paginationViewModel.RecordsPage,
                TotalRecords = TotalParameters,
                BaseUrl = Url.Action() + "?"
            };

            ViewBag.ApplyFilter = false;

            return View("Parameters", resultViemModel);
        }

        [HttpGet]
        public async Task<ActionResult> ParametersFilter(PaginationViewModel paginationViewModel, string filterColunm = "", string filterValue = "", string typeRow = "")
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
            TableParameters.IsCreate = true;
            TableParameters.IsActivate = true;
            TableParameters.IsEdit = true;
            TableParameters.IsView = true;
            TableParameters.IsInactivate = true;

            var resultViemModel = new PaginationResult<TableParametersViewModel>()
            {
                Elements = TableParameters,
                Page = paginationViewModel.Page,
                RecordsPage = paginationViewModel.RecordsPage,
                TotalRecords = TotalParameters,
                BaseUrl = Url.Action() + "?filterColunm=" + filterColunm + "&filterValue=" + filterValue + "&typeRow=" + typeRow + "&"
            };

            ViewBag.ApplyFilter = true;

            return View("ParametersFilter", resultViemModel);
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
        public async Task<IActionResult> Create([FromBody] modParameter.Parameter request)
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
                    _logger.LogInformation($"Inicia método ParametersController.Create {JsonConvert.SerializeObject(request)}");
                    var responseIn = await parametersService.InsertParameter(request);
                    _logger.LogInformation($"Finaliza método ParametersController.Create {responseIn}");
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
            modParameter.Parameter parameter = await parametersService.GetParameterByCode(code);

            List<SelectListItem> ParameterTypeList = await parametersService.GetParameterType();
            List<SelectListItem> ParameterList = await GetParameters();


            ParameterCreateViewModel model = await parametersService.GetParameterFormatCreate(parameter);

            model.ParameterTypeOption = ParameterTypeList;
            model.ListParameter = ParameterList;

            return View("Actions/EditParameter", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] modParameter.Parameter request)
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
                    _logger.LogInformation($"Inicia método ParametersController.Edit {JsonConvert.SerializeObject(request)}");
                    var responseIn = await parametersService.UpdateParameter(request);
                    _logger.LogInformation($"Finaliza método ParametersController.Edit {responseIn}");
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
            modParameter.Parameter parameter = await parametersService.GetParameterByCode(code);

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

                _logger.LogInformation($"Inicia método ParametersController.Active {JsonConvert.SerializeObject(request)}");
                var responseIn = await parametersService.ActiveParameter(request);
                _logger.LogInformation($"Finaliza método ParametersController.Active {responseIn}");
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
            modParameter.Parameter parameter = await parametersService.GetParameterByCode(code);

            return View("Actions/DesactiveParameter", parameter);
        }

        [HttpPost]
        public async Task<ActionResult> DesactiveParameter([FromBody] int code)
        {
            try
            {
                modParameter.Parameter request = await parametersService.GetParameterByCode(code);

                _logger.LogInformation($"Inicia método ParametersController.Deactive {JsonConvert.SerializeObject(request)}");
                var responseIn = await parametersService.DesactiveParameter(request);
                _logger.LogInformation($"Finaliza método ParametersController.Deactive {responseIn}");
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

            return parameters.Select(parameter => new SelectListItem(parameter.Parameter_, parameter.Code.ToString())).ToList();
        }
    }
}
