using Microsoft.AspNetCore.Mvc.Rendering;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Scenery;
using modConciliation = ParameterControl.Models.Conciliation;

namespace ParameterControl.Services.Scenarios
{
    public interface IScenariosServices
    {
        Task<List<SelectListItem>> GetImpact();
        Task<List<modConciliation.Conciliation>> GetConciliation();
        Task<List<Scenery>> GetScenarios();
        Task<Scenery> GetSceneryByCode(int code);
        Task<List<SceneryViewModel>> GetFilterScenarios(FilterViewModel filterModel);
        Task<List<SceneryViewModel>> GetScenariosFormat(List<Scenery> scenarios);
        Task<SceneryViewModel> GetSceneryFormat(Scenery scenery);
        Task<SceneryCreateViewModel> GetSceneryFormatCreate(Scenery scenery);
        Task<string> InsertScenery(Scenery request);
        Task<string> UpdateScenery(Scenery Conciliation);
        Task<int> CountScenery();
    }
}
