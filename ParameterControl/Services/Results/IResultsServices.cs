using ParameterControl.Models.Filter;
using ParameterControl.Models.Result;

namespace ParameterControl.Services.Results
{
    public interface IResultsServices
    {
        Task<List<Result>> GetResults();
        Task<Result> GetResultsById(string id);
        Task<List<ResultViewModel>> GetFilterResults(FilterViewModel filterModel);
        Task<List<ResultViewModel>> GetResultsFormat(List<Result> results);
    }
}
