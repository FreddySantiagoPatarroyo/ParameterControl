using Microsoft.AspNetCore.Mvc.Rendering;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Parameter;
using modParameter = ParameterControl.Models.Parameter;

namespace ParameterControl.Services.Parameters
{
    public class ParametersService : IParametersService
    {
        private List<Parameter> parameters = new List<Parameter>();
        public ParametersService()
        {
            parameters = new List<Parameter>()
            {
                new Parameter(){
                    Code = 1,
                    Parameter_ = "Parameter1",
                    ParameterType = "OTRO",
                    List = "Ejemplo List",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Parameter(){
                    Code = 2,
                    Parameter_ = "Parameter2",
                    ParameterType = "GENERAL",
                    List = "Ejemplo List",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Parameter(){
                    Code = 3,
                    Parameter_ = "Parameter3",
                    ParameterType = "GENERAL",
                    List = "Ejemplo List",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Parameter(){
                    Code = 4,
                    Parameter_ = "Parameter4",
                    ParameterType = "GENERAL",
                    List = "Ejemplo List",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Parameter(){
                    Code = 5,
                    Parameter_ = "Parameter5",
                    ParameterType = "GENERAL",
                    List = "Ejemplo List",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Parameter(){
                    Code = 6,
                    Parameter_ = "Parameter6",
                    ParameterType = "GENERAL",
                    List = "Ejemplo List",
                    Description = "Descripcion ejemplo",
                    Value = "ACT",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Parameter(){
                    Code = 7,
                    Parameter_ = "Parameter7",
                    ParameterType = "GENERAL",
                    List = "Ejemplo List",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Parameter(){
                    Code = 8,
                    Parameter_ = "Parameter8",
                    ParameterType = "GENERAL",
                    List = "Ejemplo List",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                }
            };
        }

        public async Task<List<Parameter>> GetParameters()
        {
            return parameters;
        }

        public async Task<List<ParameterViewModel>> GetParametersFormat(List<modParameter.Parameter> parameters)
        {
            List<ParameterViewModel> parametersModel = new List<ParameterViewModel>();

            foreach (modParameter.Parameter parameter in parameters)
            {
                ParameterViewModel parameterModel = new ParameterViewModel();

                parameterModel.Code = parameter.Code;
                parameterModel.Value = parameter.Value;
                parameterModel.Description = parameter.Description;
                parameterModel.ParameterType = parameter.ParameterType;
                parameterModel.List = parameter.List;
                parameterModel.State = parameter.State;
                parameterModel.ParameterFormat = "V_" + parameter.Parameter_;
                parameterModel.StateFormat = parameter.State ? "Activo" : "Inactivo";
                parameterModel.CreationDate = parameter.CreationDate;
                parameterModel.UpdateDate = parameter.UpdateDate;

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
            parameterModel.ParameterFormat = "V_" + parameter.Parameter_;
            parameterModel.StateFormat = parameter.State ? "Activo" : "Inactivo";
            parameterModel.CreationDate = parameter.CreationDate;
            parameterModel.UpdateDate = parameter.UpdateDate;

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
            parameterModel.ParameterFormat = "V_" + parameter.Parameter_;
            parameterModel.State = parameter.State;
            parameterModel.CreationDate = parameter.CreationDate;
            parameterModel.UpdateDate = parameter.UpdateDate;

            return parameterModel;
        }

        public async Task<Parameter> GetParameterByCode(int code)
        {
            Parameter parameter = parameters.Find(parameter => parameter.Code == code);
            return parameter;
        }

        public async Task<List<ParameterViewModel>> GetFilterParameters(FilterViewModel filterModel)
        {
            List<Parameter> parametersFilter = new List<Parameter>();

            if ((filterModel.ColumValue == null || filterModel.ColumValue == "" || filterModel.ValueFilter == null || filterModel.ValueFilter == ""))
            {
                parametersFilter = parameters;
            }
            else
            {
                switch (filterModel.ColumValue)
                {
                    case "ParameterType":
                        parametersFilter = applyFilter(filterModel);
                        break;
                    case "Value":
                        parametersFilter = applyFilter(filterModel);
                        break;
                    case "Description":
                        parametersFilter = applyFilter(filterModel);
                        break;
                    case "Code":
                        parametersFilter = applyFilter(filterModel);
                        break;
                    default:
                        break;
                }
            }

            return await GetParametersFormat(parametersFilter);
        }

        private List<Parameter> applyFilter(FilterViewModel filterModel)
        {
            var property = typeof(Parameter).GetProperty(filterModel.ColumValue);

            List<Parameter> parametersFilter = new List<Parameter>();

            foreach (Parameter parameter in parameters)
            {
                if (property.GetValue(parameter).ToString().ToUpper().Contains(filterModel.ValueFilter.ToUpper()))
                {
                    parametersFilter.Add(parameter);
                }
            }

            return parametersFilter;
        }

        public async Task<List<SelectListItem>> GetParameterType()
        {
            List<SelectListItem> parameterType = new List<SelectListItem>().ToList();
            parameterType.Add(new SelectListItem("GENERAL", "GENERAL"));
            parameterType.Add(new SelectListItem("ESCENARIO", "ESCENARIO"));
            parameterType.Add(new SelectListItem("PARÁMETROS SEGURIDAD", "PARÁMETROS SEGURIDAD"));
            parameterType.Add(new SelectListItem("PARÁMETROS SISTEMA", "PARÁMETROS SISTEMA"));
            parameterType.Add(new SelectListItem("PARÁMETROS CONCILIACIÓN", "PARÁMETROS CONCILIACIÓN"));

            return parameterType;
        }
        public async Task<List<Parameter>> GetListParameter()
        {
            //Crear funcion para buscar parametros segun el tipo de parametro
            List<Parameter> listParameter = await GetParameters();

            return listParameter;
        }
    }
}
