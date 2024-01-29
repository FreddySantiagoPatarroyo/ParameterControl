using ParameterControl.Models.Filter;
using ParameterControl.Models.Indicator;

namespace ParameterControl.Services.Indicators
{
    public interface IIndicatorsService
    {
        Task<List<Indicator>> GetIndicators();
        Task<Indicator> GetIndicatorsById(string id);
        Task<List<IndicatorViewModel>> GetFilterIndicators(FilterViewModel filterModel);
        Task<List<IndicatorViewModel>> GetindicatorsFormat(List<Indicator> indicators);
    }
}
