using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using ParameterControl.Conciliation.Entities;
using System.Data;

namespace ParameterControl.Conciliation.DataAccess
{
    public class ModifyConciliation
    {
        private readonly IConfiguration _configuration;

        public ModifyConciliation(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int UpdateConciliation(ConciliationModel entity)
        {
            int response = 0;

            try
            {
                using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                {
                    connection.Open();

                    using (OracleCommand command = new OracleCommand("UPDATE_CONCILIATION", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new OracleParameter("PARAM_CODE", entity.Code));
                        command.Parameters.Add(new OracleParameter("PARAM_NOMBRE_CONCILIACION", entity.ConciliationName));
                        command.Parameters.Add(new OracleParameter("PARAM_DESCRIPCION", entity.Description));
                        command.Parameters.Add(new OracleParameter("PARAM_EMAILS", entity.Email));
                        command.Parameters.Add(new OracleParameter("PARAM_COD_POLITICA", entity.PolicyId));
                        command.Parameters.Add(new OracleParameter("PARAM_REQUIERE_APROBACION", entity.RequiredApproval));
                        command.Parameters.Add(new OracleParameter("PARAM_TIPO_OPERACION", entity.OperationType));
                        command.Parameters.Add(new OracleParameter("PARAM_TIPO_ASIGNACION", entity.AssignmentType));
                        command.Parameters.Add(new OracleParameter("PARAM_TABLA_DESTINO", entity.TargetTable));
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
