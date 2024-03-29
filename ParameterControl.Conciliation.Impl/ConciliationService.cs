﻿using Microsoft.Extensions.Configuration;
using ParameterControl.Conciliation.DataAccess;
using ParameterControl.Conciliation.Entities;
using ParameterControl.Conciliation.Interfaces;
using System.Data;

namespace ParameterControl.Conciliation.Impl
{
    public class ConciliationService : IConciliationService
    {
        private readonly SetConciliation _setConciliation;
        private readonly RemoveConciliation _removeConciliation;
        private readonly ModifyConciliation _modifyConciliation;
        private readonly GetByIdConciliation _getByIdConciliation;
        private readonly GetAllConciliation _getAllConciliation;
        private readonly GetPaginatorConciliation _getPaginatorConciliation;
        private readonly CountConciliation _countConciliation;

        public ConciliationService(IConfiguration configuration)
        {
            _setConciliation = new SetConciliation(configuration);
            _removeConciliation = new RemoveConciliation(configuration);
            _modifyConciliation = new ModifyConciliation(configuration);
            _getByIdConciliation = new GetByIdConciliation(configuration);
            _getAllConciliation = new GetAllConciliation(configuration);
            _getPaginatorConciliation = new GetPaginatorConciliation(configuration);
            _countConciliation = new CountConciliation(configuration);
        }

        public async Task<int> InsertConciliation(ConciliationModel entity)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _setConciliation.InsertConciliation(entity);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> DeleteConciliation(ConciliationModel entity)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _removeConciliation.DeleteConciliation(entity);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ConciliationModel>> SelectAllConciliation()
        {
            try
            {
                List<ConciliationModel> mapper = new List<ConciliationModel>();
                return await Task.Run(async () =>
                {
                    var response = await _getAllConciliation.SelectAllConciliation();
                    mapper = await MapperConciliation(response);

                    return mapper;
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ConciliationModel> SelectByIdConciliation(ConciliationModel entity)
        {
            try
            {
                ConciliationModel mapper = new ConciliationModel();
                return await Task.Run(async () =>
                {
                    var response = await _getByIdConciliation.SelectByIdConciliation(entity);
                    foreach (DataRow row in response.Rows)
                    {
                        mapper = await MapperToConciliation(row);
                    }
                    return mapper;
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdateConciliation(ConciliationModel entity)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _modifyConciliation.UpdateConciliation(entity);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ConciliationModel>> SelectPaginatorConciliation(int page, int row)
        {
            try
            {
                var response = await _getPaginatorConciliation.SelectPaginatorConciliation(page, row);
                var mapper = await MapperConciliation(response);
                return mapper;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<List<ConciliationModel>> MapperConciliation(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                List<ConciliationModel> policies = new List<ConciliationModel>();
                foreach (DataRow row in dt.Rows)
                {
                    var response = await MapperToConciliation(row);
                    policies.Add(response);
                }
                return policies;
            }
            else
            {
                return new List<ConciliationModel>();
            }
        }

        private async Task<ConciliationModel> MapperToConciliation(DataRow dr)
        {
            return await Task.Run(() =>
            {
                ConciliationModel model = new ConciliationModel
                {
                    Code = Convert.ToInt32(dr["COD_CONCILIACION"]),
                    ConciliationName = dr["NOMBRE_CONCILIACION"] is DBNull ? string.Empty : dr["NOMBRE_CONCILIACION"].ToString(),
                    Description = dr["DESCRIPCION"] is DBNull ? string.Empty : dr["DESCRIPCION"].ToString(),
                    Email = dr["EMAILS"] is DBNull ? string.Empty : dr["EMAILS"].ToString(),
                    TargetTable = dr["TABLA_DESTINO"] is DBNull ? string.Empty : dr["TABLA_DESTINO"].ToString(),
                    PolicyId = Convert.ToInt32(dr["COD_POLITICA"]),
                    PolicyName = dr["NOMBRE_POLITICA"] is DBNull ? string.Empty : dr["NOMBRE_POLITICA"].ToString(),
                    CreationDate = dr["FECHA_CREACION"] is DBNull ? DateTime.Now : Convert.ToDateTime(dr["FECHA_CREACION"]),
                    ModifieldDate = dr["FECHA_ACTUALIZACION"] is DBNull ? DateTime.Now : Convert.ToDateTime(dr["FECHA_ACTUALIZACION"]),
                    RequiredApproval = dr["REQUIERE_APROBACION"] is DBNull ? string.Empty : dr["REQUIERE_APROBACION"].ToString(),
                    OperationType = dr["TIPO_OPERACION"] is DBNull ? string.Empty : dr["TIPO_OPERACION"].ToString(),
                    AssignmentType = dr["TIPO_ASIGNACION"] is DBNull ? string.Empty : dr["TIPO_ASIGNACION"].ToString(),
                    State = dr["ESTADO"] is DBNull ? false : Convert.ToBoolean(Convert.ToInt32(dr["ESTADO"])),
                    StatePolicy = dr["ESTADO_POLITICA"] is DBNull ? false : Convert.ToBoolean(Convert.ToInt32(dr["ESTADO_POLITICA"])),
                };
                return model;
            });
        }

        public async Task<int> SelectCountConciliation()
        {
            try
            {
                return await _countConciliation.SelectCountConciliation();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
