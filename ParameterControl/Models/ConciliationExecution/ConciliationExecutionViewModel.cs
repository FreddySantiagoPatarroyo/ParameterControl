using Microsoft.AspNetCore.Mvc.Rendering;

namespace ParameterControl.Models.ConciliationExecution
{
    public class ConciliationExecutionViewModel : ConciiliationExecution
    {
        public List<SelectListItem> Conciliations = new List<SelectListItem>();
        public bool IsExecution { get; set; } = false;
        public bool IsProgram { get; set; } = false;
        public bool IsAbort { get; set; } = false;
    }
}
