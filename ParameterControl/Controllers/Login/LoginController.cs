using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using ParameterControl.Models.Login;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.Users;
using System.Security.Claims;

namespace ParameterControl.Controllers.Login
{
    public class LoginController : Controller
    {
        private readonly IUsersServices _usersServices;
        private readonly AuthenticatedUser _authenticatedUser;

        public LoginController(IConfiguration configuration, AuthenticatedUser authenticatedUser)
        {
            _usersServices = new UsersServices(configuration);
            _authenticatedUser = authenticatedUser;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var users = await _usersServices.GetUsers();
            var user = users.FirstOrDefault(x => x.Name.Equals(request.user) && x.Password.Equals(request.password));

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.User_),
                    new Claim("Correo",user.Email)
                };

                claims.Add(new Claim(ClaimTypes.Role, "A")); //A=Administrador

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new System.Security.Claims.ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }            
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
