using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Models.Result;

namespace ParameterControl.Services.Results
{
    public interface IResultsServices
    {
        Task<List<Result>> GetResults();
        //Task<Result> GetResultsById(string id);
        Task<List<ResultViewModel>> GetFilterResults(FilterViewModel filterModel);
        Task<List<ResultViewModel>> GetResultsFormat(List<Result> results);
        Task<int> CountResults();
        Task<List<Result>> GetResultsPagination(PaginationViewModel pagination);
        List<ResultViewModel> GetFilterPagination(List<ResultViewModel> inicialResults, PaginationViewModel paginationViewModel, int totalData);
        Task<Result> GetOneResult(int conciliationSK, int stageSK, DateTime uploadDate);
        Task<string> UpdateAmountBenefitResult(Result result);
        Task<string> UpdateAmountImpactResult(Result result);
    }
}
