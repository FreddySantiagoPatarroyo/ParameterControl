using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using ParameterControl.Stage.Entities;
using System.Data;

namespace ParameterControl.Stage.DataAccess
{
    public class GetByIdStage
    {
        private readonly IConfiguration _configuration;
        DataTable _dataTable = new DataTable();

        public GetByIdStage(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<DataTable> SelectByIdStage(StageModel entity)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                    {
                        connection.Open();

                        using (OracleCommand command = new OracleCommand("SELECT_BY_ID_SCENARY", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new OracleParameter("PARAM_CODE", entity.Code));
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
