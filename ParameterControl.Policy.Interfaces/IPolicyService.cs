using ParameterControl.Policy.Entities;

namespace ParameterControl.Policy.Interfaces
{
    public interface IPolicyService
    {
        Task<int> InsertPolicy(PolicyModel entity);
        Task<int> UpdatePolicy(PolicyModel entity);
        Task<int> DeletePolicy(PolicyModel entity);
        Task<int> SelectAllPolicy();
        Task<int> SelectByIdPolicy(PolicyModel entity);
        Task<List<PolicyModel>> SelectPaginatorPolicy(int page, int row);
    }
}
