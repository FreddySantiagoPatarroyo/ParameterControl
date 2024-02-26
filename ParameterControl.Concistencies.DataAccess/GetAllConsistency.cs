using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ParameterControl.Consistency.DataAccess
{
    public class GetAllConsistency
    {
        private readonly IConfiguration _configuration;
        DataTable _dataTable = new DataTable();

        public GetAllConsistency(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<DataTable> SelectAllConsistency()
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                    {
                        connection.Open();

                        using (OracleCommand command = new OracleCommand("ALL_CONSISTENCIES_POST_FACT", connection))
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
