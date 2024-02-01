using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using ParameterControl.Parameter.Entities;
using System.Data;

namespace ParameterControl.Parameter.DataAccess
{
    public class RemoveParameter
    {
        private readonly IConfiguration _configuration;

        public RemoveParameter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> DeleteParameter(ParameterModel entity)
        {
            int response = 0;

            try
            {
                return await Task.Run(() => 
                {
                    using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                    {
                        connection.Open();

                        using (OracleCommand command = new OracleCommand("DELETE_PARAMETER", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new OracleParameter("PARAM_CODE", entity.Code));
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
