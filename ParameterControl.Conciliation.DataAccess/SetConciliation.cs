using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using ParameterControl.Conciliation.Entities;
using System.Data;

namespace ParameterControl.Conciliation.DataAccess
{
    public class SetConciliation
    {
        private readonly IConfiguration _configuration;

        public SetConciliation(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int InsertConciliation(ConciliationModel entity)
        {
            int response = 0;

            try
            {
                using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                {
                    connection.Open();

                    using (OracleCommand command = new OracleCommand("INSERT_CONCILIATION", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new OracleParameter("PARAM_NOMBRE_CONCILIACION", entity.ConciliationName));
                        command.Parameters.Add(new OracleParameter("PARAM_DESCRIPCION", entity.Description));
                        command.Parameters.Add(new OracleParameter("PARAM_TABLA_DESTINO", entity.TargetTable));
                        command.Parameters.Add(new OracleParameter("PARAM_APROBACION", entity.Approval));
                        command.Parameters.Add(new OracleParameter("PARAM_CAMPOS_TABLA_DESTINO", entity.FieldTargetTable));
                        command.Parameters.Add(new OracleParameter("PARAM_USUARIO_ASIGNADO", entity.AssignedUser));
                        command.Parameters.Add(new OracleParameter("PARAM_EMAILS", entity.Email));
                        command.Parameters.Add(new OracleParameter("PARAM_COD_POLITICA", entity.PolicyId));
                        command.Parameters.Add(new OracleParameter("PARAM_REQUIERE_APROBACION", entity.RequiredApproval));
                        command.Parameters.Add(new OracleParameter("PARAM_TIPO_OPERACION", entity.OperationType));
                        command.Parameters.Add(new OracleParameter("PARAM_OPERADORA", entity.Operator));
                        command.Parameters.Add(new OracleParameter("PARAM_SOX", entity.Sox));
                        command.Parameters.Add(new OracleParameter("PARAM_TIPO_ASIGNACION", entity.AssignmentType));
                        command.Parameters.Add(new OracleParameter("PARAM_KPI", entity.Kpi));
                        command.Parameters.Add(new OracleParameter("PARAM_FRECUENCIA_MES", entity.FrequencyMonth));
                        command.Parameters.Add(new OracleParameter("PARAM_TOMA", entity.Take));
                        command.Parameters.Add(new OracleParameter("PARAM_EJECUCION", entity.Execution));
                        command.Parameters.Add(new OracleParameter("PARAM_ANALISIS_REPORTE", entity.AnalysisReport));
                        command.Parameters.Add(new OracleParameter("PARAM_SEGUIMIENTO", entity.Follow));
                        command.Parameters.Add(new OracleParameter("PARAM_FECHA_PROGRAMADA", entity.ScheduledDate));
                        command.Parameters.Add(new OracleParameter("PARAM_FECHA_ENTREGA", entity.DeliverDate));
                        command.Parameters.Add(new OracleParameter("PARAM_OBSERVACIONES", entity.Observation));
                        command.Parameters.Add(new OracleParameter("PARAM_PRUEBA_FECHA", entity.TestDate));
                        command.Parameters.Add(new OracleParameter("PARAM_REQ", entity.Req));
                        command.Parameters.Add(new OracleParameter("PARAM_ESTADO", Convert.ToInt32(entity.State).ToString()));
                        OracleDataReader reader = command.ExecuteReader();
                        response = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return response;
        }
    }
}
