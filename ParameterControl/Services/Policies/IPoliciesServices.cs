using Microsoft.AspNetCore.Mvc.Rendering;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Models.Policy;
using modPolicy = ParameterControl.Models.Policy;

namespace ParameterControl.Services.Policies
{
    public interface IPoliciesServices
    {
        Task<int> CountPolicies();
        Task<List<PolicyViewModel>> GetFilterPolicies(FilterViewModel filterModel);
        Task<List<SelectListItem>> GetOperationsType();
        Task<List<PolicyViewModel>> GetPolicesFormat(List<modPolicy.Policy> policies);
        Task<List<modPolicy.Policy>> GetPolicies();
        Task<List<modPolicy.Policy>> GetPoliciesFake();
        Task<List<modPolicy.Policy>> GetPoliciesPagination(PaginationViewModel pagination);
        Task<modPolicy.Policy> GetPolicyByCode(int code);
        Task<PolicyViewModel> GetPolicyFormat(modPolicy.Policy policy);
        Task<PolicyCreateViewModel> GetPolicyFormatCreate(modPolicy.Policy policy);
        Task<string> InsertPolicy(modPolicy.Policy request);
        Task<string> UpdatePolicy(modPolicy.Policy policy);
    }
}
