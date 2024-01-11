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

        public PolicyService(IConfiguration configuration)
        {
            _setPolicy = new SetPolicy(configuration);
        }

        public int InsertPolicy(PolicyModel entity)
        {
            try
            {
                return _setPolicy.InsertPolicy(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
