using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Indicator;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.Indicators;
using ParameterControl.Services.Rows;
using System.Security.Claims;
using modIndicator = ParameterControl.Models.Indicator;

namespace ParameterControl.Controllers.Indicators
{
    [Authorize(Roles = "ADMINISTRADOR,EJECUTOR,CONSULTOR")]
    public class IndicatorsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AuthenticatedUser authenticatedUser;
        private readonly IConfiguration _configuration;

        public IndicatorsController(
            ILogger<HomeController> logger,
            AuthenticatedUser authenticatedUser,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccesor
        )
        {
            this._logger = logger;
            this.authenticatedUser = authenticatedUser;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult> Indicators()
        {
            try
            {
                ViewBag.InfoUser = authenticatedUser.GetUserNameAndRol();
                return View("Indicators", null);
            }
            catch (Exception)
            {
                return View("Indicators", null);
            }
        }
    }
}
