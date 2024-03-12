using Microsoft.Extensions.Configuration;
using ParameterControl.User.DataAccess;
using ParameterControl.User.Entities;
using ParameterControl.User.Interfaces;
using System.Data;

namespace ParameterControl.User.Impl
{
    public class UserService : IUserService
    {
        private readonly SetUser _setUser;
        private readonly RemoveUser _removeUser;
        private readonly ModifyUser _modifyUser;
        private readonly GetByIdUser _getByIdUser;
        private readonly GetAllUser _getAllUser;
        private readonly GetPaginatorUser _getPaginatorUser;
        private readonly CountUser _getCountUser;
        private readonly GetAllRole _getAllRole;

        public UserService(IConfiguration configuration)
        {
            _setUser = new SetUser(configuration);
            _removeUser = new RemoveUser(configuration);
            _modifyUser = new ModifyUser(configuration);
            _getByIdUser = new GetByIdUser(configuration);
            _getAllUser = new GetAllUser(configuration);
            _getPaginatorUser = new GetPaginatorUser(configuration);
            _getCountUser = new CountUser(configuration);
            _getAllRole = new GetAllRole(configuration);
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
                    List<UserModel> users = new List<UserModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        users.Add(MapperToUser(row).Result);
                    }
                    return users;
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
                    Code = Convert.ToInt32(dr["COD_USUARIO"]),
                    User = dr["USUARIO"] is DBNull ? string.Empty : dr["USUARIO"].ToString(),
                    Email = dr["EMAIL"] is DBNull ? string.Empty : dr["EMAIL"].ToString(),
                    UserName = dr["NOMBRE_USUARIO"] is DBNull ? string.Empty : dr["NOMBRE_USUARIO"].ToString(),
                    RolName = dr["DESCRIPCION_ROL"] is DBNull ? string.Empty : dr["DESCRIPCION_ROL"].ToString(),
                    Password = dr["CONTRASEÑA"] is DBNull ? string.Empty : dr["CONTRASEÑA"].ToString(),
                    RolId = dr["ID_ROL"] is DBNull ? 0 : Convert.ToInt32(dr["ID_ROL"]),
                    CreationDate = dr["FECHA_CREACION"] is DBNull ? DateTime.Now : Convert.ToDateTime(dr["FECHA_CREACION"]),
                    ModifiedDate = dr["FECHA_ACTUALIZACION"] is DBNull ? DateTime.Now : Convert.ToDateTime(dr["FECHA_ACTUALIZACION"]),
                    FirstAccess = dr["PRIMER_ACCESO"] is DBNull ? false : Convert.ToBoolean(Convert.ToInt32(dr["PRIMER_ACCESO"])),
                    State = dr["ESTADO_USUARIO"] is DBNull ? false : Convert.ToBoolean(Convert.ToInt32(dr["ESTADO_USUARIO"])),
                };
                return model;
            });
        }

        public async Task<int> SelectCountUser()
        {
            try
            {
                return await _getCountUser.SelectCountUser();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UserModel> SelectByEmailAndUserName(UserModel entity)
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

        public async Task<List<RoleModel>> SelectAllRole()
        {
            try
            {
                List<RoleModel> mapper = new List<RoleModel>();
                return await Task.Run(async () =>
                {
                    var response = await _getAllRole.SelectAllRole();
                    mapper = await MapperRole(response);

                    return mapper;
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<List<RoleModel>> MapperRole(DataTable dt)
        {
            return await Task.Run(() =>
            {
                if (dt.Rows.Count > 0)
                {
                    List<RoleModel> roles = new List<RoleModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        roles.Add(MapperToRole(row).Result);
                    }
                    return roles;
                }
                else
                {
                    return new List<RoleModel>();
                }
            });
        }

        private async Task<RoleModel> MapperToRole(DataRow dr)
        {
            return await Task.Run(() =>
            {
                RoleModel model = new RoleModel
                {
                    Code = dr["ID_ROL"] is DBNull ? 0 : Convert.ToInt32(dr["ID_ROL"]),
                    Name = dr["DESCRIPCION_ROL"] is DBNull ? string.Empty : dr["DESCRIPCION_ROL"].ToString()
                };
                return model;
            });
        }
    }
}
