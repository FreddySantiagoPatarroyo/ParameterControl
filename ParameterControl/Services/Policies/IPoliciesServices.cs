using ParameterControl.Models.Filter;
using ParameterControl.Models.Policy;

namespace ParameterControl.Services.Policies
{
    public interface IPoliciesServices
    {
        Task<List<PolicyViewModel>> GetFilterPolicies(FilterViewModel filterModel);
        Task<List<string>> GetOperationsType();
        List<PolicyViewModel> GetPolicesFormatTable(List<Policy> policies);
        Task<List<Policy>> GetPolicies();
        Task<Policy> GetPolicyById(string id);
    }
}
