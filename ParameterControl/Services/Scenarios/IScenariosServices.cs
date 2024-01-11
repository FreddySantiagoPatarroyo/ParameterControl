using ParameterControl.Models.Filter;
using ParameterControl.Models.Scenery;

namespace ParameterControl.Services.Scenarios
{
    public interface IScenariosServices
    {
        Task<List<string>> GetImpact();
        Task<List<string>> GetConciliation();
        Task<List<Scenery>> GetScenarios();
        Task<Scenery> GetSceneryById(string id);
        Task<List<Scenery>> GetFilterScenarios(FilterViewModel filterModel);
    }
}
