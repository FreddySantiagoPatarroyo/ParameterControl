using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using ParameterControl.Parameter.Entities;
using System.Data;

namespace ParameterControl.Parameter.DataAccess
{
    public class ModifyParameter
    {
        private readonly IConfiguration _configuration;

        public ModifyParameter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> UpdateParameter(ParameterModel entity)
        {
            int response = 0;

            try
            {
                return await Task.Run(() =>
                {
                    using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                    {
                        connection.Open();

                        using (OracleCommand command = new OracleCommand("UPDATE_PARAMETER", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new OracleParameter("PARAM_CODE", entity.Code));
                            command.Parameters.Add(new OracleParameter("PARAM_PARAMETRO", entity.Parameter));
                            command.Parameters.Add(new OracleParameter("PARAM_VALOR", entity.Value));
                            command.Parameters.Add(new OracleParameter("PARAM_DESCRIPCION", entity.Description));
                            command.Parameters.Add(new OracleParameter("PARAM_TIPO", entity.ParameterType));
                            command.Parameters.Add(new OracleParameter("PARAM_MODIFICADO_POR", entity.ModifieldBy));
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
