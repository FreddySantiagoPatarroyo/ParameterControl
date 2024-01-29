using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParameterControl.Models.Filter;
using ParameterControl.Models.User;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.Rows;
using ParameterControl.Services.Users;
using modUser = ParameterControl.Models.User;


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
        public async Task<ActionResult> Users()
        {

            List<User> Users = await usersServices.GetUsers();
            TableUsers.Data = await usersServices.GetUsersFormat(Users);

            TableUsers.Rows = rows.RowsUsers();

            TableUsers.IsCreate = true;
            TableUsers.IsActivate = true;
            TableUsers.IsEdit = true;
            TableUsers.IsInactivate = true;

            ViewBag.ApplyFilter = false;

            return View("Users", TableUsers);
        }


        [HttpGet]
        public async Task<ActionResult> UsersFilter(string filterColunm = "", string filterValue = "")
        {
            _logger.LogInformation(filterColunm);
            _logger.LogInformation(filterValue);

            if (filterColunm == null || filterColunm == "" || filterValue == null || filterValue == "")
            {
                return RedirectToAction("Users");
            }

            FilterViewModel filter = new FilterViewModel()
            {
                ColumValue = filterColunm,
                ValueFilter = filterValue
            };

            List<UserViewModel> usersFilter = await usersServices.GetFilterUsers(filter);

            TableUsers.Data = usersFilter;

            TableUsers.Rows = rows.RowsUsers();

            TableUsers.IsCreate = true;
            TableUsers.IsActivate = true;
            TableUsers.IsEdit = true;
            TableUsers.IsInactivate = true;

            ViewBag.ApplyFilter = true;

            return View("UsersFilter", TableUsers);
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

            Console.WriteLine(filter.ColumValue);

            Console.WriteLine(filter.ValueFilter);

            return RedirectToAction("UsersFilter", new
            {
                filterColunm = filter.ColumValue,
                filterValue = filter.ValueFilter
            });
        }
    }
}
