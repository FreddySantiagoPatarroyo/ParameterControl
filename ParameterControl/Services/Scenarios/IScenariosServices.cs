using Microsoft.AspNetCore.Mvc.Rendering;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Scenery;

using modConciliation = ParameterControl.Models.Conciliation;
using modScenery = ParameterControl.Models.Scenery;

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
        Task<SceneryViewModel> GetSceneryFormat(modScenery.Scenery scenery);
        Task<SceneryCreateViewModel> GetSceneryFormatCreate(modScenery.Scenery scenery);
    }
}
