using ParameterControl.Models.Policy;
using ParameterControl.Models.Scenery;
using ParameterControl.Services.Conciliations;
using System.Data.Common;
using System.Reflection;


namespace ParameterControl.Services.Scenarios
{
    public class ScenariosServices : IScenariosServices
    {
        private List<Scenery> scenarios = new List<Scenery>();
        public ScenariosServices()
        {
            scenarios = new List<Scenery>()
            {
                new Scenery(){
                    Id = "1",
                    Code = "ESC_AIC_001",
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    Query = "Ver Query",
                    Parameter = "Ver Parametro",
                    State = true
                },
                new Scenery(){
                    Id = "2",
                   Code = "ESC_AIC_001",
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    Query = "Ver Query",
                    Parameter = "Ver Parametro",
                    State = false
                },
                new Scenery(){
                    Id = "3",
                   Code = "ESC_AIC_001",
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    Query = "Ver Query",
                    Parameter = "Ver Parametro",
                    State = true
                },
                new Scenery(){
                    Id = "4",
                   Code = "ESC_AIC_001",
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    Query = "Ver Query",
                    Parameter = "Ver Parametro",
                    State = false
                },
                new Scenery(){
                    Id = "5",
                    Code = "ESC_AIC_001",
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    Query = "Ver Query",
                    Parameter = "Ver Parametro",
                    State = true
                },
                new Scenery(){
                    Id = "6",
                   Code = "ESC_AIC_001",
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    Query = "Ver Query",
                    Parameter = "Ver Parametro",
                    State = false
                },
                new Scenery(){
                    Id = "7",
                    Code = "ESC_AIC_001",
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    Query = "Ver Query",
                    Parameter = "Ver Parametro",
                    State = true
                },
                new Scenery(){
                    Id = "8",
                    Code = "ESC_AIC_001",
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    Query = "Ver Query",
                    Parameter = "Ver Parametro",
                    State = false
                }
            };
        }

        public async Task<List<Scenery>> GetScenarios()
        {
            return scenarios;
        }

        public async Task<Scenery> GetSceneryById(string id)
        {
            Scenery scenery = scenarios.Find(scenery => scenery.Id == id);
            return scenery;
        }
        public async Task<List<string>> GetImpact()
        {
            List<string> impact = new List<string>()
           {
               "CLIENTE",
                "COMPAÑIA ",
                "CONFIGURACIÓN "
           };
            return impact;
        }
        public async Task<List<string>> GetConciliation()
        {
            List<string> conciliation = new List<string>()
            {
                "Conciliacion1",
                "Conciliacion2",
                "Conciliacion3"
            };
            return conciliation;
        }


    }
}
