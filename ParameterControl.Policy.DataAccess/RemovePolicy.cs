﻿using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using ParameterControl.Policy.Entities;
using System.Data;

namespace ParameterControl.Policy.DataAccess
{
    public class RemovePolicy
    {
        private readonly IConfiguration _configuration;

        public RemovePolicy(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> DeletePolicy(PolicyModel entity)
        {
            int response = 0;

            try
            {
                return await Task.Run(() =>
                {
                    using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                    {
                        connection.Open();

                        using (OracleCommand command = new OracleCommand(_configuration.GetConnectionString("SAICDES") + "DELETE_POLICY", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new OracleParameter("PARAM_CODE", entity.Code));
                            OracleDataReader reader = command.ExecuteReader();
                            return response = 1;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
