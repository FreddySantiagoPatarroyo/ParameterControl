using Microsoft.AspNetCore.Mvc.Rendering;

namespace ParameterControl.Models.Policy
{
    public class PolicyCreateViewModel : Policy
    {
        public string CodeFormat { get; set; } = string.Empty;
        public List<SelectListItem> OperationTypeOptions = new List<SelectListItem>();
    }
}
