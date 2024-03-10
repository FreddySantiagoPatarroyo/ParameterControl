using System.Security.Claims;

namespace ParameterControl.Services.Authenticated
{
    public class AuthenticatedUser
    {
        private readonly HttpContext httpContext;

        public AuthenticatedUser(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContext = httpContextAccessor.HttpContext;
        }

        public int GetUserCode()
        {
            if (this.httpContext.User.Identity.IsAuthenticated)
            {
                var user = httpContext.User as ClaimsPrincipal;
                var claim = user.FindFirst(ClaimTypes.NameIdentifier);
                var id = int.Parse(claim.Value);

                return id;
            }
            else
            {
                return 0;
            }
        }

        public string GetUserName()
        {
            if (this.httpContext.User.Identity.IsAuthenticated)
            {
                var user = httpContext.User as ClaimsPrincipal;
                var claim = user.FindFirst(ClaimTypes.Name);
                var name = claim.Value;

                return name;
            }
            else
            {
                return "Usuario no autenticado";
            }
        }

        public string GetUserRol()
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                var user = httpContext.User as ClaimsPrincipal;
                var claim = user.FindFirst(ClaimTypes.Role);
                var rol = claim.Value;

                return rol;
            }
            else
            {
                return "Usuario no autenticado";
            }
        }

        public string GetUserNameAndRol()
        {
           return GetUserName() + " - " + GetUserRol();
        }
    }
}
