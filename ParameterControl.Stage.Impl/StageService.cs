using Microsoft.Extensions.Configuration;
using ParameterControl.Stage.DataAccess;
using ParameterControl.Stage.Entities;
using ParameterControl.Stage.Interfaces;
using System.Data;

namespace ParameterControl.Stage.Impl
{
    public class StageService : IStageService
    {
        private readonly SetStage _setStage;
        private readonly RemoveStage _removeStage;
        private readonly ModifyStage _modifyStage;
        private readonly GetByIdStage _getByIdStage;
        private readonly GetAllStage _getAllStage;
        private readonly GetPaginatorStage _getPaginatorStage;
        private readonly CountStage _countStage;
        private readonly GetAllSummaryStage _getAllSummaryStage;
        private readonly GetPaginatorSummaryStage _getPaginatorSummaryStage;
        private readonly ModifyAmountBenefitSummaryStage _modifyAmountBenefitSummaryStage;
        private readonly ModifyAmountImpactSummaryStage _modifyAmountImpactSummaryStage;
        private readonly GetOneSummaryStage _getOneSummaryStage;

        public StageService(IConfiguration configuration)
        {
            _setStage = new SetStage(configuration);
            _removeStage = new RemoveStage(configuration);
            _modifyStage = new ModifyStage(configuration);
            _getByIdStage = new GetByIdStage(configuration);
            _getAllStage = new GetAllStage(configuration);
            _getPaginatorStage = new GetPaginatorStage(configuration);
            _countStage = new CountStage(configuration);
            _getAllSummaryStage = new GetAllSummaryStage(configuration);
            _getPaginatorSummaryStage = new GetPaginatorSummaryStage(configuration);
            _modifyAmountBenefitSummaryStage = new ModifyAmountBenefitSummaryStage(configuration);
            _modifyAmountImpactSummaryStage = new ModifyAmountImpactSummaryStage(configuration);
            _getOneSummaryStage = new GetOneSummaryStage(configuration);
        }

        public async Task<int> InsertStage(StageModel entity)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _setStage.InsertStage(entity);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> DeleteStage(StageModel entity)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _removeStage.DeleteStage(entity);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<StageModel>> SelectAllStage()
        {
            try
            {
                List<StageModel> mapper = new List<StageModel>();
                return await Task.Run(async () =>
                {
                    var response = await _getAllStage.SelectAllStage();
                    mapper = await MapperStage(response);

                    return mapper;
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<StageModel> SelectByIdStage(StageModel entity)
        {
            try
            {
                StageModel mapper = new StageModel();
                return await Task.Run(async () =>
                {
                    var response = await _getByIdStage.SelectByIdStage(entity);
                    foreach (DataRow row in response.Rows)
                    {
                        mapper = await MapperToStage(row);
                    }
                    return mapper;
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdateStage(StageModel entity)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _modifyStage.UpdateStage(entity);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<StageModel>> SelectPaginatorStage(int page, int row)
        {
            try
            {
                var response = await _getPaginatorStage.SelectPaginatorStage(page, row);
                var mapper = await MapperStage(response);
                return mapper;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<List<StageModel>> MapperStage(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                List<StageModel> policies = new List<StageModel>();
                foreach (DataRow row in dt.Rows)
                {
                    var response = await MapperToStage(row);
                    policies.Add(response);
                }
                return policies;
            }
            else
            {
                return new List<StageModel>();
            }
        }

        private async Task<StageModel> MapperToStage(DataRow dr)
        {
            return await Task.Run(() =>
            {
                StageModel model = new StageModel
                {
                    Code = Convert.ToInt32(dr["COD_ESCENARIO"]),
                    Name = dr["NOMBRE_ESCENARIO"] is DBNull ? string.Empty : dr["NOMBRE_ESCENARIO"].ToString(),
                    Description = dr["DESCRIPCION"] is DBNull ? string.Empty : dr["DESCRIPCION"].ToString(),
                    Impact = dr["IMPACTO"] is DBNull ? string.Empty : dr["IMPACTO"].ToString(),
                    Conciliation = Convert.ToInt32(dr["COD_CONCILIACION"]),
                    CreationDate = dr["FECHA_CREACION"] is DBNull ? DateTime.Now : Convert.ToDateTime(dr["FECHA_CREACION"]),
                    ModifieldDate = dr["FECHA_ACTUALIZACION"] is DBNull ? DateTime.Now : Convert.ToDateTime(dr["FECHA_ACTUALIZACION"]),
                    State = dr["ESTADO_ESCENARIO"] is DBNull ? false : Convert.ToBoolean(Convert.ToInt32(dr["ESTADO_ESCENARIO"])),
                    ConciliationName = dr["NOMBRE_CONCILIACION"] is DBNull ? string.Empty : dr["NOMBRE_CONCILIACION"].ToString(),
                    StateConciliation = dr["ESTADO_CONCILIACION"] is DBNull ? false : Convert.ToBoolean(Convert.ToInt32(dr["ESTADO_CONCILIACION"]))
                };
                return model;
            });
        }

        public async Task<int> SelectCountStage()
        {
            try
            {
                return await _countStage.SelectCountStage();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<StageSummaryModel>> SelectAllSummaryStage()
        {
            try
            {
                List<StageSummaryModel> mapper = new List<StageSummaryModel>();
                return await Task.Run(async () =>
                {
                    var response = await _getAllSummaryStage.SelectAllSummaryStage();
                    mapper = await MapperSummaryStage(response);

                    return mapper;
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<StageSummaryModel>> SelectPaginatorSummaryStage(int page, int row)
        {
            try
            {
                var response = await _getPaginatorSummaryStage.SelectPaginatorSummaryStage(page, row);
                var mapper = await MapperSummaryStage(response);
                return mapper;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<StageSummaryModel> SelectOneSummaryStage(StageSummaryModel entity)
        {
            try
            {
                StageSummaryModel mapper = new StageSummaryModel();
                return await Task.Run(async () =>
                {
                    var response = await _getOneSummaryStage.SelectOneSummaryStage(entity);
                    foreach (DataRow row in response.Rows)
                    {
                        mapper = await MapperToSummaryStage(row);
                    }
                    return mapper;
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdateAmountBenefitSummaryStage(StageSummaryModel entity)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _modifyAmountBenefitSummaryStage.UpdateAmountBenefitSummaryStage(entity);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdateAmountImpactSummaryStage(StageSummaryModel entity)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _modifyAmountImpactSummaryStage.UpdateAmountImpactSummaryStage(entity);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<List<StageSummaryModel>> MapperSummaryStage(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                List<StageSummaryModel> stageSummary = new List<StageSummaryModel>();
                foreach (DataRow row in dt.Rows)
                {
                    var response = await MapperToSummaryStage(row);
                    stageSummary.Add(response);
                }
                return stageSummary;
            }
            else
            {
                return new List<StageSummaryModel>();
            }
        }

        private async Task<StageSummaryModel> MapperToSummaryStage(DataRow dr)
        {
            return await Task.Run(() =>
            {
                StageSummaryModel model = new StageSummaryModel
                {
                    ConciliationSK = dr["SK_CONCILIACION"] is DBNull ? 0 : Convert.ToInt32(dr["SK_CONCILIACION"]),
                    StageSK = dr["SK_ESCENARIO"] is DBNull ? 0 : Convert.ToInt32(dr["SK_ESCENARIO"]),
                    ConciliationCode = dr["COD_CONCILIACION"] is DBNull ? string.Empty : dr["COD_CONCILIACION"].ToString(),
                    StageCode = dr["COD_ESCENARIO"] is DBNull ? string.Empty : dr["COD_ESCENARIO"].ToString(),
                    StatusConciliation = dr["ESTADO_CONCILIACION"] is DBNull ? string.Empty : dr["ESTADO_CONCILIACION"].ToString(),
                    ValueBeneficiary = dr["VAL_BENEFICIO"] is DBNull ? string.Empty : dr["VAL_BENEFICIO"].ToString(),
                    ValueInconsistency = dr["VAL_INCONSISTENCIAS"] is DBNull ? string.Empty : dr["VAL_INCONSISTENCIAS"].ToString(),
                    ValuePqr = dr["VAL_PQR"] is DBNull ? string.Empty : dr["VAL_PQR"].ToString(),
                    ValueRepetition = dr["VAL_REINCIDENCIAS"] is DBNull ? string.Empty : dr["VAL_REINCIDENCIAS"].ToString(),
                    UploadDate = dr["FEC_CARGA_DWH"] is DBNull ? DateTime.Now : Convert.ToDateTime(dr["FEC_CARGA_DWH"]),
                    AmountBenefit = dr["MONTO_BENEFICIO"] is DBNull ? 0 : Convert.ToInt32(dr["MONTO_BENEFICIO"]),
                    AmountImpact = dr["MONTO_IMPACTO"] is DBNull ? 0 : Convert.ToInt32(dr["MONTO_IMPACTO"])
                };
                return model;
            });
        }
    }
}
