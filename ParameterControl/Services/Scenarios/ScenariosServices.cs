using ParameterControl.Models.Filter;
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

        public async Task<List<Scenery>> GetFilterScenarios(FilterViewModel filterModel)
        {
            List<Scenery> scenariosFilter = new List<Scenery>();

            if ((filterModel.ColumValue == null || filterModel.ColumValue == "" || filterModel.ValueFilter == null || filterModel.ValueFilter == ""))
            {
                scenariosFilter = scenarios;
            }
            else
            {
                switch (filterModel.ColumValue)
                {
                    case "Code":
                        scenariosFilter = applyFilter(filterModel);
                        break;
                    case "Name":
                        scenariosFilter = applyFilter(filterModel);
                        break;
                    case "Impact":
                        scenariosFilter = applyFilter(filterModel);
                        break;
                    case "Conciliation":
                        scenariosFilter = applyFilter(filterModel);
                        break;
                    case "Query":
                        scenariosFilter = applyFilter(filterModel);
                        break;
                    case "Parameter":
                        scenariosFilter = applyFilter(filterModel);
                        break;
                    default:
                        break;
                }
            }

            return scenariosFilter;
        }

        private List<Scenery> applyFilter(FilterViewModel filterModel)
        {
            var property = typeof(Scenery).GetProperty(filterModel.ColumValue);

            List<Scenery> scenariosFilter = new List<Scenery>();

            foreach (Scenery scenery in scenarios)
            {
                if (property.GetValue(scenery).ToString().ToUpper().Contains(filterModel.ValueFilter.ToUpper()))
                {
                    scenariosFilter.Add(scenery);
                }
            }

            return scenariosFilter;
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
