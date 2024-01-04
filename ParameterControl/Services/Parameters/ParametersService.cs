using ParameterControl.Models.Parameter;

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
                    ParameterType = "Emial",
                    List = "Nombre",
                     _Parameters = "V_STATUS",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    State = true
                },
                new Parameter(){
                    Id = "2",
                     ParameterType = "Emial",
                    List = "Nombre",
                     _Parameters = "V_STATUS",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    State = false
                },
                new Parameter(){
                    Id = "3",
                    ParameterType = "Emial",
                    List = "Nombre",
                     _Parameters = "V_STATUS",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    State = true
                },
                new Parameter(){
                    Id = "4",
                     ParameterType = "Emial",
                    List = "Nombre",
                     _Parameters = "V_STATUS",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    State = false
                },
                new Parameter(){
                    Id = "5",
                     ParameterType = "Emial",
                    List = "Nombre",
                     _Parameters = "V_STATUS",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    State = true
                },
                new Parameter(){
                    Id = "6",
                    ParameterType = "Emial",
                    List = "Nombre",
                     _Parameters = "V_STATUS",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    State = false
                },
                new Parameter(){
                    Id = "7",
                     ParameterType = "Emial",
                    List = "Nombre",
                     _Parameters = "V_STATUS",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    State = true
                },
                new Parameter(){
                    Id = "8",
                    ParameterType = "Emial",
                    List = "Nombre",
                     _Parameters = "V_STATUS",
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
