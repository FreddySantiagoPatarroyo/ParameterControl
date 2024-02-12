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

                        using (OracleCommand command = new OracleCommand("UPDATE_LOAD_CONTROL", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new OracleParameter("PARAM_CODE", entity.Code));
                            command.Parameters.Add(new OracleParameter("PARAM_PAQUETE", entity.Package));
                            command.Parameters.Add(new OracleParameter("PARAM_TABLA", entity.Table));
                            command.Parameters.Add(new OracleParameter("PARAM_PERIODICIDAD", entity.Periodicity));
                            command.Parameters.Add(new OracleParameter("PARAM_RESPALDO", entity.Backup));
                            command.Parameters.Add(new OracleParameter("PARAM_ESTADO", entity.Status));
                            command.Parameters.Add(new OracleParameter("PARAM_ERROR", entity.Error));
                            command.Parameters.Add(new OracleParameter("PARAM_ULTIMA_CARGA", entity.LastLoad));
                            command.Parameters.Add(new OracleParameter("PARAM_ULTIMA_EJECUCION", entity.LastExecution));
                            command.Parameters.Add(new OracleParameter("PARAM_SESION", entity.Session));
                            command.Parameters.Add(new OracleParameter("PARAM_RUTA_LOCAL_SQLUNLOAD", entity.LocalRouteSqlUnLoad));
                            command.Parameters.Add(new OracleParameter("PARAM_FLAG_SISNOT_INICIO", entity.FlagSisNotStart));
                            command.Parameters.Add(new OracleParameter("PARAM_FLAG_SISNOT_OK", entity.FlagSisNotOk));
                            command.Parameters.Add(new OracleParameter("PARAM_FLAG_SISNOT_KO", entity.FlagSisNotKo));
                            command.Parameters.Add(new OracleParameter("PARAM_FECHA_INICIAL", entity.StartDate));
                            command.Parameters.Add(new OracleParameter("PARAM_FECHA_FINAL", entity.EndDate));
                            command.Parameters.Add(new OracleParameter("PARAM_FORMULA_FECHA_INICIAL", entity.FormulaStartDate));
                            command.Parameters.Add(new OracleParameter("PARAM_FORMULA_FECHA_FINAL", entity.FormulaEndDate));
                            command.Parameters.Add(new OracleParameter("PARAM_FLAG_DROP", entity.FlagDrop));
                            command.Parameters.Add(new OracleParameter("PARAM_FLAG_ESTADISTICAS", entity.FlagStatistics));
                            command.Parameters.Add(new OracleParameter("PARAM_FLAG_DEP", entity.FlagDep));
                            command.Parameters.Add(new OracleParameter("PARAM_DIAS_DEP", entity.DaysDep));
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
