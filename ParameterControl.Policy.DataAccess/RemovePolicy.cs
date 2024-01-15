using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using ParameterControl.Policy.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterControl.Policy.DataAccess
{
    public class RemovePolicy
    {
        private readonly IConfiguration _configuration;

        public RemovePolicy(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int DeletePolicy(PolicyModel entity)
        {
            int response = 0;

            try
            {
                using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                {
                    connection.Open();

                    using (OracleCommand command = new OracleCommand("DELETE_POLICY",connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new OracleParameter("PARAM_CODE", entity.Code));
                        OracleDataReader reader = command.ExecuteReader();
                        response = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return response;
        }
    }
}
