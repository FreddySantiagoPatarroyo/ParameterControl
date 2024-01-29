using ParameterControl.Auth.Entities;

namespace ParameterControl.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<int> InsertUser(UserModel entity);
        Task<int> UpdateUser(UserModel entity);
        Task<int> DeleteUser(UserModel entity);
        Task<List<UserModel>> SelectAllUser();
        Task<UserModel> SelectByIdUser(UserModel entity);
        Task<List<UserModel>> SelectPaginatorUser(int page, int row);
    }
}
