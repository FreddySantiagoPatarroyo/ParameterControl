using Microsoft.AspNetCore.Mvc.Rendering;

namespace ParameterControl.Models.Parameter
{
    public class ParameterCreateViewModel:Parameter
    {
        public List<SelectListItem> ParameterTypeOption = new List<SelectListItem>();

        public List<SelectListItem> ListParameter = new List<SelectListItem>();
    }
}
