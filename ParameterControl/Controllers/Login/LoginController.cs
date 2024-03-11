using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ParameterControl.Models.Login;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.Users;
using ParameterControl.Services.Util;
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

                var user = await _usersServices.ValidateUser(request);

                if (user != null)
                {
                    if (user.State == false)
                    {
                        return Ok(new { message = "El usuario esta desactivado, comuniquese con el administrador", state = "Error" });
                    }
                    else
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,user.User_),
                            new Claim("Correo",user.Email),
                            new Claim(ClaimTypes.NameIdentifier, user.Code.ToString())
                        };
                        claims.Add(new Claim(ClaimTypes.Role, user.RolName));

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new System.Security.Claims.ClaimsPrincipal(claimsIdentity));

                        return Ok(new { message = "Datos ingresados correctamente", state = "Success"});
                    }
                }
                else
                {
                    return BadRequest(new { message = "El usuario o la contraseña son incorrectos", state = "Error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al ingresar", state = "Error" });
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
    }
}
