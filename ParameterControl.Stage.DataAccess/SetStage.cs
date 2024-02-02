﻿using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using ParameterControl.Stage.Entities;
using System.Data;

namespace ParameterControl.Stage.DataAccess
{
    public class SetStage
    {
        private readonly IConfiguration _configuration;

        public SetStage(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> InsertStage(StageModel entity)
        {
            int response = 0;

            try
            {
                return await Task.Run(() => 
                {
                    using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                    {
                        connection.Open();

                        using (OracleCommand command = new OracleCommand("INSERT_SCENARY", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new OracleParameter("PARAM_NAME", entity.Name));
                            command.Parameters.Add(new OracleParameter("PARAM_DESCRIPTION", entity.Description));
                            command.Parameters.Add(new OracleParameter("PARAM_MODIFIELDBY", entity.ModifieldBy));
                            command.Parameters.Add(new OracleParameter("PARAM_OBJETIVO", entity.Objetive));
                            command.Parameters.Add(new OracleParameter("PARAM_ESTADO_ACTIVACION", Convert.ToInt32(entity.State)));
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
