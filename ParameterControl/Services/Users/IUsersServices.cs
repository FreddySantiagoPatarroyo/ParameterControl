using ParameterControl.Models.Filter;
using ParameterControl.Models.User;

namespace ParameterControl.Services.Users
{
    public interface IUsersServices
    {
        Task<List<User>> GetUsers();
        Task<User> GetUsersById(string id);
        Task<List<User>> GetFilterUsers(FilterViewModel filterModel);
    }
}
