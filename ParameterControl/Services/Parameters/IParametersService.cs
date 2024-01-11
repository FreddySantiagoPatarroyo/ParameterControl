using ParameterControl.Models.Filter;
using ParameterControl.Models.Parameter;

namespace ParameterControl.Services.Parameters
{
    public interface IParametersService
    {
        Task<List<string>> GetParameterType();
        Task<List<string>> GetListParameter();
        Task<List<Parameter>> GetParameters();
        Task<Parameter> GetParameterById(string id);
        Task<List<Parameter>> GetFilterParameters(FilterViewModel filterModel);
    }
}
