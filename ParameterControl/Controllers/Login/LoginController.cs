using Microsoft.AspNetCore.Mvc;
using ParameterControl.Models.Login;

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
            if(!ModelState.IsValid)
            {
                return View(request);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
