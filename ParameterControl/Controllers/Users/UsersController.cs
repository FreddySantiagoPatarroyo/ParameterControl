using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Models.User;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.Rows;
using ParameterControl.Services.Users;
using ParameterControl.Services.Util;
using System.Data;
using System.Security.Claims;
using modUser = ParameterControl.Models.User;
using modAudit = ParameterControl.Models.Audit;
using ParameterControl.Services.Audit;

namespace ParameterControl.Controllers.Users
{    
    public class UsersController : Controller
    {
        public TableUserViewModel TableUsers = new TableUserViewModel();
        private readonly ILogger<HomeController> _logger;
        private readonly IUsersServices usersServices;
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

        public UsersController(
            ILogger<HomeController> logger,
            IUsersServices usersServices,
            Rows rows,
            AuthenticatedUser authenticatedUser,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccesor,
            IAuditsService auditsService
        )
        {
            this._logger = logger;
            this.usersServices = usersServices;
            this.rows = rows;
            this.authenticatedUser = authenticatedUser;
            _configuration = configuration;
            this.auditsService = auditsService;
            var context = httpContextAccesor.HttpContext;
            _principal = context.User as ClaimsPrincipal;
            var data = _principal.FindFirst(ClaimTypes.Role).Value;
            var section = _configuration.GetSection($"Permisos:{data}:Users").GetChildren();
            _isCreate = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnCreate")).FirstOrDefault().Value);
            _isActivate = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnActivate")).FirstOrDefault().Value);
            _isEdit = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnEdit")).FirstOrDefault().Value);
            _isView = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnDetail")).FirstOrDefault().Value);
            _isInactive = Convert.ToBoolean(section.Where(x => x.Key.Equals("btnInactive")).FirstOrDefault().Value);
        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpGet]
        public async Task<ActionResult> Users(PaginationViewModel paginationViewModel)
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            try
            {
                List<modUser.User> Users = await usersServices.GetUsersPagination(paginationViewModel);
                int TotalUsers = await usersServices.CountUsers();

                TableUsers.Data = await usersServices.GetUsersFormat(Users);

                TableUsers.Rows = rows.RowsUsers();

                TableUsers.IsCreate = _isCreate;
                TableUsers.IsActivate = _isActivate;
                TableUsers.IsEdit = _isEdit;
                TableUsers.IsView = _isView;
                TableUsers.IsInactivate = _isInactive;
                TableUsers.Filter = true;
                ViewBag.ApplyFilter = false;

                var resultViemModel = new PaginationResult<TableUserViewModel>()
                {
                    Elements = TableUsers,
                    Page = paginationViewModel.Page,
                    RecordsPage = paginationViewModel.RecordsPage,
                    TotalRecords = TotalUsers,
                    BaseUrl = Url.Action() + "?"
                };

                ViewBag.Success = true;
                return View("Users", resultViemModel);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("Users", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpGet]
        public async Task<ActionResult> UsersFilter(PaginationViewModel paginationViewModel, string filterColunm = "", string filterValue = "", string typeRow = "")
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            try
            {
                if (filterColunm == null || filterColunm == "" || filterValue == null || filterValue == "")
                {
                    return RedirectToAction("Users");
                }

                FilterViewModel filter = new FilterViewModel()
                {
                    ColumValue = filterColunm,
                    ValueFilter = filterValue,
                    TypeRow = typeRow
                };

                List<UserViewModel> usersFilter = await usersServices.GetFilterUsers(filter);
                int TotalUsers = usersFilter.Count();

                TableUsers.Data = usersServices.GetFilterPagination(usersFilter, paginationViewModel, TotalUsers);
                TableUsers.Rows = rows.RowsUsers();
                TableUsers.IsCreate = _isCreate;
                TableUsers.IsActivate = _isActivate;
                TableUsers.IsEdit = _isEdit;
                TableUsers.IsView = _isView;
                TableUsers.IsInactivate = _isInactive;
                TableUsers.Filter = true;
                ViewBag.ApplyFilter = true;

                var resultViemModel = new PaginationResult<TableUserViewModel>()
                {
                    Elements = TableUsers,
                    Page = paginationViewModel.Page,
                    RecordsPage = paginationViewModel.RecordsPage,
                    TotalRecords = TotalUsers,
                    BaseUrl = Url.Action() + "?filterColunm=" + filterColunm + "&filterValue=" + filterValue + "&typeRow=" + typeRow + "&"
                };

                ViewBag.Success = true;
                return View("UsersFilter", resultViemModel);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("UsersFilter", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            try
            {
                UserCreateViewModel model = new UserCreateViewModel();
                model.Roles = await GetRoles();

                ViewBag.Success = true;
                return View("Actions/CreateUser", model);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("Actions/CreateUser", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] modUser.User request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError($"Error en el modelo : {JsonConvert.SerializeObject(request)}");
                    return BadRequest(new { message = "Error en la informacion enviada", state = "Error" });
                }
                else
                {
                    var responseIn = await usersServices.InsertUser(request);
                    var audit = new modAudit.Audit()
                    {
                        Action = "Crear Usuario",
                        UserCode = authenticatedUser.GetUserCode(),
                        Component = "Usuarios",
                        ModifieldDate = DateTime.Now,
                        BeforeValue = ""
                    };

                    await auditsService.InsertAudit(audit);
                    _logger.LogInformation($"Finaliza método UsersController.Create {JsonConvert.SerializeObject(responseIn)}");

                    return Ok(new { message = "Se creo el usuario de manera exitosa", state = "Success" });

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método UsersController.Create : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al crear el usuario", state = "Error" });
            }

        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet]
        public async Task<ActionResult> Edit(int code)
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            try
            {
                ViewBag.CodeSend = code;
                modUser.User user = await usersServices.GetUsersByCode(code);
                if (user.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/EditUser", null);
                }
                UserCreateViewModel model = await usersServices.GetUserFormatCreate(user);
                model.Roles = await GetRoles();

                ViewBag.Success = true;
                ViewBag.EntyNull = false;
                return View("Actions/EditUser", model);

            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/EditUser", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public async Task<ActionResult> EditUser([FromBody] modUser.User request)
        {
            try
            {
                var user = await usersServices.GetUsersByCode(request.Code);

                if (user.Code == 0)
                {
                    _logger.LogError($"Error el usuario no existe : {JsonConvert.SerializeObject(request)}");
                    return BadRequest(new { message = "No existe un usuario con el codigo" + user.Code, state = "Error" });
                }
                else
                {
                    request.CreationDate = user.CreationDate;
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError($"Error en el modelo : {JsonConvert.SerializeObject(request)}");
                    return BadRequest(new { message = "Error en la informacion enviada", state = "Error" });
                }
                else
                {
                    var responseIn = await usersServices.UpdateUser(request);

                    var audit = new modAudit.Audit()
                    {
                        Action = "Editar Usuario",
                        UserCode = authenticatedUser.GetUserCode(),
                        Component = "Usuarios",
                        ModifieldDate = DateTime.Now,
                        BeforeValue = JsonConvert.SerializeObject(user).ToString()
                    };

                    await auditsService.InsertAudit(audit);

                    _logger.LogInformation($"Finaliza método UsersController.Edit {JsonConvert.SerializeObject(responseIn)}");
                    return Ok(new { message = "Se actualizo el usuario de manera exitosa", state = "Success" });

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método UsersController.Edit : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al actualizar el usuario", state = "Error" });
            }

        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpGet]
        public async Task<ActionResult> View(int code)
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            try
            {
                ViewBag.CodeSend = code;
                modUser.User user = await usersServices.GetUsersByCode(code);
                if (user.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/ViewUser", null);
                }
                UserViewModel model = await usersServices.GetUserFormat(user);

                var audit = new modAudit.Audit()
                {
                    Action = "Ver Usuario",
                    UserCode = authenticatedUser.GetUserCode(),
                    Component = "Usuarios",
                    ModifieldDate = DateTime.Now,
                    BeforeValue = ""
                };

                await auditsService.InsertAudit(audit);

                ViewBag.Success = true;
                ViewBag.EntyNull = false;
                return View("Actions/ViewUser", model);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/ViewUser", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet]
        public async Task<ActionResult> Active(int code)
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            try
            {
                ViewBag.CodeSend = code;
                modUser.User user = await usersServices.GetUsersByCode(code);
                if (user.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/ActiveUser", null);
                }

                ViewBag.Success = true;
                ViewBag.EntyNull = false;
                return View("Actions/ActiveUser", user);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/ActiveUser", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public async Task<ActionResult> ActiveUser([FromBody] int code)
        {
            try
            {
                modUser.User request = await usersServices.GetUsersByCode(code);
                var responseIn = await usersServices.ActiveUser(request);
                var audit = new modAudit.Audit()
                {
                    Action = "Activar Usuario",
                    UserCode = authenticatedUser.GetUserCode(),
                    Component = "Usuarios",
                    ModifieldDate = DateTime.Now,
                    BeforeValue = JsonConvert.SerializeObject(request).ToString()
                };

                await auditsService.InsertAudit(audit);
                _logger.LogInformation($"Finaliza método UsersController.Active {JsonConvert.SerializeObject(request)}");
                return Ok(new { message = "Se activo el usuario de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método UsersController.Active : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al activar el usuario", state = "Error" });
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet]
        public async Task<ActionResult> Desactive(int code)
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            try
            {
                ViewBag.CodeSend = code;
                modUser.User user = await usersServices.GetUsersByCode(code);
                if (user.Code == 0)
                {
                    ViewBag.Success = true;
                    ViewBag.EntyNull = true;
                    return View("Actions/DesactiveUser", null);
                }

                ViewBag.Success = true;
                ViewBag.EntyNull = false;
                return View("Actions/DesactiveUser", user);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                ViewBag.EntyNull = false;
                return View("Actions/DesactiveUser", null);
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public async Task<ActionResult> DesactiveUser([FromBody] int code)
        {
            try
            {
                modUser.User request = await usersServices.GetUsersByCode(code);
                var responseIn = await usersServices.DesactiveUser(request);
                var audit = new modAudit.Audit()
                {
                    Action = "Desactivar Usuario",
                    UserCode = authenticatedUser.GetUserCode(),
                    Component = "Usuarios",
                    ModifieldDate = DateTime.Now,
                    BeforeValue = JsonConvert.SerializeObject(request).ToString()
                };

                await auditsService.InsertAudit(audit);
                _logger.LogInformation($"Finaliza método UsersController.Desactive {JsonConvert.SerializeObject(request)}");
                return Ok(new { message = "Se desactivo el usuario de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método UsersController.Desactive : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al desactivar el usuario", state = "Error" });
            }
        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpGet]
        public ActionResult Filter()
        {
            FilterViewModel model = new FilterViewModel()
            {
                Rows = rows.RowsUsers()

            };
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            return View("Actions/Filter", model);
        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        [HttpPost]
        public async Task<ActionResult> FilterUsers(FilterViewModel filter)
        {
            if (filter.TypeRow == "Select")
            {
                filter.ValueFilter = filter.ValueFilterOptions;
            }
            else if (filter.TypeRow == "Date")
            {
                filter.ValueFilter = filter.ValueFilterDate.ToString("dd/MM/yyyy");
            }

            return RedirectToAction("UsersFilter", new
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
                default:
                    filter.TypeRow = "General";
                    break;
            }
            return Ok(filter);
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public async Task<IActionResult> ValidateDataRepeatCreate([FromBody] string value)
        {
            var data = value.Split(",");
            List<modUser.User> Users = await usersServices.GetUsers();
            var validate = await GetDataRepeat(Users, data);

            return validate == false ? Ok(validate) : BadRequest(validate);
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpPost]
        public async Task<IActionResult> ValidateDataRepeatEdit([FromBody] string value)
        {
            var data = value.Split(",");
            List<modUser.User> Users = await usersServices.GetUsers();
            List<modUser.User> UsersEdit = new List<modUser.User>();

            foreach (var user in Users)
            {
                if (user.Code.ToString() != data[2].ToUpper())
                {
                    UsersEdit.Add(user);
                }
            }

            var validate = await GetDataRepeat(UsersEdit, data);
            return validate == false ? Ok(validate) : BadRequest(validate);
        }

        private async Task<bool> GetDataRepeat(List<modUser.User> Users, string[] data)
        {
            bool validate = false;
            var property = typeof(modUser.User)?.GetProperty(data[0]);

            foreach (var user in Users)
            {
                if (property?.GetValue(user).ToString().ToUpper() == data[1].ToUpper())
                {
                    validate = true;
                    break;
                }
                validate = false;
            }

            return validate;
        }

        private async Task<List<SelectListItem>> GetRoles()
        {
            var roles = await usersServices.GetRoles();
            return roles.Select(rol => new SelectListItem(rol.Name, rol.Code.ToString())).ToList();
        }
    }
}
