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

        public PolicyService(IConfiguration configuration)
        {
            _setPolicy = new SetPolicy(configuration);
            _removePolicy = new RemovePolicy(configuration);
            _modifyPolicy = new ModifyPolicy(configuration);
            _getByIdPolicy = new GetByIdPolicy(configuration);
            _getAllPolicy = new GetAllPolicy(configuration);
            _getPaginatorPolicy = new GetPaginatorPolicy(configuration);
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
                return await Task.Run(async () =>
                {
                    var response = await _getPaginatorPolicy.SelectPaginatorPolicy(page, row);
                    var mapper = await MapperPolicy(response);
                    return mapper;
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<List<PolicyModel>> MapperPolicy(DataTable dt)
        {
            return await Task.Run(() =>
            {
                if (dt.Rows.Count > 0)
                {
                    List<PolicyModel> policies = new List<PolicyModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        policies.Add(MapperToPolicy(row).Result);
                    }
                    return policies;
                }
                else
                {
                    return new List<PolicyModel>();
                }
            });
        }

        private async Task<PolicyModel> MapperToPolicy(DataRow dr)
        {
            return await Task.Run(() =>
            {
                PolicyModel model = new PolicyModel
                {
                    Id = dr["CODE"].ToString(),
                    Code = dr["CODE_POLITICA"].ToString(),
                    Name = dr["NOMBRE_POLITICA"].ToString(),
                    Description = dr["DESCRIPCION"].ToString(),
                    Objetive = dr["OBJETIVO"].ToString(),
                    CreationDate = Convert.ToDateTime(dr["FECHA_CREACION"]),
                    ModifieldDate = Convert.ToDateTime(dr["FECHA_ACTUALIZACION"]),
                    ModifieldBy = dr["MODIFICADO_POR"].ToString()
                };
                return model;
            });
        }
    }
}
