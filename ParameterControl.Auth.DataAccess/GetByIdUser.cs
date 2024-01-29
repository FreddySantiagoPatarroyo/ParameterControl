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
    public class GetByIdUser
    {
        private readonly IConfiguration _configuration;
        DataTable _dataTable = new DataTable();

        public GetByIdUser(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<DataTable> SelectByIdUser(UserModel entity)
        {
            try
            {
                return await Task.Run(() => 
                {
                    using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                    {
                        connection.Open();

                        using (OracleCommand command = new OracleCommand("SELECT_BY_ID_USER", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new OracleParameter("PARAM_CODE", entity.Id));
                            OracleDataReader reader = command.ExecuteReader();
                            _dataTable.Load(reader);
                            return _dataTable;
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
