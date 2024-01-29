﻿using Microsoft.Extensions.Configuration;
using ParameterControl.Auth.DataAccess;
using ParameterControl.Auth.Entities;
using ParameterControl.Auth.Interfaces;
using System.Data;

namespace ParameterControl.Auth.Impl
{
    public class AuthService : IAuthService
    {
        private readonly SetUser _setUser;
        private readonly RemoveUser _removeUser;
        private readonly ModifyUser _modifyUser;
        private readonly GetByIdUser _getByIdUser;
        private readonly GetAllUser _getAllUser;
        private readonly GetPaginatorUser _getPaginatorUser;

        public AuthService(IConfiguration configuration)
        {
            _setUser = new SetUser(configuration);
            _removeUser = new RemoveUser(configuration);
            _modifyUser = new ModifyUser(configuration);
            _getByIdUser = new GetByIdUser(configuration);
            _getAllUser = new GetAllUser(configuration);
            _getPaginatorUser = new GetPaginatorUser(configuration);
        }

        public async Task<int> InsertUser(UserModel entity)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _setUser.InsertUser(entity);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> DeleteUser(UserModel entity)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _removeUser.DeleteUser(entity);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<UserModel>> SelectAllUser()
        {
            try
            {
                List<UserModel> mapper = new List<UserModel>();
                return await Task.Run(async () =>
                {
                    var response = await _getAllUser.SelectAllUser();
                    mapper = await MapperUser(response);

                    return mapper;
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UserModel> SelectByIdUser(UserModel entity)
        {
            try
            {
                UserModel mapper = new UserModel();
                return await Task.Run(async () =>
                {
                    var response = await _getByIdUser.SelectByIdUser(entity);
                    foreach (DataRow row in response.Rows)
                    {
                        mapper = await MapperToUser(row);
                    }
                    return mapper;
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdateUser(UserModel entity)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _modifyUser.UpdateUser(entity);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<UserModel>> SelectPaginatorUser(int page, int row)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    var response = await _getPaginatorUser.SelectPaginatorUser(page, row);
                    var mapper = await MapperUser(response);
                    return mapper;
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<List<UserModel>> MapperUser(DataTable dt)
        {
            return await Task.Run(() =>
            {
                if (dt.Rows.Count > 0)
                {
                    List<UserModel> policies = new List<UserModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        policies.Add(MapperToUser(row).Result);
                    }
                    return policies;
                }
                else
                {
                    return new List<UserModel>();
                }
            });
        }

        private async Task<UserModel> MapperToUser(DataRow dr)
        {
            return await Task.Run(() =>
            {
                UserModel model = new UserModel
                {
                    Id = Convert.ToInt32(dr["COD_USUARIO"]),
                    User = dr["USUARIO"].ToString(),
                    Email = dr["EMAIL"].ToString(),
                    UserName = dr["NOMBRE_USUARIO"].ToString(),
                    RolId = Convert.ToInt32(dr["IDROL"]),
                    GroupId = dr["LOGINGROUP"].ToString(),
                    CreationDate = Convert.ToDateTime(dr["FECHA_CREACION"]),
                    ModifiedDate = Convert.ToDateTime(dr["FECHA_ACTUALIZACION"]),
                    ModifiedBy = dr["MODIFICADO_POR"].ToString(),
                    FirstAccess = Convert.ToInt32(dr["PRIMER_ACCESO"])
                };
                return model;
            });
        }
    }
}
