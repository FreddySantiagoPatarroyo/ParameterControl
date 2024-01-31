using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParameterControl.Models.Filter;
using ParameterControl.Models.User;
using ParameterControl.Services.Authenticated;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParameterControl.Services.Rows;
using ParameterControl.Services.Users;
using modUser = ParameterControl.Models.User;
using ParameterControl.Models.Pagination;
using ParameterControl.Services.Policies;
using ParameterControl.Models.Policy;


namespace ParameterControl.Controllers.Users
{
    public class UsersController : Controller
    {
        public TableUserViewModel TableUsers = new TableUserViewModel();
        private readonly ILogger<HomeController> _logger;
        private readonly IUsersServices usersServices;
        private readonly Rows rows;
        private readonly AuthenticatedUser authenticatedUser;

        public UsersController(
            ILogger<HomeController> logger,
            IUsersServices usersServices,
            Rows rows,
            AuthenticatedUser authenticatedUser
        )
        {
            this._logger = logger;
            this.usersServices = usersServices;
            this.rows = rows;
            this.authenticatedUser = authenticatedUser;
        }

        [HttpGet]
        public async Task<ActionResult> Users(PaginationViewModel paginationViewModel)
        {

            List<User> Users = await usersServices.GetUsersPagination(paginationViewModel);
            int TotalUsers = await usersServices.CountUsers();

            TableUsers.Data = await usersServices.GetUsersFormat(Users);

            TableUsers.Rows = rows.RowsUsers();

            TableUsers.IsCreate = true;
            TableUsers.IsActivate = true;
            TableUsers.IsEdit = true;
            TableUsers.IsInactivate = true;

            var resultViemModel = new PaginationResult<TableUserViewModel>()
            {
                Elements = TableUsers,
                Page = paginationViewModel.Page,
                RecordsPage = paginationViewModel.RecordsPage,
                TotalRecords = TotalUsers,
                BaseUrl = Url.Action() + "?"
            };

            ViewBag.ApplyFilter = false;

            return View("Users", resultViemModel);
        }


        [HttpGet]
        public async Task<ActionResult> UsersFilter(PaginationViewModel paginationViewModel, string filterColunm = "", string filterValue = "", string typeRow = "")
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

            TableUsers.IsCreate = true;
            TableUsers.IsActivate = true;
            TableUsers.IsEdit = true;
            TableUsers.IsInactivate = true;

            var resultViemModel = new PaginationResult<TableUserViewModel>()
            {
                Elements = TableUsers,
                Page = paginationViewModel.Page,
                RecordsPage = paginationViewModel.RecordsPage,
                TotalRecords = TotalUsers,
                BaseUrl = Url.Action() + "?filterColunm=" + filterColunm + "&filterValue=" + filterValue + "&typeRow=" + typeRow + "&"
            };

            ViewBag.ApplyFilter = true;

            return View("UsersFilter", resultViemModel);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            UserCreateViewModel model = new UserCreateViewModel();

            return View("Actions/CreateUser", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] modUser.User request)
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
                    _logger.LogInformation($"Inicia método UsersController.Create {JsonConvert.SerializeObject(request)}");

                    return Ok(new { message = "Se creo el usuario de manera exitosa", state = "Success" });
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error en el método UsersController.Create : {JsonConvert.SerializeObject(ex.Message)}");
                    return BadRequest(new { message = "Error al crear el usuario", state = "Error" });
                }
            }

        }

        [HttpGet]
        public async Task<ActionResult> Edit(int code)
        {
            modUser.User user = await usersServices.GetUsersByCode(code);

            UserCreateViewModel model = await usersServices.GetUserFormatCreate(user);

            //UserCreateViewModel model = new UserCreateViewModel()
            //{
            //    Code = user.Code,
            //    Name = user.Name,
            //    User_ = user.User_,
            //    Email = user.Email,
            //    State = user.State,
            //    CreationDate = user.CreationDate
            //};

            return View("Actions/EditUser", model);
        }

        [HttpPost]
        public async Task<ActionResult> EditUser([FromBody] modUser.User request)
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
                    _logger.LogInformation($"Inicia método UsersController.Edit {JsonConvert.SerializeObject(request)}");
                    return Ok(new { message = "Se actualizo el usuario de manera exitosa", state = "Success" });
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error en el método UsersController.Edit : {JsonConvert.SerializeObject(ex.Message)}");
                    return BadRequest(new { message = "Error al actualizar el usuario", state = "Error" });
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> View(int code)
        {
            User user = await usersServices.GetUsersByCode(code);

            return View("Actions/ViewUser", user);
        }

        [HttpGet]
        public async Task<ActionResult> Active(int code)
        {
            modUser.User user = await usersServices.GetUsersByCode(code);

            return View("Actions/ActiveUser", user);
        }

        [HttpPost]
        public async Task<ActionResult> ActiveUser([FromBody] int code)
        {
            try
            {
                modUser.User request = await usersServices.GetUsersByCode(code);
                request.UserOwner = authenticatedUser.GetUserOwnerId();
                request.UpdateDate = DateTime.Now;
                request.State = true;
                _logger.LogInformation($"Inicia método UsersController.Active {JsonConvert.SerializeObject(request)}");
                return Ok(new { message = "Se activo el usuario de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método UsersController.Active : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al activar el usuario", state = "Error" });
            }
        }

        [HttpGet]
        public async Task<ActionResult> Desactive(int code)
        {
            User user = await usersServices.GetUsersByCode(code);

            return View("Actions/DesactiveUser", user);
        }

        [HttpPost]
        public async Task<ActionResult> DesactiveUser([FromBody] int code)
        {
            try
            {
                modUser.User request = await usersServices.GetUsersByCode(code);
                request.UserOwner = authenticatedUser.GetUserOwnerId();
                request.UpdateDate = DateTime.Now;
                request.State = false;
                _logger.LogInformation($"Inicia método UsersController.Desactive {JsonConvert.SerializeObject(request)}");
                return Ok(new { message = "Se desactivo el usuario de manera exitosa", state = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el método UsersController.Desactive : {JsonConvert.SerializeObject(ex.Message)}");
                return BadRequest(new { message = "Error al desactivar el usuario", state = "Error" });
            }
        }

        [HttpGet]
        public ActionResult Filter()
        {
            FilterViewModel model = new FilterViewModel()
            {
                Rows = rows.RowsUsers()

            };

            return View("Actions/Filter", model);
        }

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
    }
}
