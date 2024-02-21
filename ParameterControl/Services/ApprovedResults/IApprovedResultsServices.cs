using ParameterControl.Models.ApprovedResult;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Result;

namespace ParameterControl.Services.ApprovedResults
{
    public interface IApprovedResultsServices
    {
        Task<List<ApprovedResult>> GetApprovedResults();
        Task<ApprovedResult> GetApprovedResultsById(string id);
        Task<List<ApprovedResultViewModel>> GetFilterApprovedResults(FilterViewModel filterModel);
        Task<List<ApprovedResultViewModel>> GetApprovedResultsFormat(List<ApprovedResult> results);
    }
}
