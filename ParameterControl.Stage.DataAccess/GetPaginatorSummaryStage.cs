using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ParameterControl.Stage.DataAccess
{
    public class GetPaginatorSummaryStage
    {
        private readonly IConfiguration _configuration;
        DataTable _dataTable = new DataTable();

        public GetPaginatorSummaryStage(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<DataTable> SelectPaginatorSummaryStage(int page, int row)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                    {
                        connection.Open();

                        using (OracleCommand command = new OracleCommand(_configuration.GetConnectionString("SAICDES") + "PAGINATOR_SUMMARY_SCENARY", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new OracleParameter("PARAM_PAGE", page));
                            command.Parameters.Add(new OracleParameter("PARAM_ROW", row));
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
