using Microsoft.AspNetCore.Mvc.Rendering;
using ParameterControl.Models.Conciliation;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Policy;
using ParameterControl.Models.Scenery;
using ParameterControl.Services.Conciliations;
using System.Data.Common;
using System.Reflection;
using modScenarios = ParameterControl.Models.Scenery;
using modConciliation = ParameterControl.Models.Conciliation;


namespace ParameterControl.Services.Scenarios
{
    public class ScenariosServices : IScenariosServices
    {
        private List<Scenery> scenarios = new List<Scenery>();
        private readonly IConciliationsServices conciliationsServices;

        public ScenariosServices(
            IConciliationsServices conciliationsServices
        )
        {
            scenarios = new List<Scenery>()
            {
                new Scenery(){
                    Id = "1",
                    Code = "ESC_AIC_001",
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Scenery(){
                    Id = "2",
                   Code = "ESC_AIC_001",
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Scenery(){
                    Id = "3",
                   Code = "ESC_AIC_001",
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Scenery(){
                    Id = "4",
                   Code = "ESC_AIC_001",
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Scenery(){
                    Id = "5",
                    Code = "ESC_AIC_002",
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Scenery(){
                    Id = "6",
                   Code = "ESC_AIC_001",
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Scenery(){
                    Id = "7",
                    Code = "ESC_AIC_001",
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Scenery(){
                    Id = "8",
                    Code = "ESC_AIC_001",
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                }
            };
            this.conciliationsServices = conciliationsServices;
        }

        public async Task<List<Scenery>> GetScenarios()
        {
            return scenarios;
        }

        public async Task<List<SceneryViewModel>> GetScenariosFormat(List<modScenarios.Scenery> scenarios)
        {
            List<SceneryViewModel> scenariosModel = new List<SceneryViewModel>();

            foreach (modScenarios.Scenery scenary in scenarios)
            {
                SceneryViewModel scenaryModel = new SceneryViewModel();

                scenaryModel.Id = scenary.Id;
                scenaryModel.Code = scenary.Code;
                scenaryModel.Name = scenary.Name;
                scenaryModel.Impact = scenary.Impact;
                scenaryModel.Conciliation = scenary.Conciliation;
                scenaryModel.State = scenary.State;
                scenaryModel.StateFormat = scenary.State ? "Activo" : "Inactivo";
                scenaryModel.CreationDate = scenary.CreationDate;
                scenaryModel.UpdateDate = scenary.UpdateDate;

                scenariosModel.Add(scenaryModel);
            }

            return scenariosModel;
        }

        public async Task<Scenery> GetSceneryById(string id)
        {
            Scenery scenery = scenarios.Find(scenery => scenery.Id == id);
            return scenery;
        }

        public async Task<List<SceneryViewModel>> GetFilterScenarios(FilterViewModel filterModel)
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
            return await GetScenariosFormat(scenariosFilter);
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

        public async Task<List<SelectListItem>> GetImpact()
        {
            List<SelectListItem> impact = new List<SelectListItem>().ToList();
            impact.Add(new SelectListItem("CLIENTE", "CLIENTE"));
            impact.Add(new SelectListItem("COMPAÑIA", "COMPAÑIA"));
            impact.Add(new SelectListItem("CONFIGURACIÓN", "CONFIGURACIÓN"));

            return impact;
        }

        public async Task<List<modConciliation.Conciliation>> GetConciliation()
        {
            List<modConciliation.Conciliation> conciliation = await conciliationsServices.GetConciliations();
            return conciliation;
        }
    }
}
