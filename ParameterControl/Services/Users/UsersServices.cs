using ParameterControl.Models.Filter;
using ParameterControl.Models.User;
using modUser = ParameterControl.Models.User;

namespace ParameterControl.Services.Users
{
    public class UsersServices : IUsersServices
    {
        private List<User> users = new List<User>();
        public UsersServices()
        {
            users = new List<User>()
            {
                new User(){
                   Code = 1,
                   User_ = "EjemploUsuario",
                   Email = "ejemplo@gmail.com",
                   Name = "name1",
                   CreationDate = DateTime.Parse("2024-01-10"),
                   UpdateDate = DateTime.Parse("2023-11-09"),
                   State = true,
                    UserOwner = "User1"
                },
                 new User(){
                   Code = 2,
                   User_ = "EjemploUsuario",
                   Email = "ejemplo@gmail.com",
                   Name = "name2",
                   CreationDate = DateTime.Parse("2024-01-10"),
                   UpdateDate = DateTime.Parse("2023-11-09"),
                   State = true,
                    UserOwner = "User1"
                },
                  new User(){
                   Code = 3,
                   User_ = "EjemploUsuario",
                   Email = "ejemplo@gmail.com",
                   Name = "name3",
                   CreationDate = DateTime.Parse("2024-01-10"),
                   UpdateDate = DateTime.Parse("2023-11-09"),
                   State = false,
                    UserOwner = "User1"
                },
                   new User(){
                   Code = 4,
                   User_ = "EjemploUsuario1",
                   Email = "ejemplo@gmail.com",
                   Name = "name4",
                   CreationDate = DateTime.Parse("2024-01-10"),
                   UpdateDate = DateTime.Parse("2023-11-09"),
                   State = false,
                    UserOwner = "User1"
                },
                    new User(){
                   Code = 5,
                   User_ = "EjemploUsuario1",
                   Email = "ejemplo@gmail.com",
                   Name = "name5",
                   CreationDate = DateTime.Parse("2024-01-10"),
                   UpdateDate = DateTime.Parse("2023-11-09"),
                   State = true,
                   UserOwner = "User1"
                },

            };
        }

        public async Task<List<User>> GetUsers()
        {
            return users;
        }

        public async Task<List<UserViewModel>> GetUsersFormat(List<modUser.User> users)
        {
            List<UserViewModel> UsersModel = new List<UserViewModel>();

            foreach (modUser.User user in users)
            {
                UserViewModel userModel = new UserViewModel();

                userModel.Code = user.Code;
                userModel.User_ = user.User_;
                userModel.Email = user.Email;
                userModel.Name = user.Name;
                userModel.State = user.State;
                userModel.CodeFormat = "COD_" + user.Code;
                userModel.StateFormat = user.State ? "Activo" : "Inactivo";
                userModel.CreationDate = user.CreationDate;
                userModel.UpdateDate = user.UpdateDate;
                userModel.CreationDateFormat = user.CreationDate.ToString("dd/MM/yyyy");
                userModel.UpdateDateFormat = user.UpdateDate.ToString("dd/MM/yyyy");

                UsersModel.Add(userModel);
            }

            return UsersModel;
        }

        public async Task<UserViewModel> GetUserFormat(modUser.User user)
        {
            UserViewModel userModel = new UserViewModel();

            userModel.Code = user.Code;
            userModel.User_ = user.User_;
            userModel.Email = user.Email;
            userModel.Name = user.Name;
            userModel.State = user.State;
            userModel.CodeFormat = "COD_" + user.Code;
            userModel.StateFormat = user.State ? "Activo" : "Inactivo";
            userModel.CreationDate = user.CreationDate;
            userModel.UpdateDate = user.UpdateDate;
            userModel.CreationDateFormat = user.CreationDate.ToString("dd/MM/yyyy");
            userModel.UpdateDateFormat = user.UpdateDate.ToString("dd/MM/yyyy");

            return userModel;
        }

        public async Task<UserCreateViewModel> GetUserFormatCreate(modUser.User user)
        {
            UserCreateViewModel userModel = new UserCreateViewModel();

            userModel.Code = user.Code;
            userModel.User_ = user.User_;
            userModel.Email = user.Email;
            userModel.Name = user.Name;
            userModel.State = user.State;
            userModel.CodeFormat = "COD_" + user.Code;
            userModel.CreationDate = user.CreationDate;
            userModel.UpdateDate = user.UpdateDate;

            return userModel;
        }

        public async Task<User> GetUsersByCode(int code)
        {
            User user = users.Find(user => user.Code == code);
            return user;
        }

        public async Task<List<UserViewModel>> GetFilterUsers(FilterViewModel filterModel)
        {
            List<modUser.User> allUsers = await GetUsers();
            List<UserViewModel> usersFilter = await GetUsersFormat(allUsers);

            if (filterModel.ColumValue == null || filterModel.ColumValue == "" || filterModel.ValueFilter == null || filterModel.ValueFilter == "")
            {
                return usersFilter;
            }
            else
            {
                usersFilter = await applyFilter(filterModel, usersFilter);
            }

            return usersFilter;
        }

        private async Task<List<UserViewModel>> applyFilter(FilterViewModel filterModel, List<UserViewModel> allUsers)
        {
            Console.WriteLine(filterModel.TypeRow.ToString());

            var property = typeof(UserViewModel).GetProperty(filterModel.ColumValue);

            List<UserViewModel> usersFilter = new List<UserViewModel>();
            if (filterModel.TypeRow == "Select")
            {
                foreach (UserViewModel user in allUsers)
                {
                    if (property.GetValue(user).ToString().ToUpper() == filterModel.ValueFilter.ToUpper())
                    {
                        usersFilter.Add(user);
                    }
                }
            }
            else
            {
                foreach (UserViewModel user in allUsers)
                {
                    if (property.GetValue(user).ToString().ToUpper().Contains(filterModel.ValueFilter.ToUpper()))
                    {
                        usersFilter.Add(user);
                    }
                }
            }
            return usersFilter;
        }
    }
}

