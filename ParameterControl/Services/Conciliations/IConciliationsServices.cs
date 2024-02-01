using Microsoft.AspNetCore.Mvc.Rendering;
using ParameterControl.Models.Conciliation;
using ParameterControl.Models.Filter;
using modConciliation = ParameterControl.Models;


namespace ParameterControl.Services.Conciliations
{
    public interface IConciliationsServices
    {
        Task<List<modConciliation.Policy.Policy>> GetPolicies();
        Task<List<SelectListItem>> GetRequired();
        Task<List<modConciliation.Conciliation.Conciliation>> GetConciliations();
        Task<modConciliation.Conciliation.Conciliation> GetConciliationsByCode(int code);
        Task<List<ConciliationViewModel>> GetFilterConciliations(FilterViewModel filterModel);
        Task<List<ConciliationViewModel>> GetConciliationsFormat(List<modConciliation.Conciliation.Conciliation> conciliations);
        Task<ConciliationViewModel> GetConciliationFormat(modConciliation.Conciliation.Conciliation conciliation);
        Task<ConciliationCreateViewModel> GetConciliationFormatCreate(modConciliation.Conciliation.Conciliation conciliation);
        Task<List<SelectListItem>> GetOperationsType();
        Task<string> InsertConciliation(modConciliation.Conciliation.Conciliation request);
        Task<string> UpdateConciliation(modConciliation.Conciliation.Conciliation Conciliation);
        Task<int> CountConciliations();
    }
}
