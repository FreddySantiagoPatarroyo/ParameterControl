using Microsoft.AspNetCore.Mvc;
using ParameterControl.Models.Login;
using System.Xml.Serialization;

namespace ParameterControl.Controllers.Login
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel request)
        {
            var user = request;
            return View();
        }
    }
}
