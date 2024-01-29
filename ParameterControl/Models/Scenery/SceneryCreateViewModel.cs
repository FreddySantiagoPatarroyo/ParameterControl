using Microsoft.AspNetCore.Mvc.Rendering;

namespace ParameterControl.Models.Scenery
{
    public class SceneryCreateViewModel : Scenery
    {
        public string CodeFormat { get; set; } = string.Empty;
        public List<SelectListItem> ImpactOptions = new List<SelectListItem>();
        public List<SelectListItem> ConciliationOptions = new List<SelectListItem>();
    }
}
