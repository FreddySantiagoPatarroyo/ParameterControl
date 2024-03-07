using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParameterControl.Models;
using ParameterControl.Services.Authenticated;
using System.Diagnostics;

namespace ParameterControl.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AuthenticatedUser authenticatedUser;
        public HomeController(ILogger<HomeController> logger, AuthenticatedUser authenticatedUser)
        {
            this._logger = logger;
            this.authenticatedUser = authenticatedUser;
        }

        public IActionResult Index()
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            return View();
        }

        public IActionResult Home()
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            return View("Index");
        }

        public IActionResult Privacy()
        {
            ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
            return View();
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
