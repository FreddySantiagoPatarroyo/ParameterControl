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
                    UserOwner = 1
                },
                 new User(){
                   Code = 2,
                   User_ = "EjemploUsuario",
                   Email = "ejemplo@gmail.com",
                   Name = "name2",
                   CreationDate = DateTime.Parse("2024-01-10"),
                   UpdateDate = DateTime.Parse("2023-11-09"),
                   State = true,
                    UserOwner = 1
                },
                  new User(){
                   Code = 3,
                   User_ = "EjemploUsuario",
                   Email = "ejemplo@gmail.com",
                   Name = "name3",
                   CreationDate = DateTime.Parse("2024-01-10"),
                   UpdateDate = DateTime.Parse("2023-11-09"),
                   State = false,
                    UserOwner = 1
                },
                   new User(){
                   Code = 4,
                   User_ = "EjemploUsuario1",
                   Email = "ejemplo@gmail.com",
                   Name = "name4",
                   CreationDate = DateTime.Parse("2024-01-10"),
                   UpdateDate = DateTime.Parse("2023-11-09"),
                   State = false,
                    UserOwner = 1
                },
                    new User(){
                   Code = 5,
                   User_ = "EjemploUsuario1",
                   Email = "ejemplo@gmail.com",
                   Name = "name5",
                   CreationDate = DateTime.Parse("2024-01-10"),
                   UpdateDate = DateTime.Parse("2023-11-09"),
                   State = true,
                   UserOwner = 1
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
            List<User> UsersFilter = new List<User>();

            if ((filterModel.ColumValue == null || filterModel.ColumValue == "" || filterModel.ValueFilter == null || filterModel.ValueFilter == ""))
            {
                UsersFilter = await GetUsers();
            }
            else
            {
                switch (filterModel.ColumValue)
                {
                    case "Code":
                        UsersFilter = applyFilter(filterModel);
                        break;
                    case "User_":
                        UsersFilter = applyFilter(filterModel);
                        break;
                    case "Email":
                        UsersFilter = applyFilter(filterModel);
                        break;
                    case "Name":
                        UsersFilter = applyFilter(filterModel);
                        break;
                    case "CreationDate":
                        UsersFilter = applyFilter(filterModel);
                        break;
                    case "UpdateDate":
                        UsersFilter = applyFilter(filterModel);
                        break;
                    default:
                        break;
                }
            }

            return await GetUsersFormat(UsersFilter);
        }

        private List<User> applyFilter(FilterViewModel filterModel)
        {
            var property = typeof(User).GetProperty(filterModel.ColumValue);

            List<User> UsersFilter = new List<User>();

            foreach (User User in users)
            {
                if (property.GetValue(User).ToString().ToUpper().Contains(filterModel.ValueFilter.ToUpper()))
                {
                    UsersFilter.Add(User);
                }
            }

            return UsersFilter;
        }

    }
}

