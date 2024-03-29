﻿using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using ParameterControl.User.Entities;
using System.Data;

namespace ParameterControl.User.DataAccess
{
    public class RemoveUser
    {
        private readonly IConfiguration _configuration;

        public RemoveUser(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int DeleteUser(UserModel entity)
        {
            int response = 0;

            try
            {
                using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                {
                    connection.Open();

                    using (OracleCommand command = new OracleCommand(_configuration.GetConnectionString("SAICDES") + "DELETE_USER", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new OracleParameter("PARAM_CODE", entity.Code));
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
