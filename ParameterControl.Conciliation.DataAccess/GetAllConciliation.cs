using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterControl.Conciliation.DataAccess
{
    public class GetAllConciliation
    {
        private readonly IConfiguration _configuration;
        DataTable _dataTable = new DataTable();

        public GetAllConciliation(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<DataTable> SelectAllConciliation()
        {
            try
            {
                return await Task.Run(() => 
                {
                    using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                    {
                        connection.Open();

                        using (OracleCommand command = new OracleCommand("ALL_CONCILIATION", connection))
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
