using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParameterControl.Models.ApprovedResult;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Models.Result;
using ParameterControl.Services.ApprovedResults;
using ParameterControl.Services.Audit;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.Results;
using ParameterControl.Services.Rows;
using System.Security.Claims;
using modApprovedResult = ParameterControl.Models.ApprovedResult;

namespace ParameterControl.Controllers.ApprovedResults
{
    [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
    public class ApprovedResultsController : Controller
    {
        public TableApprovedResultViewModel TableApprovedResults = new TableApprovedResultViewModel();
        private readonly ILogger<HomeController> _logger;
        private readonly IApprovedResultsServices approvedResultsServices;
        private readonly Rows rows;
        private readonly AuthenticatedUser authenticatedUser;
        private readonly IConfiguration _configuration;
        private readonly IAuditsService auditsService;
        private readonly ClaimsPrincipal _principal;
        private readonly bool _isCreate;
        private readonly bool _isActivate;
        private readonly bool _isEdit;
        private readonly bool _isView;
        private readonly bool _isInactive;

        public ApprovedResultsController(
            ILogger<HomeController> logger,
            IApprovedResultsServices approvedResultsServices,
            Rows rows,
            AuthenticatedUser authenticatedUser,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccesor,
            IAuditsService auditsService
        )
        {
            this._logger = logger;
            this.approvedResultsServices = approvedResultsServices;
            this.rows = rows;
            this.authenticatedUser = authenticatedUser;
            _configuration = configuration;
            this.auditsService = auditsService;
            var context = httpContextAccesor.HttpContext;
            _principal = context.User as ClaimsPrincipal;
            var data = _principal.FindFirst(ClaimTypes.Role).Value;
            var section = _configuration.GetSection($"Permisos:{data}:ApprovedResults").GetChildren();
            _isCreate = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnCreate")).FirstOrDefault().Value);
            _isActivate = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnActivate")).FirstOrDefault().Value);
            _isEdit = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnEdit")).FirstOrDefault().Value);
            _isView = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnDetail")).FirstOrDefault().Value);
            _isInactive = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnInactive")).FirstOrDefault().Value);
        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpGet]
        public async Task<ActionResult> ApprovedResults(PaginationViewModel paginationViewModel)
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            try
            {
                List<modApprovedResult.ApprovedResult> approvedResults = await approvedResultsServices.GetAppovedResultsPagination(paginationViewModel);
                int TotalApprovedResults = await approvedResultsServices.CountApprovedResults();

                TableApprovedResults.Data = await approvedResultsServices.GetApprovedResultsFormat(approvedResults);
                TableApprovedResults.Rows = rows.RowsApprovedResults();
                TableApprovedResults.IsCreate = _isCreate;
                TableApprovedResults.IsActivate = _isActivate;
                TableApprovedResults.IsEdit = _isEdit;
                TableApprovedResults.IsView = _isView;
                TableApprovedResults.IsInactivate = _isInactive;
                TableApprovedResults.Filter = true;
                ViewBag.ApplyFilter = false;

                var resultViemModel = new PaginationResult<TableApprovedResultViewModel>()
                {
                    Elements = TableApprovedResults,
                    Page = paginationViewModel.Page,
                    RecordsPage = paginationViewModel.RecordsPage,
                    TotalRecords = TotalApprovedResults,
                    BaseUrl = Url.Action() + "?"
                };

                ViewBag.Success = true;
                return View("ApprovedResults", resultViemModel);

            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("ApprovedResults", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpGet]
        public async Task<ActionResult> ApprovedResultsFilter(PaginationViewModel paginationViewModel, string filterColunm = "", string filterValue = "", string typeRow = "")
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            try
            {
                if (filterColunm == null || filterColunm == "" || filterValue == null || filterValue == "")
                {
                    return RedirectToAction("ApprovedResults");
                }

                FilterViewModel filter = new FilterViewModel()
                {
                    ColumValue = filterColunm,
                    ValueFilter = filterValue,
                    TypeRow = typeRow
                };

                List<ApprovedResultViewModel> approvedResultsFilter = await approvedResultsServices.GetFilterApprovedResults(filter);
                int TotalApprovedResults = approvedResultsFilter.Count();

                TableApprovedResults.Data = approvedResultsServices.GetFilterPagination(approvedResultsFilter, paginationViewModel, TotalApprovedResults);
                TableApprovedResults.Rows = rows.RowsApprovedResults();
                TableApprovedResults.IsCreate = _isCreate;
                TableApprovedResults.IsActivate = _isActivate;
                TableApprovedResults.IsEdit = _isEdit;
                TableApprovedResults.IsView = _isView;
                TableApprovedResults.IsInactivate = _isInactive;
                TableApprovedResults.Filter = true;
                ViewBag.ApplyFilter = true;

                var resultViemModel = new PaginationResult<TableApprovedResultViewModel>()
                {
                    Elements = TableApprovedResults,
                    Page = paginationViewModel.Page,
                    RecordsPage = paginationViewModel.RecordsPage,
                    TotalRecords = TotalApprovedResults,
                    BaseUrl = Url.Action() + "?filterColunm=" + filterColunm + "&filterValue=" + filterValue + "&typeRow=" + typeRow + "&"
                };

                ViewBag.Success = true;
                return View("ApprovedResultsFilter", resultViemModel);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("ApprovedResultsFilter", null);
            }
        }

        //[HttpGet]
        //public async Task<ActionResult> View(string id)
        //{
        //    ApprovedResult approvedResult = await approvedResultsServices.GetApprovedResultsById(id);
        //    ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
        //    return View("Actions/ViewApprovedResult", approvedResult);
        //}

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpGet]
        public ActionResult Filter()
        {
            FilterViewModel model = new FilterViewModel()
            {
                Rows = rows.RowsApprovedResults()

            };
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            return View("Actions/Filter", model);
        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpPost]
        public async Task<ActionResult> FilterApprovedResults(FilterViewModel filter)
        {
            if (filter.TypeRow == "Select")
            {
                filter.ValueFilter = filter.ValueFilterOptions;
            }
            else if (filter.TypeRow == "Date")
            {
                filter.ValueFilter = filter.ValueFilterDate.ToString("dd/MM/yyyy");
            }

            return RedirectToAction("ApprovedResultsFilter", new
            {
                filterColunm = filter.ColumValue,
                filterValue = filter.ValueFilter,
                typeRow = filter.TypeRow
            });
        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
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
                case "CreationDateFormat":
                    filter.TypeRow = "Date";
                    break;
                case "UpdateDateFormat":
                    filter.TypeRow = "Date";
                    break;
                case "UploadDateFormat":
                    filter.TypeRow = "Date";
                    break;
                case "StartDateFormat":
                    filter.TypeRow = "Date";
                    break;
                case "EndDateFormat":
                    filter.TypeRow = "Date";
                    break;
                default:
                    filter.TypeRow = "General";
                    break;
            }
            return Ok(filter);
        }
    }
}
