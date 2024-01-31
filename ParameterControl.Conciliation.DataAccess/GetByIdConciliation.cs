﻿using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using ParameterControl.Conciliation.Entities;
using System.Data;

namespace ParameterControl.Conciliation.DataAccess
{
    public class GetByIdConciliation
    {
        private readonly IConfiguration _configuration;
        DataTable _dataTable = new DataTable();

        public GetByIdConciliation(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<DataTable> SelectByIdConciliation(ConciliationModel entity)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                    {
                        connection.Open();

                        using (OracleCommand command = new OracleCommand("SELECT_BY_ID_CONCILIATION", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new OracleParameter("PARAM_CODE", entity.Id));
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
