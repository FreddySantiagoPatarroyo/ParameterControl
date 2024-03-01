using Microsoft.AspNetCore.Mvc.Rendering;

namespace ParameterControl.Models.ConciliationExecution
{
    public class ConciliationExecutionViewModel : ConciiliationExecution
    {
        public List<SelectListItem> Conciliations = new List<SelectListItem>();
    }
}
