using Microsoft.Extensions.Configuration;
using ParameterControl.LoadControl.DataAccess;
using ParameterControl.LoadControl.Entities;
using ParameterControl.LoadControl.Interfaces;
using System.Data;

namespace ParameterControl.LoadControl.Impl
{
    public class LoadControlService : ILoadControlService
    {
        private readonly ModifyLoadControl _modifyLoadControl;
        private readonly GetAllLoadControl _getAllLoadControl;
        private readonly GetPaginatorLoadControl _getPaginatorLoadControl;

        public LoadControlService(IConfiguration configuration)
        {
            _modifyLoadControl = new ModifyLoadControl(configuration);
            _getAllLoadControl = new GetAllLoadControl(configuration);
            _getPaginatorLoadControl = new GetPaginatorLoadControl(configuration);
        }

        public async Task<List<LoadControlModel>> SelectAllLoadControl()
        {
            try
            {
                List<LoadControlModel> mapper = new List<LoadControlModel>();
                return await Task.Run(async () =>
                {
                    var response = await _getAllLoadControl.SelectAllLoadControl();
                    mapper = await MapperLoadControl(response);

                    return mapper;
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdateLoadControl(LoadControlModel entity)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _modifyLoadControl.UpdateLoadControl(entity);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<LoadControlModel>> SelectPaginatorLoadControl(int page, int row)
        {
            try
            {
                var response = await _getPaginatorLoadControl.SelectPaginatorLoadControl(page, row);
                var mapper = await MapperLoadControl(response);
                return mapper;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<List<LoadControlModel>> MapperLoadControl(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                List<LoadControlModel> policies = new List<LoadControlModel>();
                foreach (DataRow row in dt.Rows)
                {
                    var response = await MapperToLoadControl(row);
                    policies.Add(response);
                }
                return policies;
            }
            else
            {
                return new List<LoadControlModel>();
            }
        }

        private async Task<LoadControlModel> MapperToLoadControl(DataRow dr)
        {
            return await Task.Run(() =>
            {
                LoadControlModel model = new LoadControlModel
                {
                    Package = dr["PAQUETE"] is DBNull ? string.Empty : dr["PAQUETE"].ToString(),
                    Table = dr["TABLA"] is DBNull ? string.Empty : dr["TABLA"].ToString(),
                    Periodicity = dr["PERIODICIDAD"] is DBNull ? string.Empty : dr["PERIODICIDAD"].ToString(),
                    Backup = dr["RESPALDO"] is DBNull ? string.Empty : dr["RESPALDO"].ToString(),
                    Status = dr["ESTADO"] is DBNull ? string.Empty : dr["ESTADO"].ToString(),
                    Error = dr["ERROR"] is DBNull ? string.Empty : dr["ERROR"].ToString(),
                    LastLoad = dr["ULTIMA_CARGA"] is DBNull ? DateTime.Now : Convert.ToDateTime(dr["ULTIMA_CARGA"]),
                    LastExecution = dr["ULTIMA_EJECUCION"] is DBNull ? DateTime.Now : Convert.ToDateTime(dr["ULTIMA_EJECUCION"]),
                    Session = dr["SESION"] is DBNull ? string.Empty : dr["SESION"].ToString(),
                    LocalRouteSqlUnLoad = dr["RUTA_LOCAL_SQLUNLOAD"] is DBNull ? string.Empty : dr["RUTA_LOCAL_SQLUNLOAD"].ToString(),
                    FlagSisNotStart = dr["FLAG_SISNOT_INICIO"] is DBNull ? string.Empty : dr["FLAG_SISNOT_INICIO"].ToString(),
                    FlagSisNotOk = dr["FLAG_SISNOT_OK"] is DBNull ? string.Empty : dr["FLAG_SISNOT_OK"].ToString(),
                    FlagSisNotKo = dr["FLAG_SISNOT_KO"] is DBNull ? string.Empty : dr["FLAG_SISNOT_KO"].ToString(),
                    StartDate = dr["FECHA_INICIAL"] is DBNull ? DateTime.Now : Convert.ToDateTime(dr["FECHA_INICIAL"]),
                    EndDate = dr["FECHA_FINAL"] is DBNull ? DateTime.Now : Convert.ToDateTime(dr["FECHA_FINAL"]),
                    FormulaEndDate = dr["FORMULA_FECHA_INICIAL"] is DBNull ? string.Empty : dr["FORMULA_FECHA_INICIAL"].ToString(),
                    FormulaStartDate = dr["FORMULA_FECHA_FINAL"] is DBNull ? string.Empty : dr["FORMULA_FECHA_FINAL"].ToString(),
                    FlagDrop = dr["FLAG_DROP"] is DBNull ? string.Empty : dr["FLAG_DROP"].ToString(),
                    FlagStatistics = dr["FLAG_ESTADISTICAS"] is DBNull ? string.Empty : dr["FLAG_ESTADISTICAS"].ToString(),
                    FlagDep = dr["FLAG_DEP"] is DBNull ? string.Empty : dr["FLAG_DEP"].ToString(),
                    DaysDep = dr["DIAS_DEP"] is DBNull ? string.Empty : dr["DIAS_DEP"].ToString(),
                    State = dr["ESTADO_ACTIVACION"] is DBNull ? false : Convert.ToBoolean(dr["ESTADO_ACTIVACION"]),
                };
                return model;
            });
        }
    }
}
