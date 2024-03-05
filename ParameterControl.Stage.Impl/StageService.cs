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

        public StageService(IConfiguration configuration)
        {
            _setStage = new SetStage(configuration);
            _removeStage = new RemoveStage(configuration);
            _modifyStage = new ModifyStage(configuration);
            _getByIdStage = new GetByIdStage(configuration);
            _getAllStage = new GetAllStage(configuration);
            _getPaginatorStage = new GetPaginatorStage(configuration);
            _countStage = new CountStage(configuration);
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
                    StateConciliation = dr["ESTADO_ACTIVACION_CONCILIACION"] is DBNull ? false : Convert.ToBoolean(dr["ESTADO_ACTIVACION_CONCILIACION"])
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
    }
}
