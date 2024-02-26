using Microsoft.Extensions.Configuration;
using ParameterControl.Consistency.DataAccess;
using ParameterControl.Consistency.Entities;
using ParameterControl.Consistency.Interfaces;
using System.Data;

namespace ParameterControl.Consistency.Impl
{
    public class ConsistencyService : IConsistencyService
    {
        private readonly GetAllConsistency _getAllConsistency;
        private readonly GetPaginatorConsistency _getPaginatorConsistency;

        public ConsistencyService(IConfiguration configuration)
        {
            _getAllConsistency = new GetAllConsistency(configuration);
            _getPaginatorConsistency = new GetPaginatorConsistency(configuration);
        }

        public async Task<List<ConsistencyModel>> SelectAllConsistency()
        {
            try
            {
                List<ConsistencyModel> mapper = new List<ConsistencyModel>();
                return await Task.Run(async () =>
                {
                    var response = await _getAllConsistency.SelectAllConsistency();
                    mapper = await MapperConsistency(response);

                    return mapper;
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<List<ConsistencyModel>> MapperConsistency(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                List<ConsistencyModel> policies = new List<ConsistencyModel>();
                foreach (DataRow row in dt.Rows)
                {
                    var response = await MapperToConsistency(row);
                    policies.Add(response);
                }
                return policies;
            }
            else
            {
                return new List<ConsistencyModel>();
            }
        }

        private async Task<ConsistencyModel> MapperToConsistency(DataRow dr)
        {
            return await Task.Run(() =>
            {
                ConsistencyModel model = new ConsistencyModel
                {
                    Code = Convert.ToInt32(dr["CO_ID"]),
                    CustomerId = dr["CUSTOMERID"] is DBNull ? string.Empty : dr["CUSTOMERID"].ToString(),
                    Otmerch = dr["OTMERCH"] is DBNull ? 0 : Convert.ToInt32(dr["OTMERCH"]),
                    Status = dr["ESTADO"] is DBNull ? false : Convert.ToBoolean(dr["ESTADO"]),
                    BillingDate = dr["FECHA_FACTURACION"] is DBNull ? DateTime.Now : Convert.ToDateTime(dr["FECHA_FACTURACION"]),
                    ActivationDate = dr["FECHA_ACTIVACION"] is DBNull ? DateTime.Now : Convert.ToDateTime(dr["FECHA_ACTIVACION"]),
                    SuspensionDate = dr["FECHA_SUSPENSION"] is DBNull ? DateTime.Now : Convert.ToDateTime(dr["FECHA_SUSPENSION"]),
                    Cfm = dr["CFM"] is DBNull ? 0 : Convert.ToInt32(dr["CFM"]),
                    Cycle = dr["CICLO"] is DBNull ? 0 : Convert.ToInt32(dr["CICLO"]),
                    Difference = dr["DIFERENCIA"] is DBNull ? 0 : Convert.ToInt32(dr["DIFERENCIA"]),
                    Difference1 = dr["DIFERENCIA_1"] is DBNull ? 0 : Convert.ToInt32(dr["DIFERENCIA_1"]),
                    ConciliationCode = dr["COD_CONCILIACION"] is DBNull ? string.Empty : dr["COD_CONCILIACION"].ToString(),
                    StageCode = dr["COD_ESCENARIO"] is DBNull ? string.Empty : dr["COD_ESCENARIO"].ToString(),
                    Repetition = dr["REINCIDENCIA"] is DBNull ? 0 : Convert.ToInt32(dr["REINCIDENCIA"]),
                    RepetitionDate = dr["FEC_REINCIDENCIA"] is DBNull ? DateTime.Now : Convert.ToDateTime(dr["ESTADO_ACTIVACION"]),
                    Pqr = dr["PQR"] is DBNull ? 0 : Convert.ToInt32(dr["PQR"]),
                    UploadDate = dr["FEC_CARGA"] is DBNull ? DateTime.Now : Convert.ToDateTime(dr["FEC_CARGA"])
                };
                return model;
            });
        }

        public async Task<List<ConsistencyModel>> SelectPaginatorConsistency(int page, int row)
        {
            try
            {
                var response = await _getPaginatorConsistency.SelectPaginatorConsistency(page, row);
                var mapper = await MapperConsistency(response);
                return mapper;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
