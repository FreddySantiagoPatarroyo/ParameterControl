using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Models.Policy;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.Policies;
using ParameterControl.Services.Rows;
using modPolicy = ParameterControl.Models.Policy;


namespace ParameterControl.Controllers.Policies
{
    public class PoliciesController : Controller
    {
        public TablePoliciesViewModel TablePolicies = new TablePoliciesViewModel();
        private readonly ILogger<HomeController> _logger;
        private readonly IPoliciesServices policiesServices;
        private readonly Rows rows;
        private readonly AuthenticatedUser authenticatedUser;

        public PoliciesController(
            ILogger<HomeController> logger,
            IPoliciesServices policiesServices,
            Rows rows,
            AuthenticatedUser authenticatedUser
        )
        {
            this._logger = logger;
            this.policiesServices = policiesServices;
            this.rows = rows;
            this.authenticatedUser = authenticatedUser;
        }

        [HttpGet]
        public async Task<ActionResult> Policies(PaginationViewModel paginationViewModel)
        {
            List<modPolicy.Policy> policies = await policiesServices.GetPolicies();

            TablePolicies.Data = await policiesServices.GetPolicesFormat(policies);

            TablePolicies.Rows = rows.RowsPolicies();

            TablePolicies.Filter = true;
            TablePolicies.IsCreate = true;
            TablePolicies.IsActivate = true;
            TablePolicies.IsEdit = true;
            TablePolicies.IsInactivate = true;

            ViewBag.ApplyFilter = false;

            return View("Policies", TablePolicies);
        }

        public async Task<ActionResult> PoliciesFilter(string filterColunm = "", string filterValue = "", string typeRow = "")
        {

            if (filterColunm == null || filterColunm == "" || filterValue == null || filterValue == "")
            {
                return RedirectToAction("Policies");
            }

            FilterViewModel filter = new FilterViewModel()
            {
                ColumValue = filterColunm,
                ValueFilter = filterValue,
                TypeRow = typeRow
            };

            List<PolicyViewModel> policiesFilter = await policiesServices.GetFilterPolicies(filter);
            TablePolicies.Data = policiesFilter;

            TablePolicies.Rows = rows.RowsPolicies();

            TablePolicies.Filter = true;
            TablePolicies.IsCreate = true;
            TablePolicies.IsActivate = true;
            TablePolicies.IsEdit = true;
            TablePolicies.IsInactivate = true;

            ViewBag.ApplyFilter = true;

            return View("PoliciesFilter", TablePolicies);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {

            List<SelectListItem> OperationTypeOptionsList = await policiesServices.GetOperationsType();

            PolicyCreateViewModel model = new PolicyCreateViewModel()
            {
                OperationTypeOptions = OperationTypeOptionsList
            };

            return View("Actions/CreatePolicy", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] modPolicy.Policy request)
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
                    _logger.LogInformation($"Inicia método PoliciesController.Create {JsonConvert.SerializeObject(request)}");
                    var responseIn = await policiesServices.InsertPolicy(request);
                    _logger.LogInformation($"Finaliza método PoliciesController.Create {responseIn}");
                    return Ok(new { message = "Se creo la politica de manera exitosa", state = "Success" });
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error en el método PoliciesController.Create : {JsonConvert.SerializeObject(ex.Message)}");
                    return BadRequest(new { message = "Error al crear la politica", state = "Error" });
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int code)
        {
            modPolicy.Policy policy = await policiesServices.GetPolicyByCode(code);

            PolicyCreateViewModel model = await policiesServices.GetPolicyFormatCreate(policy);

            return View("Actions/EditPolicy", model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromBody] modPolicy.Policy request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Error en el modelo : {JsonConvert.SerializeObject(request)}");
                var responseIn = await policiesServices.InsertPolicy(request);
                return BadRequest(new { message = "Error en la informacion enviada", state = "Error" });
            }
            else
            {
                try
                {
                    request.UserOwner = authenticatedUser.GetUserOwnerId();
                    request.UpdateDate = DateTime.Now;
                    _logger.LogInformation($"Inicia método PoliciesController.Edit {JsonConvert.SerializeObject(request)}");
                    return Ok(new { message = "Se actualizo la politica de manera exitosa", state = "Success" });
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error en el método PoliciesController.Edit : {JsonConvert.SerializeObject(ex.Message)}");
                    return BadRequest(new { message = "Error al actualizar la politica", state = "Error" });
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> View(int code)
        {
            modPolicy.Policy policy = await policiesServices.GetPolicyByCode(code);

            PolicyViewModel model = await policiesServices.GetPolicyFormat(policy);

            return View("Actions/ViewPolicy", model);
        }

        [HttpGet]
        public async Task<ActionResult> Active(int code)
        {
            modPolicy.Policy policy = await policiesServices.GetPolicyByCode(code);

            return View("Actions/ActivePolicy", policy);
        }

        [HttpPost]
        public async Task<ActionResult> ActivePolicy([FromBody] int code)
        {
            try
            {
                modPolicy.Policy request = await policiesServices.GetPolicyByCode(code);
                request.UserOwner = authenticatedUser.GetUserOwnerId();
                request.UpdateDate = DateTime.Now;
                request.State = true;
                _logger.LogInformation($"Inicia método PoliciesController.Active {JsonConvert.SerializeObject(request)}");
                return Ok(new { message = "Se activo la politica de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método PoliciesController.Active : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al activar la politica", state = "Error" });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Desactive(int code)
        {
            modPolicy.Policy policy = await policiesServices.GetPolicyByCode(code);

            return View("Actions/DesactivePolicy", policy);
        }

        [HttpPost]
        public async Task<ActionResult> DesactivePolicy([FromBody] int code)
        {
            try
            {
                modPolicy.Policy request = await policiesServices.GetPolicyByCode(code);
                request.UserOwner = authenticatedUser.GetUserOwnerId();
                request.UpdateDate = DateTime.Now;
                request.State = true;
                _logger.LogInformation($"Inicia método PoliciesController.Desactive {JsonConvert.SerializeObject(request)}");
                return Ok(new { message = "Se desactivo la politica de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método PoliciesController.Desactive : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al desactivar la politica", state = "Error" });
            }
        }

        [HttpGet]
        public ActionResult Filter()
        {
            FilterViewModel model = new FilterViewModel()
            {
                Rows = rows.RowsPolicies()

            };

            return View("Actions/Filter", model);
        }

        [HttpPost]
        public async Task<ActionResult> FilterPolicies(FilterViewModel filter)
        {
            if(filter.TypeRow == "Select")
            {
                filter.ValueFilter = filter.ValueFilterOptions;
            }else if(filter.TypeRow == "Date")
            {
                filter.ValueFilter = filter.ValueFilterDate.ToString("dd/MM/yyyy");
            }

            return RedirectToAction("PoliciesFilter", new
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
                case "OperationType":
                    filter.Options = await policiesServices.GetOperationsType();
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

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] PaginationViewModel pagination)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Error en el modelo : {JsonConvert.SerializeObject(pagination)}");
                return BadRequest(new { message = "Error en la consulta enviada", state = "Error" });
            }
            else
            {
                try
                {
                    _logger.LogInformation($"Inicia método PoliciesController.Index {JsonConvert.SerializeObject(pagination)}");
                    var responseIn = await policiesServices.GetPoliciesPagination(pagination);
                    _logger.LogInformation($"Finaliza método PoliciesController.Index {responseIn}");
                    return Ok(new { message = "Se creo la politica de manera exitosa", state = "Success" });
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error en el método PoliciesController.Index : {JsonConvert.SerializeObject(ex.Message)}");
                    return BadRequest(new { message = "Error al crear la politica", state = "Error" });
                }
            }

        }
    }
}
