using Microsoft.AspNetCore.Mvc.Rendering;

namespace ParameterControl.Models.Conciliation
{
    public class ConciliationCreateViewModel : Conciliation
    {
        public string CodeFormat { get; set; } = string.Empty;
        public List<SelectListItem> OperationTypeOptions = new List<SelectListItem>();
        public List<SelectListItem> PoliciesOption = new List<SelectListItem>();
        public List<SelectListItem> RequiredOption = new List<SelectListItem>();
        public List<SelectListItem> Emails = new List<SelectListItem>();
        public string RequiredFormat { get; set; } = string.Empty;
    }
}
