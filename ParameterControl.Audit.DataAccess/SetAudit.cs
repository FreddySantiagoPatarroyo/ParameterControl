using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using ParameterControl.Audit.Entities;
using System.Data;

namespace ParameterControl.Audit.DataAccess
{
    public class SetAudit
    {
        private readonly IConfiguration _configuration;

        public SetAudit(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task InsertAudit(AuditModel entity)
        {
            try
            {
                await Task.Run(() =>
                {
                    using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                    {
                        connection.Open();

                        using (OracleCommand command = new OracleCommand(_configuration.GetConnectionString("SAICDES") + "INSERT_AUDIT", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new OracleParameter("PARAM_FECHA_CAMBIO", entity.ModifieldDate));
                            command.Parameters.Add(new OracleParameter("PARAM_COD_USUARIO", entity.UserCode));
                            command.Parameters.Add(new OracleParameter("PARAM_ACCION", entity.Action));
                            command.Parameters.Add(new OracleParameter("PARAM_COMPONENTE", entity.Component));
                            command.Parameters.Add(new OracleParameter("PARAM_VALOR_ANTERIOR", entity.BeforeValue));
                            OracleDataReader reader = command.ExecuteReader();
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
