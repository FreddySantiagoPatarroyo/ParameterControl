using ParameterControl.Models.Indicator;
using ParameterControl.Models.Filter;

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
