using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using ParameterControl.User.Entities;
using System.Data;

namespace ParameterControl.User.DataAccess
{
    public class SetUser
    {
        private readonly IConfiguration _configuration;

        public SetUser(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int InsertUser(UserModel entity)
        {
            int response = 0;

            try
            {
                using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                {
                    connection.Open();

                    using (OracleCommand command = new OracleCommand("INSERT_USER", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new OracleParameter("PARAM_NAME", entity.User));
                        command.Parameters.Add(new OracleParameter("PARAM_EMAIL", entity.Email));
                        command.Parameters.Add(new OracleParameter("PARAM_USER_NAME", entity.UserName));
                        command.Parameters.Add(new OracleParameter("PARAM_IDROL", entity.RolId));
                        command.Parameters.Add(new OracleParameter("PARAM_LOGINGROUP", entity.GroupId));
                        command.Parameters.Add(new OracleParameter("PARAM_MODIFIELDBY", entity.ModifiedBy));
                        command.Parameters.Add(new OracleParameter("PARAM_ESTADO_ACTIVACION", Convert.ToInt32(entity.State)));
                        command.Parameters.Add(new OracleParameter("PARAM_PRIMER_ACCESO", Convert.ToInt32(entity.FirstAccess)));
                        command.Parameters.Add(new OracleParameter("PARAM_CONTRASEÑA", entity.Password));

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
