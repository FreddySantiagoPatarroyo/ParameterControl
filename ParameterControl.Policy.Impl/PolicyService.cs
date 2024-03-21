using Microsoft.Extensions.Configuration;
using ParameterControl.Policy.DataAccess;
using ParameterControl.Policy.Entities;
using ParameterControl.Policy.Interfaces;
using System.Data;

namespace ParameterControl.Policy.Impl
{
    public class PolicyService : IPolicyService
    {
        private readonly SetPolicy _setPolicy;
        private readonly RemovePolicy _removePolicy;
        private readonly ModifyPolicy _modifyPolicy;
        private readonly GetByIdPolicy _getByIdPolicy;
        private readonly GetAllPolicy _getAllPolicy;
        private readonly GetPaginatorPolicy _getPaginatorPolicy;
        private readonly CountPolicy _countPolicy;

        public PolicyService(IConfiguration configuration)
        {
            _setPolicy = new SetPolicy(configuration);
            _removePolicy = new RemovePolicy(configuration);
            _modifyPolicy = new ModifyPolicy(configuration);
            _getByIdPolicy = new GetByIdPolicy(configuration);
            _getAllPolicy = new GetAllPolicy(configuration);
            _getPaginatorPolicy = new GetPaginatorPolicy(configuration);
            _countPolicy = new CountPolicy(configuration);
        }

        public async Task<int> InsertPolicy(PolicyModel entity)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _setPolicy.InsertPolicy(entity);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> DeletePolicy(PolicyModel entity)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _removePolicy.DeletePolicy(entity);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<PolicyModel>> SelectAllPolicy()
        {
            try
            {
                List<PolicyModel> mapper = new List<PolicyModel>();
                return await Task.Run(async () =>
                {
                    var response = await _getAllPolicy.SelectAllPolicy();
                    mapper = await MapperPolicy(response);

                    return mapper;
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PolicyModel> SelectByIdPolicy(PolicyModel entity)
        {
            try
            {
                PolicyModel mapper = new PolicyModel();
                return await Task.Run(async () =>
                {
                    var response = await _getByIdPolicy.SelectByIdPolicy(entity);
                    foreach (DataRow row in response.Rows)
                    {
                        mapper = await MapperToPolicy(row);
                    }
                    return mapper;
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdatePolicy(PolicyModel entity)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _modifyPolicy.UpdatePolicy(entity);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<PolicyModel>> SelectPaginatorPolicy(int page, int row)
        {
            try
            {
                var response = await _getPaginatorPolicy.SelectPaginatorPolicy(page, row);
                var mapper = await MapperPolicy(response);
                return mapper;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<List<PolicyModel>> MapperPolicy(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                List<PolicyModel> policies = new List<PolicyModel>();
                foreach (DataRow row in dt.Rows)
                {
                    var response = await MapperToPolicy(row);
                    policies.Add(response);
                }
                return policies;
            }
            else
            {
                return new List<PolicyModel>();
            }
        }

        private async Task<PolicyModel> MapperToPolicy(DataRow dr)
        {
            return await Task.Run(() =>
            {
                PolicyModel model = new PolicyModel
                {
                    Code = Convert.ToInt32(dr["COD_POLITICA"]),
                    Name = dr["NOMBRE_POLITICA"] is DBNull ? string.Empty : dr["NOMBRE_POLITICA"].ToString(),
                    Description = dr["DESCRIPCION"] is DBNull ? string.Empty : dr["DESCRIPCION"].ToString(),
                    Objetive = dr["OBJETIVO"] is DBNull ? string.Empty : dr["OBJETIVO"].ToString(),
                    CreationDate = dr["FECHA_CREACION"] is DBNull ? DateTime.Now : Convert.ToDateTime(dr["FECHA_CREACION"]),
                    ModifieldDate = dr["FECHA_ACTUALIZACION"] is DBNull ? DateTime.Now : Convert.ToDateTime(dr["FECHA_ACTUALIZACION"]),
                    Package = dr["PAQUETE"] is DBNull ? string.Empty : dr["PAQUETE"].ToString(),
                    State = dr["ESTADO_POLITICA"] is DBNull ? false : Convert.ToBoolean(Convert.ToInt32(dr["ESTADO_POLITICA"])),
                };
                return model;
            });
        }

        public async Task<int> SelectCountPolicy()
        {
            try
            {
                return await _countPolicy.SelectCountPolicy();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
