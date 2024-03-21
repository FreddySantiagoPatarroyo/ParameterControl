using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using ParameterControl.Parameter.Entities;
using System.Data;

namespace ParameterControl.Parameter.DataAccess
{
    public class GetByIdParameter
    {
        private readonly IConfiguration _configuration;
        DataTable _dataTable = new DataTable();

        public GetByIdParameter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<DataTable> SelectByIdParameter(ParameterModel entity)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                    {
                        connection.Open();

                        using (OracleCommand command = new OracleCommand(_configuration.GetConnectionString("SAICDES") + "SELECT_BY_ID_PARAMETER", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new OracleParameter("PARAM_CODE", entity.Code));
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
