using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParameterControl.Models;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.Users;
using System.Diagnostics;
using System.Reflection;

using modUser = ParameterControl.Models.User;

namespace ParameterControl.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AuthenticatedUser authenticatedUser;
        private readonly IUsersServices usersServices;

        public HomeController(
            ILogger<HomeController> logger, 
            AuthenticatedUser authenticatedUser,
            IUsersServices usersServices
        )
        {
            this._logger = logger;
            this.authenticatedUser = authenticatedUser;
            this.usersServices = usersServices;
        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        public async Task<IActionResult> Index()
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            ViewBag.User = authenticatedUser.GetUserName();
            try
            {
                var code = authenticatedUser.GetUserCode();

                modUser.User request = await usersServices.GetUsersByCode(code);
                ViewBag.FirstAccess = request.FirstAccess;
                request.FirstAccess = true;
                var responseIn = await usersServices.UpdateUser(request);

                return View("Index");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
        public IActionResult Home()
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AuthorizedError()
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            return View();
        }
    }
}
