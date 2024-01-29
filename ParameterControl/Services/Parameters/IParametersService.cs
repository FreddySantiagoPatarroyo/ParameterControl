using Microsoft.AspNetCore.Mvc.Rendering;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Parameter;

namespace ParameterControl.Services.Parameters
{
    public interface IParametersService
    {
        Task<List<SelectListItem>> GetParameterType();
        Task<List<Parameter>> GetListParameter();
        Task<List<Parameter>> GetParameters();
        Task<Parameter> GetParameterByCode(int code);
        Task<List<ParameterViewModel>> GetFilterParameters(FilterViewModel filterModel);
        Task<List<ParameterViewModel>> GetParametersFormat(List<Parameter> parameters);
        Task<ParameterViewModel> GetParameterFormat(Parameter parameter);
        Task<ParameterCreateViewModel> GetParameterFormatCreate(Parameter parameter);
    }
}
