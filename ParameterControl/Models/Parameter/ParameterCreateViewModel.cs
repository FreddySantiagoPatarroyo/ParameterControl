using Microsoft.AspNetCore.Mvc.Rendering;

namespace ParameterControl.Models.Parameter
{
    public class ParameterCreateViewModel : Parameter
    {
        public string ParameterFormat { get; set; } = string.Empty;
        public List<SelectListItem> ParameterTypeOption = new List<SelectListItem>();
        public List<SelectListItem> ListParameter = new List<SelectListItem>();
    }
}
