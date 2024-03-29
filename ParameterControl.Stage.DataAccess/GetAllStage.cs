﻿using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ParameterControl.Stage.DataAccess
{
    public class GetAllStage
    {
        private readonly IConfiguration _configuration;
        DataTable _dataTable = new DataTable();

        public GetAllStage(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<DataTable> SelectAllStage()
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                    {
                        connection.Open();

                        using (OracleCommand command = new OracleCommand(_configuration.GetConnectionString("SAICDES") + "ALL_SCENARY", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            OracleDataReader reader = command.ExecuteReader();
                            _dataTable.Load(reader);
                            return _dataTable;
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
