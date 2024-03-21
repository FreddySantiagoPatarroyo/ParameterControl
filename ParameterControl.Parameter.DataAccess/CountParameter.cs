﻿using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ParameterControl.Parameter.DataAccess
{
    public class CountParameter
    {
        private readonly IConfiguration _configuration;
        DataTable _dataTable = new DataTable();

        public CountParameter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SelectCountParameter()
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                    {
                        connection.Open();

                        using (OracleCommand command = new OracleCommand(_configuration.GetConnectionString("SAICDES") + "COUNT_PARAMETER", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            OracleDataReader reader = command.ExecuteReader();
                            _dataTable.Load(reader);
                            return Convert.ToInt32(_dataTable.Rows[0]["TOTAL"].ToString());
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
