using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using ParameterControl.Stage.Entities;
using System.Data;

namespace ParameterControl.Stage.DataAccess
{
    public class ModifyAmountBenefitSummaryStage
    {
        private readonly IConfiguration _configuration;

        public ModifyAmountBenefitSummaryStage(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int UpdateAmountBenefitSummaryStage(StageSummaryModel entity)
        {
            int response = 0;

            try
            {
                using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("conn-db")))
                {
                    connection.Open();

                    using (OracleCommand command = new OracleCommand(_configuration.GetConnectionString("SAICDES") + "UPDATE_BENEFIT_AMOUNT_SUMMARY_SCENARY", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new OracleParameter("PARAM_CONCILIACIONSK", entity.ConciliationSK));
                        command.Parameters.Add(new OracleParameter("PARAM_ESCENARIOSK", entity.StageSK));
                        command.Parameters.Add(new OracleParameter("PARAM_FECHA_CARGA", entity.UploadDate));
                        command.Parameters.Add(new OracleParameter("PARAM_MONTO_BENEFICIO", entity.AmountBenefit));
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