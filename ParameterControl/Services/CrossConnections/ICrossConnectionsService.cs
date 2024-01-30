using ParameterControl.Models.CrossConnection;
using ParameterControl.Models.Filter;

namespace ParameterControl.Services.CrossConnections
{
    public interface ICrossConnectionsService
    {
        Task<CrossConnection> GetCrossConnectionByCode(int code);
        Task<CrossConnectionViewModel> GetCrossConnectionFormat(CrossConnection crossConnection);
        Task<List<CrossConnection>> GetCrossConnections();
        Task<List<CrossConnectionViewModel>> GetCrossConnectionsFormat(List<CrossConnection> crossConnections);
        Task<List<CrossConnectionViewModel>> GetFilterCrossConnections(FilterViewModel filterModel);
    }
}
