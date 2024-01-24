using Microsoft.AspNetCore.Mvc.Rendering;

namespace ParameterControl.Models.Policy
{
    public class PolicyCreateViewModel:Policy
    {
        public List<SelectListItem> OperationTypeOptions = new List<SelectListItem>();
    }
}
