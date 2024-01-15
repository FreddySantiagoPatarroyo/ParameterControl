using ParameterControl.Models.Filter;
using ParameterControl.Models.Policy;
using modPolicy = ParameterControl.Models.Policy;

namespace ParameterControl.Services.Policies
{
    public interface IPoliciesServices
    {
        Task<List<PolicyViewModel>> GetFilterPolicies(FilterViewModel filterModel);
        Task<List<string>> GetOperationsType();
        List<PolicyViewModel> GetPolicesFormatTable(List<modPolicy.Policy> policies);
        Task<List<modPolicy.Policy>> GetPolicies();
        Task<modPolicy.Policy> GetPolicyById(string id);
        Task<string> InsertPolicy(modPolicy.Policy request);
    }
}
