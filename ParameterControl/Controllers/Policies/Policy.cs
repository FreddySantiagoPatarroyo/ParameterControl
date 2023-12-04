using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ParameterControl.Controllers.Policies
{
    public class Policy : Controller
    {
        public ActionResult Policies()
        {
            return View();
        }
    }
}
