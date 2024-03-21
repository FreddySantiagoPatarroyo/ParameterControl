using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ParameterControl.LoadControl.DataAccess
{
    public class GetAllLoadControl
    {
        private readonly IConfiguration _configuration;
        DataTable _dataTable = new DataTable();

        public GetAllLoadControl(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<DataTable> SelectAllLoadControl()
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                    {
                        connection.Open();

                        using (OracleCommand command = new OracleCommand(_configuration.GetConnectionString("SAICDES") + "ALL_LOAD_CONTROL", connection))
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
