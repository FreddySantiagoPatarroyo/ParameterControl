using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using ParameterControl.LoadControl.Entities;
using System.Data;

namespace ParameterControl.LoadControl.DataAccess
{
    public class ModifyLoadControl
    {
        private readonly IConfiguration _configuration;

        public ModifyLoadControl(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> UpdateLoadControl(LoadControlModel entity)
        {
            int response = 0;

            try
            {
                return await Task.Run(() =>
                {
                    using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                    {
                        connection.Open();

                        using (OracleCommand command = new OracleCommand(_configuration.GetConnectionString("SAICDES") + "UPDATE_LOAD_CONTROL", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new OracleParameter("PARAM_PACKAGE", entity.Package));
                            command.Parameters.Add(new OracleParameter("PARAM_TABLA", entity.Table));
                            command.Parameters.Add(new OracleParameter("PARAM_PERIODICIDAD", entity.Periodicity));
                            command.Parameters.Add(new OracleParameter("PARAM_ESTADO", entity.Status));
                            command.Parameters.Add(new OracleParameter("PARAM_ERROR", entity.Error));
                            command.Parameters.Add(new OracleParameter("PARAM_ULTIMA_CARGA", entity.LastLoad));
                            command.Parameters.Add(new OracleParameter("PARAM_ULTIMA_EJECUCION", entity.LastExecution));
                            command.Parameters.Add(new OracleParameter("PARAM_ESTADO_ACTIVACION", Convert.ToInt32(entity.State)));
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
