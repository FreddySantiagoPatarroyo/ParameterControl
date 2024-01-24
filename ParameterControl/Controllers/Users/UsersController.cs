using Microsoft.AspNetCore.Mvc;
using ParameterControl.Models.User;
using ParameterControl.Models.Filter;
using ParameterControl.Services.Users;
using ParameterControl.Services.Rows;
using modUser = ParameterControl.Models.User;
using Newtonsoft.Json;
using ParameterControl.Services.Policies;


namespace ParameterControl.Controllers.Users
{
    public class UsersController: Controller
    {
        public TableUserViewModel TableUsers = new TableUserViewModel();
        private readonly ILogger<HomeController> _logger;
        private readonly IUsersServices usersServices;
        private readonly Rows rows;

        public UsersController(
            ILogger<HomeController> logger,
            IUsersServices usersServices,
            Rows rows
        )
        {
            this._logger = logger;
            this.usersServices = usersServices;
            this.rows = rows;
        }

        [HttpGet]
        public async Task<ActionResult> Users()
        {

            TableUsers.Data = await usersServices.GetUsers();

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

            List<User> usersFilter = await usersServices.GetFilterUsers(filter);

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
            modUser.User user = new User();

            user.CreationDate = DateTime.Now;
            user.UpdateDate = DateTime.Now;

            return View("Actions/CreateUser", user);
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
        public async Task<ActionResult> Edit(string id)
        {
            modUser.User user = await usersServices.GetUsersById(id);

            user.UpdateDate = DateTime.Now;

            return View("Actions/EditUser", user);
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
        public async Task<ActionResult> Desactive(string id)
        {
            User user = await usersServices.GetUsersById(id);

            return View("Actions/DesactiveUser", user);
        }
        [HttpPost]
        public async Task<ActionResult> DesactiveUser([FromBody] string request)
        {
            try
            {
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
        public async Task<ActionResult> Active(string id)
        {
            modUser.User user = await usersServices.GetUsersById(id);

            return View("Actions/ActiveUser", user);
        }
        [HttpPost]
        public async Task<ActionResult> ActiveUser([FromBody] string request)
        {
            try
            {
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
        public async Task<ActionResult> View(string id)
        {
            User user = await usersServices.GetUsersById(id);

            return View("Actions/ViewUser", user);
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
