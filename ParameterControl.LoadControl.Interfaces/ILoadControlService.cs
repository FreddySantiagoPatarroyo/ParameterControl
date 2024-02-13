

using ParameterControl.LoadControl.Entities;

namespace ParameterControl.LoadControl.Interfaces
{
    public interface ILoadControlService
    {
        Task<int> UpdateLoadControl(LoadControlModel entity);
        Task<List<LoadControlModel>> SelectAllLoadControl();
        Task<List<LoadControlModel>> SelectPaginatorLoadControl(int page, int row);
        Task<LoadControlModel> SelectByIdLoadControl(LoadControlModel entity);
    }
}
