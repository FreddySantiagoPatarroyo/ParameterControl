using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ParameterControl.LoadControl.Entities;
using ParameterControl.Models.Conciliation;
using ParameterControl.Models.CrossConnection;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Models.Policy;
using ParameterControl.Models.Scenery;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.Conciliations;
using ParameterControl.Services.Policies;
using ParameterControl.Services.Rows;
using ParameterControl.Stage.Entities;
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
        public async Task<ActionResult> Conciliations(PaginationViewModel paginationViewModel)
        {

            List<modConciliation.Conciliation> Conciliations = await conciliationsServices.GetConciliationsPagination(paginationViewModel);
            int TotalConciliations = await conciliationsServices.CountConciliations();

            TableConciliations.Data = await conciliationsServices.GetConciliationsFormat(Conciliations);

            TableConciliations.Rows = rows.RowsConciliations();

            TableConciliations.IsCreate = true;
            TableConciliations.IsActivate = true;
            TableConciliations.IsEdit = true;
            TableConciliations.IsInactivate = true;
            TableConciliations.Filter = true;

            var resultViemModel = new PaginationResult<TableConciliationViewModel>()
            {
                Elements = TableConciliations,
                Page = paginationViewModel.Page,
                RecordsPage = paginationViewModel.RecordsPage,
                TotalRecords = TotalConciliations,
                BaseUrl = Url.Action() + "?"
            };

            ViewBag.ApplyFilter = false;

            return View("Conciliations", resultViemModel);
        }

        [HttpGet]
        public async Task<ActionResult> ConciliationsFilter(PaginationViewModel paginationViewModel, string filterColunm = "", string filterValue = "", string typeRow = "")
        {
  
            if (filterColunm == null || filterColunm == "" || filterValue == null || filterValue == "")
            {
                return RedirectToAction("Conciliations");
            }

            FilterViewModel filter = new FilterViewModel()
            {
                ColumValue = filterColunm,
                ValueFilter = filterValue,
                TypeRow = typeRow
            };

            List<ConciliationViewModel> conciliationsFilter = await conciliationsServices.GetFilterConciliations(filter);
            int TotalConciliations = conciliationsFilter.Count();

            TableConciliations.Data = conciliationsServices.GetFilterPagination(conciliationsFilter, paginationViewModel, TotalConciliations);

            TableConciliations.Rows = rows.RowsConciliations();

            TableConciliations.IsCreate = true;
            TableConciliations.IsActivate = true;
            TableConciliations.IsEdit = true;
            TableConciliations.IsInactivate = true;
            TableConciliations.Filter = true;

            var resultViemModel = new PaginationResult<TableConciliationViewModel>()
            {
                Elements = TableConciliations,
                Page = paginationViewModel.Page,
                RecordsPage = paginationViewModel.RecordsPage,
                TotalRecords = TotalConciliations,
                BaseUrl = Url.Action() + "?filterColunm=" + filterColunm + "&filterValue=" + filterValue + "&typeRow=" + typeRow + "&"
            };

            ViewBag.ApplyFilter = true;

            return View("ConciliationsFilter", resultViemModel);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {

            List<SelectListItem> PoliciesOptionsList = await GetPolicies();
            List<SelectListItem> RequiredOptionList = await conciliationsServices.GetRequired();
            List<SelectListItem> OperationTypeOptionsList = await conciliationsServices.GetOperationsType();
            List<SelectListItem> EmailsOptionsList = await conciliationsServices.GetEmailUsers();

            ConciliationCreateViewModel model = new ConciliationCreateViewModel()
            {
                OperationTypeOptions = OperationTypeOptionsList,
                PoliciesOption = PoliciesOptionsList,
                RequiredOption = RequiredOptionList,
                Emails = EmailsOptionsList
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
                    _logger.LogInformation($"Inicia método ConciliationController.Create {JsonConvert.SerializeObject(request)}");
                    var responseIn = await conciliationsServices.InsertConciliation(request);
                    _logger.LogInformation($"Finaliza método ConciliationController.Create {responseIn}");
                    return Ok(new { message = "Se creo la conciliación de manera exitosa", state = "Success" });
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
            List<SelectListItem> OperationTypeOptionsList = await conciliationsServices.GetOperationsType();
            List<SelectListItem> EmailsOptionsList = await conciliationsServices.GetEmailUsers();

            ConciliationCreateViewModel model = await conciliationsServices.GetConciliationFormatCreate(conciliation);

            Console.WriteLine(model.CreationDate);
            
            model.OperationTypeOptions = OperationTypeOptionsList;
            model.PoliciesOption = PoliciesOptionsList;
            model.RequiredOption = RequiredOptionsList;
            model.Emails = EmailsOptionsList;

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
                    _logger.LogInformation($"Inicia método ConciliationsController.Edit {JsonConvert.SerializeObject(request)}");
                    var responseIn = await conciliationsServices.UpdateConciliation(request);
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
            modConciliation.Conciliation conciliation = await conciliationsServices.GetConciliationsByCode(code);

            ConciliationViewModel model = await conciliationsServices.GetConciliationFormat(conciliation);

            return View("Actions/ViewConciliation", model);
        }

        [HttpGet]
        public async Task<ActionResult> ViewPolicy(int code)
        {
            modPolicy.Policy policy = await policiesServices.GetPolicyByCode(code);

            PolicyViewModel model = await policiesServices.GetPolicyFormat(policy);

            return View("Actions/ViewPolicyConciliations", model);
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
                modConciliation.Conciliation request = await conciliationsServices.GetConciliationsByCode(code);
                var responseIn = await conciliationsServices.ActiveConciliation(request);
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
            if (await conciliationsServices.ValidateScenariosActivos(code))
            {
                _logger.LogError($"Error en el método PoliciesController.Desactive");
                return BadRequest(new { message = "La conciliacion cuenta con esenarios activos, desactive los escenarios para desactivar la conciliacion", state = "Error" });
            }
            else
            {
                try
                {
                    modConciliation.Conciliation request = await conciliationsServices.GetConciliationsByCode(code);
                    var responseIn = await conciliationsServices.DesactiveConciliation(request);
                    _logger.LogInformation($"Inicia método ConciliationsController.Desactive {JsonConvert.SerializeObject(request)}");
                    return Ok(new { message = "Se desactivo la conciliación de manera exitosa", state = "Success" });
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error en el método PoliciesController.Desactive : {JsonConvert.SerializeObject(ex.Message)}");
                    return BadRequest(new { message = "Error al desactivar la conciliación", state = "Error" });
                }
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

            if (filter.TypeRow == "Select")
            {
                filter.ValueFilter = filter.ValueFilterOptions;
            }
            else if (filter.TypeRow == "Date")
            {
                filter.ValueFilter = filter.ValueFilterDate.ToString("dd/MM/yyyy");
            }

            return RedirectToAction("ConciliationsFilter", new
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
                case "RequiredFormat":
                    filter.Options = await conciliationsServices.GetRequired();
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

        public async Task<List<SelectListItem>> GetPolicies()
        {
            List<modPolicy.Policy> policies = await conciliationsServices.GetPolicies();
            return policies.Select(policy => new SelectListItem(policy.Name, policy.Code.ToString())).ToList();
        }
    }
}
