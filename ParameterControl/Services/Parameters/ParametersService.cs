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
                    _Parameters = "V_STATUS",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    ParameterType = "Emial",
                    State = true
                },
                new Parameter(){
                    Id = "2",
                    _Parameters = "V_STATUS",
                    Value = "DATA",
                    Description = "Descripcion ejemplo",
                    ParameterType = "ESCENARIO",
                    State = false
                },
                new Parameter(){
                    Id = "3",
                    _Parameters = "V_STATUS",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    ParameterType = "ESCENARIO",
                    State = true
                },
                new Parameter(){
                    Id = "4",
                    _Parameters = "V_STATUS",
                    Value = "DATA",
                    Description = "Descripcion ejemplo",
                    ParameterType = "ESCENARIO",
                    State = false
                },
                new Parameter(){
                    Id = "5",
                    _Parameters = "V_STATUS",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    ParameterType = "ESCENARIO",
                    State = true
                },
                new Parameter(){
                    Id = "6",
                    _Parameters = "V_STATUS",
                    Value = "DATA",
                    Description = "Descripcion ejemplo",
                    ParameterType = "ESCENARIO",
                    State = false
                },
                new Parameter(){
                    Id = "7",
                    _Parameters = "V_STATUS",
                    Value = "ACT",
                    Description = "Descripcion ejemplo",
                    ParameterType = "ESCENARIO",
                    State = true
                },
                new Parameter(){
                    Id = "8",
                    _Parameters = "V_STATUS",
                    Value = "DATA",
                    Description = "Descripcion ejemplo",
                    ParameterType = "ESCENARIO",
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
    }
}
