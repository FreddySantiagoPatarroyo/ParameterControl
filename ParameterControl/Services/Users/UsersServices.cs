using ParameterControl.Models.User;
using ParameterControl.Models.Filter;
using ParameterControl.Models.User;
using modUser = ParameterControl.Models.User;

namespace ParameterControl.Services.Users
{
    public class UsersServices: IUsersServices
    {
        private List<User> users = new List<User>();
        public UsersServices()
        {
            users = new List<User>()
            {
                new User(){
                   Id = "1",
                   Code = "COD_001",
                   User_ = "EjemploUsuario",
                   Email = "ejemplo@gmail.com",
                   Name = "name1",
                   CreationDate = DateTime.Parse("2024-01-10"),
                   UpdateDate = DateTime.Parse("2023-11-09"),
                   State = true
                },
                 new User(){
                   Id = "2",
                   Code = "COD_002",
                   User_ = "EjemploUsuario",
                   Email = "ejemplo@gmail.com",
                   Name = "name2",
                   CreationDate = DateTime.Parse("2024-01-10"),
                   UpdateDate = DateTime.Parse("2023-11-09"),
                   State = true
                },
                  new User(){
                   Id = "3",
                   Code = "COD_003",
                   User_ = "EjemploUsuario",
                   Email = "ejemplo@gmail.com",
                   Name = "name3",
                   CreationDate = DateTime.Parse("2024-01-10"),
                   UpdateDate = DateTime.Parse("2023-11-09"),
                   State = false
                },
                   new User(){
                   Id = "4",
                   Code = "COD_004",
                   User_ = "EjemploUsuario1",
                   Email = "ejemplo@gmail.com",
                   Name = "name4",
                   CreationDate = DateTime.Parse("2024-01-10"),
                   UpdateDate = DateTime.Parse("2023-11-09"),
                   State = false
                },
                    new User(){
                   Id = "5",
                   Code = "COD_005",
                   User_ = "EjemploUsuario1",
                   Email = "ejemplo@gmail.com",
                   Name = "name5",
                   CreationDate = DateTime.Parse("2024-01-10"),
                   UpdateDate = DateTime.Parse("2023-11-09"),
                   State = true
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

                userModel.Id = user.Id;
                userModel.Code = user.Code;
                userModel.User_ = user.User_;
                userModel.Email = user.Email;
                userModel.Name = user.Name;
                userModel.State = user.State;
                userModel.StateFormat = user.State ? "Activo" : "Inactivo";
                userModel.CreationDate = user.CreationDate;
                userModel.UpdateDate = user.UpdateDate;

                UsersModel.Add(userModel);
            }

            return UsersModel;
        }

        public async Task<User> GetUsersById(string id)
        {
            User user = users.Find(user => user.Id == id);
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

