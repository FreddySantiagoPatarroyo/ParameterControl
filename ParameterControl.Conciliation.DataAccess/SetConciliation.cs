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

                    using (OracleCommand command = new OracleCommand("INSERT_CONCILIATION",connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new OracleParameter("PARAM_NAME", entity.Name));
                        command.Parameters.Add(new OracleParameter("PARAM_DESCRIPTION", entity.Description));
                        command.Parameters.Add(new OracleParameter("PARAM_MODIFIELDBY", entity.ModifieldBy));
                        command.Parameters.Add(new OracleParameter("PARAM_OBJETIVO", entity.Objetive));
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
