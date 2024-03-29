﻿using ParameterControl.User.Entities;

namespace ParameterControl.User.Interfaces
{
    public interface IUserService
    {
        Task<int> InsertUser(UserModel entity);
        Task<int> UpdateUser(UserModel entity);
        Task<int> DeleteUser(UserModel entity);
        Task<List<UserModel>> SelectAllUser();
        Task<UserModel> SelectByIdUser(UserModel entity);
        Task<List<UserModel>> SelectPaginatorUser(int page, int row);
        Task<int> SelectCountUser();
        Task<List<RoleModel>> SelectAllRole();
    }
}
