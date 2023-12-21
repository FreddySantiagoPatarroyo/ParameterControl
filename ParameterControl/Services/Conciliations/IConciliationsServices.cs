using ParameterControl.Models.Conciliation;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Policy;

namespace ParameterControl.Services.Conciliations
{
    public interface IConciliationsServices
    {
        Task<List<Conciliation>> GetFilterPolicies(FilterViewModel filterModel);
        Task<List<Conciliation>> GetPolicies();
        Task<Policy> GetPolicyById(string id);
    }
}
