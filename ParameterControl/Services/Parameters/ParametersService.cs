using ParameterControl.Models.Filter;
using ParameterControl.Models.Parameter;
using ParameterControl.Models.Policy;

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
                    Id = "1",
                    ParameterType = "OTRO",
                    List = "Ejemplo List",
                    Parameters_ = "V_STATUS",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    State = true
                },
                new Parameter(){
                    Id = "2",
                    ParameterType = "GENERAL",
                    List = "Ejemplo List",
                    Parameters_ = "V_STATUS",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    State = false
                },
                new Parameter(){
                    Id = "3",
                    ParameterType = "GENERAL",
                    List = "Ejemplo List",
                    Parameters_ = "V_STATUS",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    State = true
                },
                new Parameter(){
                    Id = "4",
                    ParameterType = "GENERAL",
                    List = "Ejemplo List",
                    Parameters_ = "V_STATUS",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    State = false
                },
                new Parameter(){
                    Id = "5",
                    ParameterType = "GENERAL",
                    List = "Ejemplo List",
                    Parameters_ = "V_STATUS",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    State = true
                },
                new Parameter(){
                    Id = "6",
                    ParameterType = "GENERAL",
                    List = "Ejemplo List",
                    Parameters_ = "V_STATUS",
                    Description = "Descripcion ejemplo",
                    Value = "ACT",
                    State = false
                },
                new Parameter(){
                    Id = "7",
                    ParameterType = "GENERAL",
                    List = "Ejemplo List",
                    Parameters_ = "V_STATUS",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    State = true
                },
                new Parameter(){
                    Id = "8",
                    ParameterType = "GENERAL",
                    List = "Ejemplo List",
                    Parameters_ = "GENERAL",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    State = false
                }
            };
        }

        public async Task<List<Parameter>> GetParameters()
        {
            return parameters;
        }

        public async Task<Parameter> GetParameterById(string id)
        {
            Parameter parameter = parameters.Find(parameter => parameter.Id == id);
            return parameter;
        }

        public async Task<List<Parameter>> GetFilterParameters(FilterViewModel filterModel)
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
                    case "Parameters_":
                        parametersFilter = applyFilter(filterModel);
                        break;
                    default:
                        break;
                }
            }

            return parametersFilter;
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

        public async Task<List<string>> GetParameterType()
        {
            List<string> parameterType = new List<string>()
           {
               "GENERAL",
               "ESCENARIO",
               "PARÁMETROS ",
               "SEGURIDAD",
               "PARÁMETROS SISTEMA ",
               "PARÁMETROS CONCILIACIÓN"

           };
            return parameterType;
        }
        public async Task<List<string>> GetListParameter()
        {
            List<string> listParameter = new List<string>()
           {
               "Ejemplo List",
               "Ejemplo Lis 2"
           };
            return listParameter;
        }
    }
}
