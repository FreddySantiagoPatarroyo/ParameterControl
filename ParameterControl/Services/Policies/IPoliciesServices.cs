using ParameterControl.Models.Filter;
using ParameterControl.Models.Policy;

namespace ParameterControl.Services.Policies
{
    public interface IPoliciesServices
    {
        Task<List<Policy>> GetFilterPolicies(FilterViewModel filterModel);
        Task<List<string>> GetOperationsType();
        Task<List<Policy>> GetPolicies();
        Task<Policy> GetPolicyById(string id);
    }
}
