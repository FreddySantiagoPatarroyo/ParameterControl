using ParameterControl.Models.User;
using ParameterControl.Models.Filter;

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
                   CodeUser = "COD_001",
                   User_ = "EjemploUsuario",
                   Email = "ejemplo@gmail.com",
                   CreationDate = DateTime.Parse("2024-01-10"),
                   UpdateDate = DateTime.Parse("2023-11-09"),
                   State = true
                },
                 new User(){
                   Id = "2",
                   CodeUser = "COD_002",
                   User_ = "EjemploUsuario",
                   Email = "ejemplo@gmail.com",
                   CreationDate = DateTime.Parse("2024-01-10"),
                   UpdateDate = DateTime.Parse("2023-11-09"),
                   State = true
                },
                  new User(){
                   Id = "3",
                   CodeUser = "COD_003",
                   User_ = "EjemploUsuario",
                   Email = "ejemplo@gmail.com",
                   CreationDate = DateTime.Parse("2024-01-10"),
                   UpdateDate = DateTime.Parse("2023-11-09"),
                   State = false
                },
                   new User(){
                   Id = "4",
                   CodeUser = "COD_004",
                   User_ = "EjemploUsuario1",
                   Email = "ejemplo@gmail.com",
                   CreationDate = DateTime.Parse("2024-01-10"),
                   UpdateDate = DateTime.Parse("2023-11-09"),
                   State = false
                },
                    new User(){
                   Id = "5",
                   CodeUser = "COD_005",
                   User_ = "EjemploUsuario1",
                   Email = "ejemplo@gmail.com",
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

        public async Task<User> GetUsersById(string id)
        {
            User user = users.Find(user => user.Id == id);
            return user;
        }

        public async Task<List<User>> GetFilterUsers(FilterViewModel filterModel)
        {
            List<User> UsersFilter = new List<User>();

            if ((filterModel.ColumValue == null || filterModel.ColumValue == "" || filterModel.ValueFilter == null || filterModel.ValueFilter == ""))
            {
                UsersFilter = users;
            }
            else
            {
                switch (filterModel.ColumValue)
                {
                    case "CodeUser":
                        UsersFilter = applyFilter(filterModel);
                        break;
                    case "User_":
                        UsersFilter = applyFilter(filterModel);
                        break;
                    case "Email":
                        UsersFilter = applyFilter(filterModel);
                        break;
                    case "NameUser":
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

            return UsersFilter;
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

