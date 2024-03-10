using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using ParameterControl.Stage.Entities;
using System.Data;

namespace ParameterControl.Stage.DataAccess
{
    public class ModifyStage
    {
        private readonly IConfiguration _configuration;

        public ModifyStage(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> UpdateStage(StageModel entity)
        {
            int response = 0;

            try
            {
                return await Task.Run(() =>
                {
                    using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                    {
                        connection.Open();

                        using (OracleCommand command = new OracleCommand("UPDATE_SCENARY", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new OracleParameter("PARAM_CODE", entity.Code));
                            command.Parameters.Add(new OracleParameter("PARAM_NOMBRE_ESCENARIO", entity.Name));
                            command.Parameters.Add(new OracleParameter("PARAM_IMPACTO", entity.Impact));
                            command.Parameters.Add(new OracleParameter("PARAM_COD_CONCILIACION", entity.Conciliation));
                            command.Parameters.Add(new OracleParameter("PARAM_ESTADO_ACTIVACION", Convert.ToInt32(entity.State).ToString()));
                            OracleDataReader reader = command.ExecuteReader();
                            return response = 1;
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
