using ParameterControl.Models.ApprovedResult;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;

namespace ParameterControl.Services.ApprovedResults
{
    public interface IApprovedResultsServices
    {
        Task<List<ApprovedResult>> GetApprovedResults();
        //Task<ApprovedResult> GetApprovedResultsById(string id);
        Task<List<ApprovedResultViewModel>> GetFilterApprovedResults(FilterViewModel filterModel);
        Task<List<ApprovedResultViewModel>> GetApprovedResultsFormat(List<ApprovedResult> results);
        Task<int> CountApprovedResults();
        Task<List<ApprovedResult>> GetAppovedResultsPagination(PaginationViewModel pagination);
        List<ApprovedResultViewModel> GetFilterPagination(List<ApprovedResultViewModel> inicialApprovedResults, PaginationViewModel paginationViewModel, int totalData);
    }
}
