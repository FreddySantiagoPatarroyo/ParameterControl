using Microsoft.Extensions.Configuration;
using ParameterControl.Policy.DataAccess;
using ParameterControl.Policy.Entities;
using ParameterControl.Policy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterControl.Policy.Impl
{
    public class PolicyService : IPolicyService
    {
        private readonly SetPolicy _setPolicy;
        private readonly RemovePolicy _removePolicy;
        private readonly ModifyPolicy _modifyPolicy;
        private readonly GetByIdPolicy _getByIdPolicy;
        private readonly GetAllPolicy _getAllPolicy;

        public PolicyService(IConfiguration configuration)
        {
            _setPolicy = new SetPolicy(configuration);
            _removePolicy = new RemovePolicy(configuration);
            _modifyPolicy = new ModifyPolicy(configuration);
            _getByIdPolicy = new GetByIdPolicy(configuration);
            _getAllPolicy = new GetAllPolicy(configuration);
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
                return await Task.Run(()=>
                {
                    return _removePolicy.DeletePolicy(entity);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> SelectAllPolicy(int page, int row)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _getAllPolicy.SelectAllPolicy(page, row);
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
    }
}
