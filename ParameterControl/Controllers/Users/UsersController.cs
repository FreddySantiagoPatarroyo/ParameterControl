﻿using Microsoft.AspNetCore.Mvc;
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
using ParameterControl.User.Entities;
using ParameterControl.Models.Result;
using System.Reflection;


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
            try
            {
                List<modUser.User> Users = await usersServices.GetUsersPagination(paginationViewModel);
                int TotalUsers = await usersServices.CountUsers();

                TableUsers.Data = await usersServices.GetUsersFormat(Users);

                TableUsers.Rows = rows.RowsUsers();

                TableUsers.IsCreate = true;
                TableUsers.IsActivate = true;
                TableUsers.IsEdit = true;
                TableUsers.IsView = true;
                TableUsers.IsInactivate = true;
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


        [HttpGet]
        public async Task<ActionResult> UsersFilter(PaginationViewModel paginationViewModel, string filterColunm = "", string filterValue = "", string typeRow = "")
        {
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
                TableUsers.IsCreate = true;
                TableUsers.IsActivate = true;
                TableUsers.IsEdit = true;
                TableUsers.IsView = true;
                TableUsers.IsInactivate = true;
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

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            try
            {
                UserCreateViewModel model = new UserCreateViewModel();

                ViewBag.Success = true;
                return View("Actions/CreateUser", model);
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View("Actions/CreateUser", null);
            }
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
                    var responseIn = await usersServices.InsertUser(request);
                    _logger.LogInformation($"Finaliza método UsersController.Create {JsonConvert.SerializeObject(responseIn)}");

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

        [HttpPost]
        public async Task<ActionResult> EditUser([FromBody] modUser.User request)
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
                try
                {
                    var responseIn = await usersServices.UpdateUser(request);
                    _logger.LogInformation($"Finaliza método UsersController.Edit {JsonConvert.SerializeObject(responseIn)}");
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

        [HttpGet]
        public async Task<ActionResult> Active(int code)
        {
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

        [HttpPost]
        public async Task<ActionResult> ActiveUser([FromBody] int code)
        {
            try
            {
                modUser.User request = await usersServices.GetUsersByCode(code);
                var responseIn = await usersServices.ActiveUser(request);
                _logger.LogInformation($"Finaliza método UsersController.Active {JsonConvert.SerializeObject(request)}");
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

        [HttpPost]
        public async Task<ActionResult> DesactiveUser([FromBody] int code)
        {
            try
            {
                modUser.User request = await usersServices.GetUsersByCode(code);
                var responseIn = await usersServices.DesactiveUser(request);
                _logger.LogInformation($"Finaliza método UsersController.Desactive {JsonConvert.SerializeObject(request)}");
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
