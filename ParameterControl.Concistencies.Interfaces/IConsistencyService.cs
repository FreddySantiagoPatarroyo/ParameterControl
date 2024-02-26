using ParameterControl.Consistency.Entities;

namespace ParameterControl.Consistency.Interfaces
{
    public interface IConsistencyService
    {
        Task<List<ConsistencyModel>> SelectAllConsistency();
        Task<List<ConsistencyModel>> SelectPaginatorConsistency(int page, int row);
    }
}
