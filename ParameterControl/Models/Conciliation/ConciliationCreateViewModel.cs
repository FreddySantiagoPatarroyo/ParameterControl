using Microsoft.AspNetCore.Mvc.Rendering;

namespace ParameterControl.Models.Conciliation
{
    public class ConciliationCreateViewModel:Conciliation
    {
        public List<SelectListItem> PoliciesOption = new List<SelectListItem>();
        public List<SelectListItem> RequiredOption = new List<SelectListItem>();
        public string RequiredFormat { get; set; } = string.Empty;
    }
}
