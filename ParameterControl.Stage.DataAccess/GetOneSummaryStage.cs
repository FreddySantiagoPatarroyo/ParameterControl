using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using ParameterControl.Stage.Entities;
using System.Data;

namespace ParameterControl.Stage.DataAccess
{
    public class GetOneSummaryStage
    {
        private readonly IConfiguration _configuration;
        DataTable _dataTable = new DataTable();

        public GetOneSummaryStage(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<DataTable> SelectOneSummaryStage(StageSummaryModel entity)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                    {
                        connection.Open();

                        using (OracleCommand command = new OracleCommand(_configuration.GetConnectionString("SAICDES") + "SELECT_ONE_SUMMARY_SCENARY", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new OracleParameter("PARAM_CONCILIACIONSK", entity.ConciliationSK));
                            command.Parameters.Add(new OracleParameter("PARAM_ESCENARIOSK", entity.StageSK));
                            command.Parameters.Add(new OracleParameter("PARAM_FECHA_CARGA", entity.UploadDate));
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
