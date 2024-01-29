using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using ParameterControl.Auth.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterControl.Auth.DataAccess
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

                    using (OracleCommand command = new OracleCommand("INSERT_USER",connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new OracleParameter("PARAM_NAME", entity.User));
                        command.Parameters.Add(new OracleParameter("PARAM_EMAIL", entity.Email));
                        command.Parameters.Add(new OracleParameter("PARAM_USER_NAME", entity.UserName));
                        command.Parameters.Add(new OracleParameter("PARAM_IDROL", entity.RolId));
                        command.Parameters.Add(new OracleParameter("PARAM_LOGINGROUP", entity.GroupId));
                        command.Parameters.Add(new OracleParameter("PARAM_MODIFIELDBY", entity.ModifiedBy));
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
