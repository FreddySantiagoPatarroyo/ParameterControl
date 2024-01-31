using ParameterControl.Conciliation.Entities;

namespace ParameterControl.Conciliation.Interfaces
{
    public interface IConciliationService
    {
        Task<int> InsertConciliation(ConciliationModel entity);
        Task<int> UpdateConciliation(ConciliationModel entity);
        Task<int> DeleteConciliation(ConciliationModel entity);
        Task<List<ConciliationModel>> SelectAllConciliation();
        Task<ConciliationModel> SelectByIdConciliation(ConciliationModel entity);
        Task<List<ConciliationModel>> SelectPaginatorConciliation(int page, int row);
        Task<int> SelectCountConciliation();
    }
}
