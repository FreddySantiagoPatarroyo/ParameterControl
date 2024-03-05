using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using ParameterControl.Parameter.Entities;
using System.Data;

namespace ParameterControl.Parameter.DataAccess
{
    public class SetParameter
    {
        private readonly IConfiguration _configuration;

        public SetParameter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> InsertParameter(ParameterModel entity)
        {
            int response = 0;

            try
            {
                return await Task.Run(() =>
                {
                    using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                    {
                        connection.Open();

                        using (OracleCommand command = new OracleCommand("INSERT_PARAMETER", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new OracleParameter("PARAM_PARAMETRO", entity.Parameter));
                            command.Parameters.Add(new OracleParameter("PARAM_VALOR", entity.Value));
                            command.Parameters.Add(new OracleParameter("PARAM_DESCRIPCION", entity.Description));
                            command.Parameters.Add(new OracleParameter("PARAM_TIPO", entity.ParameterType));
                            command.Parameters.Add(new OracleParameter("PARAM_CODE_PADRE", entity.FatherId));
                            command.Parameters.Add(new OracleParameter("PARAM_VALOR1", entity.Value1));
                            command.Parameters.Add(new OracleParameter("PARAM_ESTADO_PARAMETRO", Convert.ToInt32(entity.State).ToString()));
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
