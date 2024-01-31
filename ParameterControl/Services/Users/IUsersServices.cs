using ParameterControl.Models.Filter;
using ParameterControl.Models.User;
using modUser = ParameterControl.Models.User;


namespace ParameterControl.Services.Users
{
    public interface IUsersServices
    {
        Task<List<User>> GetUsers();
        Task<User> GetUsersByCode(int code);
        Task<List<UserViewModel>> GetFilterUsers(FilterViewModel filterModel);
        Task<List<UserViewModel>> GetUsersFormat(List<User> users);
        Task<UserViewModel> GetUserFormat(modUser.User user);
        Task<UserCreateViewModel> GetUserFormatCreate(modUser.User user);
        Task<string> InsertUser(modUser.User user);
        Task<string> UpdateUser(modUser.User user);
        Task<int> CountUsers();
    }
}
