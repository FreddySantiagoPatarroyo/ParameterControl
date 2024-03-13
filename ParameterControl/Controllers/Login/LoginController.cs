using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ParameterControl.Models.Login;
using ParameterControl.Services.Audit;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.Users;
using ParameterControl.Services.Util;
using System.Security.Claims;
using modAudit = ParameterControl.Models.Audit;

namespace ParameterControl.Controllers.Login
{
    public class LoginController : Controller
    {
        private readonly IUsersServices _usersServices;
        private readonly AuthenticatedUser _authenticatedUser;
        private readonly IAuditsService auditsService;

        public LoginController(
            IConfiguration configuration, 
            AuthenticatedUser authenticatedUser,
            IAuditsService auditsService
        )
        {
            _usersServices = new UsersServices(configuration);
            _authenticatedUser = authenticatedUser;
            this.auditsService = auditsService;
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

                        var audit = new modAudit.Audit()
                        {
                            Action = "Ingresar a la aplicacion",
                            UserCode = _authenticatedUser.GetUserCode(),
                            Component = "Login",
                            ModifieldDate = DateTime.Now,
                            BeforeValue = ""
                        };

                        await auditsService.InsertAudit(audit);

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

            var audit = new modAudit.Audit()
            {
                Action = "Salir de la aplicacion",
                UserCode = _authenticatedUser.GetUserCode(),
                Component = "Login",
                ModifieldDate = DateTime.Now,
                BeforeValue = ""
            };

            await auditsService.InsertAudit(audit);

            return RedirectToAction("Login");
        }
    }
}
