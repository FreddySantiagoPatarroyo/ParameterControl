using ParameterControl.User.Entities;
using ParameterControl.User.Impl;
using ParameterControl.User.Interfaces;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Models.Policy;
using ParameterControl.Models.User;
using ParameterControl.Policy.Entities;
using modUser = ParameterControl.Models.User;

namespace ParameterControl.Services.Users
{
    public class UsersServices : IUsersServices
    {
        private List<modUser.User> users = new List<modUser.User>();
        private readonly IUserService _userService;

        public UsersServices(IConfiguration configuration)
        {
            _userService = new UserService(configuration);
            users = new List<modUser.User>()
            {
                new modUser.User(){
                   Code = 1,
                   User_ = "EjemploUsuario",
                   Email = "ejemplo@gmail.com",
                   Name = "name1",
                   CreationDate = DateTime.Parse("2024-01-10"),
                   UpdateDate = DateTime.Parse("2023-11-09"),
                   State = true,
                    UserOwner = "User1"
                },
                 new modUser.User(){
                   Code = 2,
                   User_ = "EjemploUsuario",
                   Email = "ejemplo@gmail.com",
                   Name = "name2",
                   CreationDate = DateTime.Parse("2024-01-10"),
                   UpdateDate = DateTime.Parse("2023-11-09"),
                   State = true,
                    UserOwner = "User1"
                }
            };
        }

        public async Task<List<modUser.User>> GetUsersFake()
        {
            return users;
        }

        public async Task<List<modUser.User>> GetUsers()
        {
            var collectionUsers = await _userService.SelectAllUser();
            var response = await MapperUser(collectionUsers);
            return response;
        }

        public async Task<int> CountUsers()
        {
            var collectionUsers = await _userService.SelectAllUser();
            return collectionUsers.Count();
        }

        public async Task<List<modUser.User>> GetUsersPagination(PaginationViewModel pagination)
        {
            try
            {
                var response = await _userService.SelectPaginatorUser(pagination.Page, pagination.RecordsPage);
                var result = await MapperUser(response);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
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

        public async Task<modUser.User> GetUsersByCode(int code)
        {
            var response = await _userService.SelectByIdUser(new UserModel { Code = code });
            var user = await MapperToUser(response);
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

        public List<UserViewModel>GetFilterPagination(List<UserViewModel> inicialUsers, PaginationViewModel paginationViewModel, int totalData)
        {
            var limit = paginationViewModel.Page * paginationViewModel.RecordsPage;
            var index = limit - paginationViewModel.RecordsPage;
            var count = 0;

            if (limit > totalData)
            {
                count = totalData - index;
            }
            else
            {
                count = paginationViewModel.RecordsPage;
            }

            List<UserViewModel> usersFilterPagination = inicialUsers.GetRange(index, count);

            return usersFilterPagination;
        }

        public async Task<string> InsertUser(modUser.User request)
        {
            try
            {
                var user = new User.Entities.UserModel
                {
                    UserName = request.Name,
                    Email = request.Email,
                    User = request.UserOwner
                };

                var response = await _userService.InsertUser(user);

                return response.Equals(1) ? "Usuario creado correctamente" : "Error creando el usuario";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<List<modUser.User>> MapperUser(List<UserModel> userModel)
        {
            return await Task.Run(() =>
            {
                List<modUser.User> users = new List<modUser.User>();
                if (userModel.Count > 0)
                {
                    foreach (var user in userModel)
                    {
                        users.Add(MapperToUser(user).Result);
                    }
                }
                return users;
            });
        }

        private async Task<modUser.User> MapperToUser(UserModel User)
        {
            return await Task.Run(() =>
            {
                modUser.User model = new modUser.User
                {
                    Code = Convert.ToInt32(User.Code),
                    User_ = User.User,
                    Email = User.Email,
                    Name = User.UserName,
                    CreationDate = User.CreationDate,
                    UpdateDate = User.ModifiedDate,
                    UserOwner = User.ModifiedBy
                };
                return model;
            });
        }

        public async Task<string> UpdateUser(modUser.User User)
        {
            var mapping = await MapperUpdateUser(User);
            var response = await _userService.UpdateUser(mapping);

            return response.Equals(1) ? "Usuario actualizado correctamente" : "Error actualizando el usuario";
        }

        private async Task<UserModel> MapperUpdateUser(modUser.User User)
        {
            return await Task.Run(() =>
            {
                UserModel model = new UserModel
                {
                    Code = User.Code,
                    UserName = User.Name
                };
                return model;
            });
        }

        public async Task<modUser.User> GetByEmailAndUserName(string userName, string email)
        {
            var collectionUsers = await _userService.SelectAllUser();
            var response = await MapperUser(collectionUsers);
            var user = response.FirstOrDefault(x => x.User_.Equals(userName) && x.Email.Equals(email));
            return user;
        }
    }
}

