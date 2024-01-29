﻿using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using ParameterControl.Policy.Entities;
using System.Data;

namespace ParameterControl.Policy.DataAccess
{
    public class SetPolicy
    {
        private readonly IConfiguration _configuration;

        public SetPolicy(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int InsertPolicy(PolicyModel entity)
        {
            int response = 0;

            try
            {
                using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                {
                    connection.Open();

                    using (OracleCommand command = new OracleCommand("INSERT_POLICY", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new OracleParameter("PARAM_NAME", entity.Name));
                        command.Parameters.Add(new OracleParameter("PARAM_DESCRIPTION", entity.Description));
                        command.Parameters.Add(new OracleParameter("PARAM_MODIFIELDBY", entity.ModifieldBy));
                        command.Parameters.Add(new OracleParameter("PARAM_OBJETIVO", entity.Objetive));
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
