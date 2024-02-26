using Microsoft.AspNetCore.Mvc.Rendering;

namespace ParameterControl.Models.Filter
{
    public class FilterConciliacionViewModel
    {
        public List<SelectListItem> Conciliations { get; set; } = new List<SelectListItem>();
        public string Conciliation { get; set; } = string.Empty;
    }
}
