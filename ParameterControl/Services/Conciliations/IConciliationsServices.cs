using Microsoft.AspNetCore.Mvc.Rendering;
using ParameterControl.Models.Conciliation;
using ParameterControl.Models.Filter;
using modConciliation = ParameterControl.Models.Conciliation;
using modPolicy = ParameterControl.Models.Policy;


namespace ParameterControl.Services.Conciliations
{
    public interface IConciliationsServices
    {
        Task<List<modPolicy.Policy>> GetPolicies();
        Task<List<SelectListItem>> GetRequired();
        Task<List<modConciliation.Conciliation>> GetConciliations();
        Task<modConciliation.Conciliation> GetConciliationsById(string id);
        Task<List<ConciliationViewModel>> GetFilterConciliations(FilterViewModel filterModel);
        Task<List<ConciliationViewModel>> GetConciliationsFormat(List<Conciliation> conciliations);
    }
}
