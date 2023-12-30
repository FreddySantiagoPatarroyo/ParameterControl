using ParameterControl.Models.Filter;
using ParameterControl.Models.Scenery;

namespace ParameterControl.Services.Scenarios
{
    public interface IScenariosServices
    {
        Task<List<Scenery>> GetScenarios();
        Task<Scenery> GetSceneryById(string id);
    }
}
