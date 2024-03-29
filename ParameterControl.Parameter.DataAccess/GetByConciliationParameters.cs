﻿using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ParameterControl.Parameter.DataAccess
{
    public class GetByConciliationParameters
    {
        private readonly IConfiguration _configuration;
        DataTable _dataTable = new DataTable();

        public GetByConciliationParameters(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<DataTable> SelectByConciliationParameters(string entity)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                    {
                        connection.Open();

                        using (OracleCommand command = new OracleCommand(_configuration.GetConnectionString("SAICDES") + "SELECT_BY_CONCILIATION_PARAMETERS", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new OracleParameter("PARAM_NOMBRE_CONCILIACION", entity));
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
