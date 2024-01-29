using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ParameterControl.Models.Conciliation;
using ParameterControl.Models.Filter;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.Conciliations;
using ParameterControl.Services.Policies;
using ParameterControl.Services.Rows;
using modConciliation = ParameterControl.Models.Conciliation;
using modPolicy = ParameterControl.Models.Policy;

namespace ParameterControl.Controllers.Conciliations
{
    public class ConciliationsController : Controller
    {
        public TableConciliationViewModel TableConciliations = new TableConciliationViewModel();
        private readonly ILogger<HomeController> _logger;
        private readonly IConciliationsServices conciliationsServices;
        private readonly Rows rows;
        private readonly IPoliciesServices policiesServices;
        private readonly AuthenticatedUser authenticatedUser;

        public ConciliationsController(
            ILogger<HomeController> logger,
            IConciliationsServices conciliationsServices,
            Rows rows,
            IPoliciesServices policiesServices,
            AuthenticatedUser authenticatedUser
        )
        {
            this._logger = logger;
            this.conciliationsServices = conciliationsServices;
            this.rows = rows;
            this.policiesServices = policiesServices;
            this.authenticatedUser = authenticatedUser;
        }

        [HttpGet]
        public async Task<ActionResult> Conciliations()
        {

            List<modConciliation.Conciliation> Conciliations = await conciliationsServices.GetConciliations();

            TableConciliations.Data = await conciliationsServices.GetConciliationsFormat(Conciliations);

            TableConciliations.Rows = rows.RowsConciliations();

            TableConciliations.IsCreate = true;
            TableConciliations.IsActivate = true;
            TableConciliations.IsEdit = true;
            TableConciliations.IsInactivate = true;
            TableConciliations.Filter = true;

            ViewBag.ApplyFilter = false;

            return View("Conciliations", TableConciliations);
        }

        [HttpGet]
        public async Task<ActionResult> ConciliationsFilter(string filterColunm = "", string filterValue = "")
        {
            _logger.LogInformation(filterColunm);
            _logger.LogInformation(filterValue);

            if (filterColunm == null || filterColunm == "" || filterValue == null || filterValue == "")
            {
                return RedirectToAction("Conciliations");
            }

            FilterViewModel filter = new FilterViewModel()
            {
                ColumValue = filterColunm,
                ValueFilter = filterValue
            };

            List<ConciliationViewModel> conciliationsFilter = await conciliationsServices.GetFilterConciliations(filter);

            TableConciliations.Data = conciliationsFilter;

            TableConciliations.Rows = rows.RowsConciliations();

            TableConciliations.IsCreate = true;
            TableConciliations.IsActivate = true;
            TableConciliations.IsEdit = true;
            TableConciliations.IsInactivate = true;
            TableConciliations.Filter = true;

            ViewBag.ApplyFilter = true;

            return View("ConciliationsFilter", TableConciliations);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {

            List<SelectListItem> PoliciesOptionsList = await GetPolicies();
            List<SelectListItem> RequiredOptionList = await conciliationsServices.GetRequired();

            ConciliationCreateViewModel model = new ConciliationCreateViewModel()
            {
                PoliciesOption = PoliciesOptionsList,
                RequiredOption = RequiredOptionList
            };

            return View("Actions/CreateConciliation", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] modConciliation.Conciliation request)
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
                    return Ok(new { message = "Se creo la politica de manera exitosa", state = "Success" });
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error en el método ConciliationController.Create : {JsonConvert.SerializeObject(ex.Message)}");
                    return BadRequest(new { message = "Error al crear la conciliación", state = "Error" });
                }
            }

        }

        [HttpGet]
        public async Task<ActionResult> Edit(int code)
        {
            modConciliation.Conciliation conciliation = await conciliationsServices.GetConciliationsByCode(code);

            List<SelectListItem> PoliciesOptionsList = await GetPolicies();
            List<SelectListItem> RequiredOptionsList = await conciliationsServices.GetRequired();

            ConciliationCreateViewModel model = await conciliationsServices.GetConciliationFormatCreate(conciliation);

            Console.WriteLine(model.CreationDate);
            //ConciliationCreateViewModel model = new ConciliationCreateViewModel()
            //{
            //    Code = conciliation.Code,
            //    Name = conciliation.Name,
            //    Description = conciliation.Description,
            //    Conciliation_ = conciliation.Conciliation_,
            //    Package = conciliation.Package,
            //    Email = conciliation.Email,
            //    Destination = conciliation.Destination,
            //    Policies = conciliation.Policies,
            //    Required = conciliation.Required,
            //    RequiredFormat = conciliation.Required ? "Si" : "No",
            //    State = conciliation.State,
            //    CodeFormat = "CO_" + conciliation.Code,
            //    CreationDate = conciliation.CreationDate,
            //    UpdateDate = conciliation.UpdateDate
            //};
            model.PoliciesOption = PoliciesOptionsList;
            model.RequiredOption = RequiredOptionsList;

            return View("Actions/EditConciliation", model);
        }


        [HttpPost]
        public async Task<ActionResult> Edit([FromBody] modConciliation.Conciliation request)
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
                    request.UpdateDate = DateTime.Now;
                    _logger.LogInformation($"Inicia método ConciliationsController.Edit {JsonConvert.SerializeObject(request)}");
                    return Ok(new { message = "Se actualizo la conciliación de manera exitosa", state = "Success" });
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error en el método ConciliationsController.Edit : {JsonConvert.SerializeObject(ex.Message)}");
                    return BadRequest(new { message = "Error al actualizar la conciliación", state = "Error" });
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> View(int code)
        {
            Conciliation conciliation = await conciliationsServices.GetConciliationsByCode(code);

            return View("Actions/ViewConciliation", conciliation);
        }

        [HttpGet]
        public async Task<ActionResult> Active(int code)
        {
            modConciliation.Conciliation conciliation = await conciliationsServices.GetConciliationsByCode(code);

            return View("Actions/ActiveConciliation", conciliation);
        }

        [HttpPost]
        public async Task<ActionResult> ActiveConciliation([FromBody] int code)
        {
            try
            {
                Conciliation request = await conciliationsServices.GetConciliationsByCode(code);
                request.UserOwner = authenticatedUser.GetUserOwnerId();
                request.UpdateDate = DateTime.Now;
                request.State = true;
                _logger.LogInformation($"Inicia método ConciliationsController.Active {JsonConvert.SerializeObject(request)}");
                return Ok(new { message = "Se activo la conciliación de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método ConciliationsController.Active : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al activar la conciliación", state = "Error" });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Desactive(int code)
        {
            modConciliation.Conciliation conciliation = await conciliationsServices.GetConciliationsByCode(code);

            return View("Actions/DesactiveConciliation", conciliation);
        }

        [HttpPost]
        public async Task<ActionResult> DesactiveConciliation([FromBody] int code)
        {
            try
            {
                Conciliation request = await conciliationsServices.GetConciliationsByCode(code);
                request.UserOwner = authenticatedUser.GetUserOwnerId();
                request.UpdateDate = DateTime.Now;
                request.State = false;
                _logger.LogInformation($"Inicia método ConciliationsController.Desactive {JsonConvert.SerializeObject(request)}");
                return Ok(new { message = "Se desactivo la conciliación de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método PoliciesController.Desactive : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al desactivar la conciliación", state = "Error" });
            }
        }

        [HttpGet]
        public ActionResult Filter()
        {
            FilterViewModel model = new FilterViewModel()
            {
                Rows = rows.RowsConciliations()

            };

            return View("Actions/Filter", model);
        }

        [HttpPost]
        public async Task<ActionResult> FilterConciliations(FilterViewModel filter)
        {

            Console.WriteLine(filter.ColumValue);

            Console.WriteLine(filter.ValueFilter);

            return RedirectToAction("ConciliationsFilter", new
            {
                filterColunm = filter.ColumValue,
                filterValue = filter.ValueFilter
            });
        }

        public async Task<List<SelectListItem>> GetPolicies()
        {
            List<modPolicy.Policy> policies = await policiesServices.GetPolicies();
            return policies.Select(policy => new SelectListItem(policy.Name, policy.Code.ToString())).ToList();
        }
    }
}
