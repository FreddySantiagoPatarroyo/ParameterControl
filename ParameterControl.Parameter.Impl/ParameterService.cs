using Microsoft.Extensions.Configuration;
using ParameterControl.Parameter.DataAccess;
using ParameterControl.Parameter.Entities;
using ParameterControl.Parameter.Interfaces;
using System.Data;

namespace ParameterControl.Parameter.Impl
{
    public class ParameterService : IParameterService
    {
        private readonly SetParameter _setParameter;
        private readonly RemoveParameter _removeParameter;
        private readonly ModifyParameter _modifyParameter;
        private readonly GetByIdParameter _getByIdParameter;
        private readonly GetByConciliationParameters _getByConciliationParameter;
        private readonly GetAllParameter _getAllParameter;
        private readonly GetPaginatorParameter _getPaginatorParameter;
        private readonly CountParameter _countParameter;

        public ParameterService(IConfiguration configuration)
        {
            _setParameter = new SetParameter(configuration);
            _removeParameter = new RemoveParameter(configuration);
            _modifyParameter = new ModifyParameter(configuration);
            _getByIdParameter = new GetByIdParameter(configuration);
            _getByConciliationParameter = new GetByConciliationParameters(configuration);
            _getAllParameter = new GetAllParameter(configuration);
            _getPaginatorParameter = new GetPaginatorParameter(configuration);
            _countParameter = new CountParameter(configuration);
        }

        public async Task<int> InsertParameter(ParameterModel entity)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _setParameter.InsertParameter(entity);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> DeleteParameter(ParameterModel entity)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _removeParameter.DeleteParameter(entity);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ParameterModel>> SelectAllParameter()
        {
            try
            {
                List<ParameterModel> mapper = new List<ParameterModel>();
                return await Task.Run(async () =>
                {
                    var response = await _getAllParameter.SelectAllParameter();
                    mapper = await MapperParameter(response);

                    return mapper;
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ParameterModel> SelectByIdParameter(ParameterModel entity)
        {
            try
            {
                ParameterModel mapper = new ParameterModel();
                return await Task.Run(async () =>
                {
                    var response = await _getByIdParameter.SelectByIdParameter(entity);
                    foreach (DataRow row in response.Rows)
                    {
                        mapper = await MapperToParameter(row);
                    }
                    return mapper;
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ParameterModel>> SelectByConciliationParameter(string entity)
        {
            try
            {
                List<ParameterModel> mapper = new List<ParameterModel>();
                return await Task.Run(async () =>
                {
                    var response = await _getByConciliationParameter.SelectByConciliationParameters(entity);
                    mapper = await MapperParameter(response);

                    return mapper;
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdateParameter(ParameterModel entity)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _modifyParameter.UpdateParameter(entity);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ParameterModel>> SelectPaginatorParameter(int page, int row)
        {
            try
            {
                var response = await _getPaginatorParameter.SelectPaginatorParameter(page, row);
                var mapper = await MapperParameter(response);
                return mapper;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<List<ParameterModel>> MapperParameter(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                List<ParameterModel> policies = new List<ParameterModel>();
                foreach (DataRow row in dt.Rows)
                {
                    var response = await MapperToParameter(row);
                    policies.Add(response);
                }
                return policies;
            }
            else
            {
                return new List<ParameterModel>();
            }
        }

        private async Task<ParameterModel> MapperToParameter(DataRow dr)
        {
            return await Task.Run(() =>
            {
                ParameterModel model = new ParameterModel
                {
                    Code = Convert.ToInt32(dr["COD_PARAMETRO"]),
                    Parameter = dr["PARAMETRO"] is DBNull ? string.Empty : dr["PARAMETRO"].ToString(),
                    Value = dr["VALOR"] is DBNull ? string.Empty : dr["VALOR"].ToString(),
                    Description = dr["DESCRIPCION"] is DBNull ? string.Empty : dr["DESCRIPCION"].ToString(),
                    ParameterType = dr["TIPO"] is DBNull ? string.Empty : dr["TIPO"].ToString(),
                    CreationDate = dr["FECHA_CREACION"] is DBNull ? DateTime.Now : Convert.ToDateTime(dr["FECHA_CREACION"]),
                    ModifieldDate = dr["FECHA_ACTUALIZACION"] is DBNull ? DateTime.Now : Convert.ToDateTime(dr["FECHA_ACTUALIZACION"]),
                    ModifieldBy = dr["MODIFICADO_POR"] is DBNull ? string.Empty : dr["MODIFICADO_POR"].ToString(),
                    State = dr["ESTADO_ACTIVACION"] is DBNull ? false : Convert.ToBoolean(dr["ESTADO_ACTIVACION"]),
                };
                return model;
            });
        }

        public async Task<int> SelectCountParameter()
        {
            try
            {
                return await _countParameter.SelectCountParameter();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
