﻿using Microsoft.AspNetCore.Mvc.Rendering;
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
                    Code = 1,
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Scenery(){
                    Code = 2,
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Scenery(){
                    Code = 3,
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Scenery(){
                    Code = 4,
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Scenery(){
                    Code = 5,
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Scenery(){
                    Code = 6,
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Scenery(){
                    Code = 7,
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Scenery(){
                    Code = 8,
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

            foreach (modScenarios.Scenery scenery in scenarios)
            {
                SceneryViewModel sceneryModel = new SceneryViewModel();

                sceneryModel.Code = scenery.Code;
                sceneryModel.Name = scenery.Name;
                sceneryModel.Impact = scenery.Impact;
                sceneryModel.Conciliation = scenery.Conciliation;
                sceneryModel.State = scenery.State;
                sceneryModel.CodeFormat = "ESC_" + scenery.Code;
                sceneryModel.StateFormat = scenery.State ? "Activo" : "Inactivo";
                sceneryModel.CreationDate = scenery.CreationDate;
                sceneryModel.UpdateDate = scenery.UpdateDate;

                scenariosModel.Add(sceneryModel);
            }

            return scenariosModel;
        }

        public async Task<SceneryViewModel> GetSceneryFormat(modScenarios.Scenery scenery)
        {
           
                SceneryViewModel sceneryModel = new SceneryViewModel();

                sceneryModel.Code = scenery.Code;
                sceneryModel.Name = scenery.Name;
                sceneryModel.Impact = scenery.Impact;
                sceneryModel.Conciliation = scenery.Conciliation;
                sceneryModel.State = scenery.State;
                sceneryModel.CodeFormat = "ESC_" + scenery.Code;
                sceneryModel.StateFormat = scenery.State ? "Activo" : "Inactivo";
                sceneryModel.CreationDate = scenery.CreationDate;
                sceneryModel.UpdateDate = scenery.UpdateDate;

            
                return sceneryModel;
        }

        public async Task<SceneryCreateViewModel> GetSceneryFormatCreate(modScenarios.Scenery scenery)
        {

            SceneryCreateViewModel sceneryModel = new SceneryCreateViewModel();

            sceneryModel.Code = scenery.Code;
            sceneryModel.Name = scenery.Name;
            sceneryModel.Impact = scenery.Impact;
            sceneryModel.Conciliation = scenery.Conciliation;
            sceneryModel.State = scenery.State;
            sceneryModel.CodeFormat = "ESC_" + scenery.Code;
            sceneryModel.CreationDate = scenery.CreationDate;
            sceneryModel.UpdateDate = scenery.UpdateDate;


            return sceneryModel;
        }


        public async Task<Scenery> GetSceneryByCode(int code)
        {
            Scenery scenery = scenarios.Find(scenery => scenery.Code == code);
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
