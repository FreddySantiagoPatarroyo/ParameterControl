using Microsoft.AspNetCore.Mvc;
using ParameterControl.Models.Indicator;
using ParameterControl.Models.Filter;
using ParameterControl.Services.Indicators;
using ParameterControl.Services.Rows;
using modIndicator = ParameterControl.Models.Indicator;
using ParameterControl.Services.Results;
using Newtonsoft.Json;
using ParameterControl.Services.Authenticated;
using ParameterControl.Models.User;


namespace ParameterControl.Controllers.Indicators
{
    public class IndicatorsController: Controller
    {
        public TableIndicatorViewModel TableIndicators = new TableIndicatorViewModel();
        private readonly ILogger<HomeController> _logger;
        private readonly IIndicatorsService indicatorsService;
        private readonly Rows rows;
        private readonly AuthenticatedUser authenticatedUser;

        public IndicatorsController(
            ILogger<HomeController> logger,
            IIndicatorsService indicatorsService,
            Rows rows,
            AuthenticatedUser authenticatedUser
        )
        {
            this._logger = logger;
            this.indicatorsService = indicatorsService;
            this.rows = rows;
            this.authenticatedUser = authenticatedUser;
        }

        [HttpGet]
        public async Task<ActionResult> Indicators()
        {
            List<Indicator> indicators = await indicatorsService.GetIndicators();

            TableIndicators.Data = await indicatorsService.GetindicatorsFormat(indicators);

            TableIndicators.Rows = rows.RowsIndicators();

            TableIndicators.IsCreate = true;
            TableIndicators.IsActivate = true;
            TableIndicators.IsEdit = true;
            TableIndicators.IsInactivate = true;

            ViewBag.ApplyFilter = false;

            return View("Indicators", TableIndicators);
        }


        [HttpGet]
        public async Task<ActionResult> IndicatorsFilter(string filterColunm = "", string filterValue = "")
        {
            _logger.LogInformation(filterColunm);
            _logger.LogInformation(filterValue);

            if (filterColunm == null || filterColunm == "" || filterValue == null || filterValue == "")
            {
                return RedirectToAction("Indicators");
            }

            FilterViewModel filter = new FilterViewModel()
            {
                ColumValue = filterColunm,
                ValueFilter = filterValue
            };

            List<IndicatorViewModel> indicatorsFilter = await indicatorsService.GetFilterIndicators(filter);

            TableIndicators.Data = indicatorsFilter;

            TableIndicators.Rows = rows.RowsIndicators();

            TableIndicators.IsCreate = true;
            TableIndicators.IsActivate = true;
            TableIndicators.IsEdit = true;
            TableIndicators.IsInactivate = true;

            ViewBag.ApplyFilter = true;

            return View("IndicatorsFilter", TableIndicators);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {

            IndicatorCreationViewModel model = new IndicatorCreationViewModel();

            return View("Actions/CreateIndicators", model);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Indicator request)
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
                    _logger.LogInformation($"Inicia método IndicatorsController.Create {JsonConvert.SerializeObject(request)}");
                    return Ok(new { message = "Se creo el indicador de manera exitosa", state = "Success" });
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error en el método IndicatorsController.Create : {JsonConvert.SerializeObject(ex.Message)}");
                    return BadRequest(new { message = "Error al crear el indicador", state = "Error" });
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            Indicator indicator = await indicatorsService.GetIndicatorsById(id);

            IndicatorCreationViewModel model = new IndicatorCreationViewModel()
            {
                Id = indicator.Id,
                Name = indicator.Name,
                Description = indicator.Description,
                Formula = indicator.Formula,
                Scenery = indicator.Scenery,
                Parameter = indicator.Parameter,
                State = indicator.State,
                CreationDate = indicator.CreationDate
            };

            return View("Actions/EditIndicators", model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromBody] Indicator request)
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
                    _logger.LogInformation($"Inicia método IndicatorsController.Edit {JsonConvert.SerializeObject(request)}");
                    return Ok(new { message = "Se actualizo el indicador de manera exitosa", state = "Success" });
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error en el método IndicatorsController.Edit : {JsonConvert.SerializeObject(ex.Message)}");
                    return BadRequest(new { message = "Error al actualizar el indicador", state = "Error" });
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> View(string id)
        {
            Indicator indicator = await indicatorsService.GetIndicatorsById(id);

            return View("Actions/ViewIndicators", indicator);
        }

        [HttpGet]
        public async Task<ActionResult> Active(string id)
        {
            modIndicator.Indicator indicator = await indicatorsService.GetIndicatorsById(id);

            return View("Actions/ActiveIndicators", indicator);
        }

        [HttpPost]
        public async Task<ActionResult> ActiveIndicator([FromBody] string id)
        {
            try
            {
                modIndicator.Indicator request = await indicatorsService.GetIndicatorsById(id);
                request.UserOwner = authenticatedUser.GetUserOwnerId();
                request.UpdateDate = DateTime.Now;
                request.State = true;
                _logger.LogInformation($"Inicia método IndicatorsController.Active {JsonConvert.SerializeObject(request)}");
                return Ok(new { message = "Se activo el indicador de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método IndicatorsController.Active : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al activar el indicador", state = "Error" });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Desactive(string id)
        {
            Indicator indicator = await indicatorsService.GetIndicatorsById(id);

            return View("Actions/DesactiveIndicators", indicator);
        }

        [HttpPost]
        public async Task<ActionResult> DesactiveIndicator([FromBody] string id)
        {
            try
            {
                modIndicator.Indicator request = await indicatorsService.GetIndicatorsById(id);
                request.UserOwner = authenticatedUser.GetUserOwnerId();
                request.UpdateDate = DateTime.Now;
                request.State = false;
                _logger.LogInformation($"Inicia método IndicatorsController.Desactive {JsonConvert.SerializeObject(request)}");
                return Ok(new { message = "Se desactivo el indicador de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método IndicatorsController.Desactive : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al desactivar el indicador", state = "Error" });
            }
        }

        [HttpGet]
        public ActionResult Filter()
        {
            FilterViewModel model = new FilterViewModel()
            {
                Rows = rows.RowsIndicators()

            };

            return View("Actions/Filter", model);
        }

        [HttpPost]
        public async Task<ActionResult> FilterIndicators(FilterViewModel filter)
        {

            Console.WriteLine(filter.ColumValue);

            Console.WriteLine(filter.ValueFilter);

            return RedirectToAction("IndicatorsFilter", new
            {
                filterColunm = filter.ColumValue,
                filterValue = filter.ValueFilter
            });
        }

    }
}
