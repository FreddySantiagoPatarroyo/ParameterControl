using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Models.User;
using modUser = ParameterControl.Models.User;


namespace ParameterControl.Services.Users
{
    public interface IUsersServices
    {
        Task<List<modUser.User>> GetUsers();
        Task<modUser.User> GetUsersByCode(int code);
        Task<List<UserViewModel>> GetFilterUsers(FilterViewModel filterModel);
        Task<List<UserViewModel>> GetUsersFormat(List<modUser.User> users);
        Task<UserViewModel> GetUserFormat(modUser.User user);
        Task<UserCreateViewModel> GetUserFormatCreate(modUser.User user);
        Task<string> InsertUser(modUser.User user);
        Task<string> UpdateUser(modUser.User user);
        Task<int> CountUsers();
        List<UserViewModel> GetFilterPagination(List<UserViewModel> inicialUsers, PaginationViewModel paginationViewModel, int totalData);
        Task<List<modUser.User>> GetUsersPagination(PaginationViewModel pagination);
        Task<List<modUser.User>> GetUsersFake();
        Task<string> DesactiveUser(modUser.User User);
        Task<string> ActiveUser(modUser.User User);
        Task<List<modUser.Role>> GetRoles();
    }
}
