using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ParameterControl.User.DataAccess
{
    public class CountUser
    {
        private readonly IConfiguration _configuration;
        DataTable _dataTable = new DataTable();

        public CountUser(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SelectCountUser()
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                    {
                        connection.Open();

                        using (OracleCommand command = new OracleCommand(_configuration.GetConnectionString("SAICDES") + "COUNT_USER", connection))
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
