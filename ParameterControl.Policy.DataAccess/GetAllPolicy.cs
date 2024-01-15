using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using ParameterControl.Policy.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterControl.Policy.DataAccess
{
    public class GetAllPolicy
    {
        private readonly IConfiguration _configuration;

        public GetAllPolicy(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int SelectAllPolicy(int page, int row)
        {
            int response = 0;

            try
            {
                using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                {
                    connection.Open();

                    using (OracleCommand command = new OracleCommand("ALL_POLICY",connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new OracleParameter("PARAM_PAGE", page));
                        command.Parameters.Add(new OracleParameter("PARAM_ROW", row));
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
