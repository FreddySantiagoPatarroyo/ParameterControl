using ParameterControl.Models.Filter;
using ParameterControl.Models.Parameter;

namespace ParameterControl.Services.Parameters
{
    public interface IParametersService
    {
        Task<List<Parameter>> GetParameters();
        Task<Parameter> GetParameterById(string id);
    }
}
