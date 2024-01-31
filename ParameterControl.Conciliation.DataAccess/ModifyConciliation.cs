﻿using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using ParameterControl.Conciliation.Entities;
using System.Data;

namespace ParameterControl.Conciliation.DataAccess
{
    public class ModifyConciliation
    {
        private readonly IConfiguration _configuration;

        public ModifyConciliation(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int UpdateConciliation(ConciliationModel entity)
        {
            int response = 0;

            try
            {
                using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                {
                    connection.Open();

                    using (OracleCommand command = new OracleCommand("UPDATE_CONCILIATION", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new OracleParameter("PARAM_CODE", entity.Id));
                        command.Parameters.Add(new OracleParameter("PARAM_NAME", entity.ConciliationName));
                        command.Parameters.Add(new OracleParameter("PARAM_DESCRIPTION", entity.Description));
                        command.Parameters.Add(new OracleParameter("PARAM_MODIFIELDBY", entity.AssignedUser));
                        command.Parameters.Add(new OracleParameter("PARAM_OBJETIVO", entity.Observation));
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
