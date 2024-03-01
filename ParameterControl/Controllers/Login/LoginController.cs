using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> LoginUser([FromBody] LoginViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Error en la informacion enviada", state = "Error" });
                }
                var users = await _usersServices.GetUsers();
                var user = users.FirstOrDefault(x => x.User_.Equals(request.User) && x.Password.Equals(request.Password));

                if (user != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.User_),
                    new Claim("Correo",user.Email)
                };

                    claims.Add(new Claim(ClaimTypes.Role, user.RolName));

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new System.Security.Claims.ClaimsPrincipal(claimsIdentity));

                    return Ok(new { message = "Se creo el usuario de manera exitosa", state = "Success" });
                }
                else
                {
                    return BadRequest(new { message = "El usuario o la contraseña son incorrectos", state = "Error" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Error al ingresar", state = "Error" });
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
