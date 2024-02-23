using Microsoft.AspNetCore.Mvc.Rendering;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Models.Parameter;
using modParameter = ParameterControl.Models.Parameter;

namespace ParameterControl.Services.Parameters
{
    public interface IParametersService
    {
        Task<List<SelectListItem>> GetParameterType();
        Task<List<modParameter.Parameter>> GetListParameter();
        Task<List<modParameter.Parameter>> GetParameters();
        Task<modParameter.Parameter> GetParameterByCode(int code);
        Task<List<ParameterViewModel>> GetFilterParameters(FilterViewModel filterModel);
        Task<List<ParameterViewModel>> GetParametersFormat(List<modParameter.Parameter> parameters);
        Task<ParameterViewModel> GetParameterFormat(modParameter.Parameter parameter);
        Task<ParameterCreateViewModel> GetParameterFormatCreate(modParameter.Parameter parameter);
        Task<string> InsertParameter(modParameter.Parameter parameter);
        Task<string> UpdateParameter(modParameter.Parameter parameter);
        Task<int> CountParameters();
        Task<List<modParameter.Parameter>> GetParametersPagination(PaginationViewModel pagination);
        List<ParameterViewModel> GetFilterPagination(List<ParameterViewModel> inicialParameters, PaginationViewModel paginationViewModel, int totalData);
        Task<string> ActiveParameter(modParameter.Parameter Parameter);
        Task<string> DesactiveParameter(modParameter.Parameter Parameter);
        Task<List<Models.Conciliation.Conciliation>> GetConciliations();
        Task<List<modParameter.Parameter>> GetParametersByConciliation(string conciliation);
    }
}
