using Microsoft.AspNetCore.Mvc;
using ParameterControl.Models.User;
using ParameterControl.Models.Filter;
using ParameterControl.Services.Users;
using ParameterControl.Services.Rows;

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
        public async Task<ActionResult> Desactive(string id)
        {
            User user = await usersServices.GetUsersById(id);

            return View("Actions/DesactiveUser", user);
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
