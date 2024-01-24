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
        Task<Parameter> GetParameterById(string id);
        Task<List<ParameterViewModel>> GetFilterParameters(FilterViewModel filterModel);
        Task<List<ParameterViewModel>> GetParametersFormat(List<Parameter> parameters);
    }
}
