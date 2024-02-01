using ParameterControl.Parameter.Entities;

namespace ParameterControl.Parameter.Interfaces
{
    public interface IParameterService
    {
        Task<int> InsertParameter(ParameterModel entity);
        Task<int> UpdateParameter(ParameterModel entity);
        Task<int> DeleteParameter(ParameterModel entity);
        Task<List<ParameterModel>> SelectAllParameter();
        Task<ParameterModel> SelectByIdParameter(ParameterModel entity);
        Task<List<ParameterModel>> SelectPaginatorParameter(int page, int row);
        Task<int> SelectCountParameter();
    }
}
