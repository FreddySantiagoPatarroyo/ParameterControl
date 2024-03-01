using Microsoft.AspNetCore.Mvc.Rendering;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Models.Parameter;
using ParameterControl.Parameter.Entities;
using ParameterControl.Parameter.Impl;
using ParameterControl.Parameter.Interfaces;
using ParameterControl.Services.Conciliations;
using modConciliation = ParameterControl.Models.Conciliation;
using modParameter = ParameterControl.Models.Parameter;

namespace ParameterControl.Services.Parameters
{
    public class ParametersService : IParametersService
    {
        private List<modParameter.Parameter> parameters = new List<modParameter.Parameter>();
        private readonly IParameterService _parameterServices;
        private readonly IConciliationsServices conciliationsServices;

        public ParametersService(
            IConfiguration configuration,
            IConciliationsServices conciliationsServices
        )
        {
            _parameterServices = new ParameterService(configuration);
            this.conciliationsServices = conciliationsServices;
            parameters = new List<modParameter.Parameter>()
            {
                new modParameter.Parameter(){
                    Code = 1,
                    Parameter_ = "Parameter1",
                    ParameterType = "OTRO",
                    List = "Ejemplo List",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new modParameter.Parameter(){
                    Code = 2,
                    Parameter_ = "Parameter2",
                    ParameterType = "GENERAL",
                    List = "Ejemplo List",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                }
            };
        }

        public async Task<List<modParameter.Parameter>> GetParameters()
        {
            var collectionParameters = await _parameterServices.SelectAllParameter();
            var response = await MapperParameter(collectionParameters);
            return response;
        }

        public async Task<int> CountParameters()
        {
            var collectionParameters = await _parameterServices.SelectAllParameter();
            return collectionParameters.Count();
        }

        public async Task<List<modParameter.Parameter>> GetParametersPagination(PaginationViewModel pagination)
        {
            try
            {
                var response = await _parameterServices.SelectPaginatorParameter(pagination.Page, pagination.RecordsPage);
                var result = await MapperParameter(response);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ParameterViewModel>> GetParametersFormat(List<modParameter.Parameter> parameters)
        {
            List<ParameterViewModel> parametersModel = new List<ParameterViewModel>();

            foreach (modParameter.Parameter parameter in parameters)
            {
                ParameterViewModel parameterModel = new ParameterViewModel();
                parameterModel.Code = parameter.Code;
                parameterModel.Parameter_ = parameter.Parameter_;
                parameterModel.Value = parameter.Value;
                parameterModel.Description = parameter.Description;
                parameterModel.ParameterType = parameter.ParameterType;
                parameterModel.List = parameter.List;
                parameterModel.State = parameter.State;
                parameterModel.StateFormat = parameter.State ? "Activo" : "Inactivo";
                parameterModel.CreationDate = parameter.CreationDate;
                parameterModel.UpdateDate = parameter.UpdateDate;
                parameterModel.CreationDateFormat = parameter.CreationDate.ToString("dd/MM/yyyy");
                parameterModel.UpdateDateFormat = parameter.UpdateDate.ToString("dd/MM/yyyy");

                parametersModel.Add(parameterModel);
            }

            return parametersModel;
        }

        public async Task<ParameterViewModel> GetParameterFormat(modParameter.Parameter parameter)
        {
            ParameterViewModel parameterModel = new ParameterViewModel();
            parameterModel.Code = parameter.Code;
            parameterModel.Parameter_ = parameter.Parameter_;
            parameterModel.Value = parameter.Value;
            parameterModel.Description = parameter.Description;
            parameterModel.ParameterType = parameter.ParameterType;
            parameterModel.List = parameter.List;
            parameterModel.State = parameter.State;
            parameterModel.StateFormat = parameter.State ? "Activo" : "Inactivo";
            parameterModel.CreationDate = parameter.CreationDate;
            parameterModel.UpdateDate = parameter.UpdateDate;
            parameterModel.CreationDateFormat = parameter.CreationDate.ToString("dd/MM/yyyy");
            parameterModel.UpdateDateFormat = parameter.UpdateDate.ToString("dd/MM/yyyy");

            return parameterModel;
        }

        public async Task<ParameterCreateViewModel> GetParameterFormatCreate(modParameter.Parameter parameter)
        {
            ParameterCreateViewModel parameterModel = new ParameterCreateViewModel();
            parameterModel.Code = parameter.Code;
            parameterModel.Parameter_ = parameter.Parameter_;
            parameterModel.Value = parameter.Value;
            parameterModel.Description = parameter.Description;
            parameterModel.ParameterType = parameter.ParameterType;
            parameterModel.List = parameter.List;
            parameterModel.State = parameter.State;
            parameterModel.CreationDate = parameter.CreationDate;
            parameterModel.UpdateDate = parameter.UpdateDate;

            return parameterModel;
        }

        public async Task<modParameter.Parameter> GetParameterByCode(int code)
        {
            var response = await _parameterServices.SelectByIdParameter(new ParameterModel { Code = code });
            var parameter = await MapperToParameter(response);
            return parameter;
        }

        public async Task<List<modParameter.Parameter>> GetParametersByConciliation(string conciliation)
        {
            var collectionParameters = await _parameterServices.SelectByConciliationParameter(conciliation);
            var response = await MapperParameter(collectionParameters);
            return response;
        }

        public async Task<List<ParameterViewModel>> GetFilterParameters(FilterViewModel filterModel)
        {
            List<modParameter.Parameter> allParameters = await GetParameters();
            List<ParameterViewModel> parametersFilter = await GetParametersFormat(allParameters);

            if (filterModel.ColumValue == null || filterModel.ColumValue == "" || filterModel.ValueFilter == null || filterModel.ValueFilter == "")
            {
                return parametersFilter;
            }
            else
            {
                parametersFilter = await applyFilter(filterModel, parametersFilter);
            }

            return parametersFilter;
        }

        private async Task<List<ParameterViewModel>> applyFilter(FilterViewModel filterModel, List<ParameterViewModel> allParameters)
        {
            var property = typeof(ParameterViewModel).GetProperty(filterModel.ColumValue);

            List<ParameterViewModel> parametersFilter = new List<ParameterViewModel>();
            if (filterModel.TypeRow == "Select")
            {
                foreach (ParameterViewModel parameter in allParameters)
                {
                    if (property.GetValue(parameter).ToString().ToUpper() == filterModel.ValueFilter.ToUpper())
                    {
                        parametersFilter.Add(parameter);
                    }
                }
            }
            else
            {
                foreach (ParameterViewModel parameter in allParameters)
                {
                    if (property.GetValue(parameter).ToString().ToUpper().Contains(filterModel.ValueFilter.ToUpper()))
                    {
                        parametersFilter.Add(parameter);
                    }
                }
            }

            return parametersFilter;
        }

        public List<ParameterViewModel> GetFilterPagination(List<ParameterViewModel> inicialParameters, PaginationViewModel paginationViewModel, int totalData)
        {
            var limit = paginationViewModel.Page * paginationViewModel.RecordsPage;
            var index = limit - paginationViewModel.RecordsPage;
            var count = 0;

            if (limit > totalData)
            {
                count = totalData - index;
            }
            else
            {
                count = paginationViewModel.RecordsPage;
            }

            List<ParameterViewModel> parametersFilterPagination = inicialParameters.GetRange(index, count);

            return parametersFilterPagination;
        }

        public async Task<List<SelectListItem>> GetParameterType()
        {
            List<SelectListItem> parameterType = new List<SelectListItem>().ToList();
            parameterType.Add(new SelectListItem("ESCENARIO", "ESCENARIO"));
            parameterType.Add(new SelectListItem("PARÁMETROS SEGURIDAD", "PARÁMETROS SEGURIDAD"));
            parameterType.Add(new SelectListItem("PARÁMETROS SISTEMA", "PARÁMETROS SISTEMA"));
            parameterType.Add(new SelectListItem("PARÁMETROS CONCILIACIÓN", "PARÁMETROS CONCILIACIÓN"));

            return parameterType;
        }
        public async Task<List<modParameter.Parameter>> GetListParameter()
        {
            //Crear funcion para buscar parametros segun el tipo de parametro
            List<modParameter.Parameter> listParameter = await GetParameters();

            return listParameter;
        }

        public async Task<List<modConciliation.Conciliation>> GetConciliations()
        {
            List<modConciliation.Conciliation> conciliations = await conciliationsServices.GetConciliations();
            return conciliations;
        }

        private async Task<List<modParameter.Parameter>> MapperParameter(List<ParameterModel> ParameterModel)
        {
            return await Task.Run(() =>
            {
                List<modParameter.Parameter> Parameters = new List<modParameter.Parameter>();
                if (ParameterModel.Count > 0)
                {
                    foreach (var Parameter in ParameterModel)
                    {
                        Parameters.Add(MapperToParameter(Parameter).Result);
                    }
                }
                return Parameters;
            });
        }

        private async Task<modParameter.Parameter> MapperToParameter(ParameterModel Parameter)
        {
            return await Task.Run(() =>
            {
                modParameter.Parameter model = new modParameter.Parameter
                {
                    Code = Convert.ToInt32(Parameter.Code),
                    Parameter_ = Parameter.Parameter,
                    Value = Parameter.Value,
                    Description = Parameter.Description,
                    ParameterType = Parameter.ParameterType,
                    CreationDate = Parameter.CreationDate,
                    UpdateDate = Parameter.ModifieldDate,
                    UserOwner = Parameter.ModifieldBy,
                    State = Parameter.State,

                };
                return model;
            });
        }

        public async Task<string> InsertParameter(modParameter.Parameter request)
        {
            ParameterModel Parameter = new ParameterModel
            {
                Parameter = request.Parameter_,
                ParameterType = request.ParameterType,
                Value = request.Value,
                Description = request.Description,
                CreationDate = DateTime.Now,
                ModifieldDate = DateTime.Now,
                ModifieldBy = "CreateToUserDev",
                State = request.State,

            };
            var response = await _parameterServices.InsertParameter(Parameter);

            return response.Equals(1) ? "Parametro creado correctamente" : "Error creando el Parametro";
        }

        public async Task<string> UpdateParameter(modParameter.Parameter Parameter)
        {
            var mapping = await MapperUpdateParameter(Parameter);
            var response = await _parameterServices.UpdateParameter(mapping);

            return response.Equals(1) ? "Parametro actualizado correctamente" : "Error actualizando el Parametro";
        }

        public async Task<string> ActiveParameter(modParameter.Parameter Parameter)
        {
            var mapping = await MapperActiveParameter(Parameter);
            var response = await _parameterServices.UpdateParameter(mapping);

            return response.Equals(1) ? "Parametro activado correctamente" : "Error activando el Parametro";
        }

        public async Task<string> DesactiveParameter(modParameter.Parameter Parameter)
        {
            var mapping = await MapperDesctiveParameter(Parameter);
            var response = await _parameterServices.UpdateParameter(mapping);

            return response.Equals(1) ? "Parametro desactivado correctamente" : "Error desactivando el Parametro";
        }

        private async Task<ParameterModel> MapperUpdateParameter(modParameter.Parameter Parameter)
        {
            return await Task.Run(() =>
            {
                ParameterModel model = new ParameterModel
                {
                    Code = Parameter.Code,
                    Parameter = Parameter.Parameter_,
                    ParameterType = Parameter.ParameterType,
                    Value = Parameter.Value,
                    Description = Parameter.Description,
                    CreationDate = Parameter.CreationDate,
                    ModifieldDate = DateTime.Now,
                    ModifieldBy = "CreateToUserDev",
                    State = Parameter.State,
                };
                return model;
            });
        }

        private async Task<ParameterModel> MapperActiveParameter(modParameter.Parameter Parameter)
        {
            return await Task.Run(() =>
            {
                ParameterModel model = new ParameterModel
                {
                    Code = Parameter.Code,
                    Parameter = Parameter.Parameter_,
                    ParameterType = Parameter.ParameterType,
                    Value = Parameter.Value,
                    Description = Parameter.Description,
                    CreationDate = Parameter.CreationDate,
                    ModifieldDate = DateTime.Now,
                    ModifieldBy = "CreateToUserDev",
                    State = true,
                };
                return model;
            });
        }

        private async Task<ParameterModel> MapperDesctiveParameter(modParameter.Parameter Parameter)
        {
            return await Task.Run(() =>
            {
                ParameterModel model = new ParameterModel
                {
                    Code = Parameter.Code,
                    Parameter = Parameter.Parameter_,
                    ParameterType = Parameter.ParameterType,
                    Value = Parameter.Value,
                    Description = Parameter.Description,
                    CreationDate = Parameter.CreationDate,
                    ModifieldDate = DateTime.Now,
                    ModifieldBy = "CreateToUserDev",
                    State = false,
                };
                return model;
            });
        }
    }
}
