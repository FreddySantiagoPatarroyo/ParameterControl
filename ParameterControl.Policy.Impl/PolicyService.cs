using Microsoft.Extensions.Configuration;
using ParameterControl.Policy.DataAccess;
using ParameterControl.Policy.Entities;
using ParameterControl.Policy.Interfaces;
using System.Data;
using System.Xml.Linq;

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

        public async Task<int> SelectAllPolicy()
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _getAllPolicy.SelectAllPolicy();
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> SelectByIdPolicy(PolicyModel entity)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _getByIdPolicy.SelectByIdPolicy(entity);
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
                    var mapper = await MapperToPolicy(response);
                    return mapper;
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<List<PolicyModel>> MapperToPolicy(DataTable dt)
        {
            return await Task.Run(() =>
            {
                if (dt.Rows.Count > 0)
                {
                    List<PolicyModel> policies = new List<PolicyModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        PolicyModel model = new PolicyModel
                        {
                            IdPolicy = Convert.ToInt32(row["CODE"]),
                            Code = row["CODE"].ToString(),
                            Name = row["CODE"].ToString(),
                            Description = row["CODE"].ToString(),
                            CreationDate = Convert.ToDateTime(row["CODE"]),
                            ModifieldDate = Convert.ToDateTime(row["CODE"]),
                            ModifieldBy = row["CODE"].ToString()
                        };
                        policies.Add(model);
                    }
                    return policies;
                }
                else
                {
                    return new List<PolicyModel>();
                }
            });
        }
    }
}
