using Microsoft.AspNetCore.Mvc.Rendering;
using ParameterControl.Conciliation.Entities;
using ParameterControl.Models.Conciliation;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Models.Scenery;
using ParameterControl.Services.Conciliations;
using ParameterControl.Stage.Entities;
using ParameterControl.Stage.Impl;
using ParameterControl.Stage.Interfaces;
using modConciliation = ParameterControl.Models.Conciliation;

using modScenarios = ParameterControl.Models.Scenery;



namespace ParameterControl.Services.Scenarios
{
    public class ScenariosServices : IScenariosServices
    {
        private List<Scenery> scenarios = new List<Scenery>();
        private readonly IConciliationsServices conciliationsServices;
        private readonly IStageService _stageService;

        public ScenariosServices(
            IConciliationsServices conciliationsServices,
            IConfiguration configuration
        )
        {
            _stageService = new StageService(configuration);
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
                    UserOwner = "User1"
                },
                new Scenery(){
                    Code = 2,
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new Scenery(){
                    Code = 3,
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new Scenery(){
                    Code = 4,
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new Scenery(){
                    Code = 5,
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new Scenery(){
                    Code = 6,
                    Name = "ESC_AIC_001",
                    Impact = "COMPAÑIA",
                    Conciliation = "Conciliacion1",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new Scenery(){
                    Code = 7,
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new Scenery(){
                    Code = 8,
                    Name = "ESC_AIC_001",
                    Impact = "CLIENTE",
                    Conciliation = "Conciliacion1",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                }
            };
            this.conciliationsServices = conciliationsServices;
        }

        public async Task<List<Scenery>> GetScenarios()
        {
            var collectionScenarios = await _stageService.SelectAllStage();
            var response = await MapperScenery(collectionScenarios);
            return response;
        }

        public async Task<int> CountScenarios()
        {
            var collectionScenarios = await _stageService.SelectAllStage();
            return collectionScenarios.Count();
        }

        public async Task<List<modScenarios.Scenery>> GetScenariosPagination(PaginationViewModel pagination)
        {
            try
            {
                var response = await _stageService.SelectPaginatorStage(pagination.Page, pagination.RecordsPage);
                var result = await MapperScenery(response);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
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
                sceneryModel.CodeConciliation = scenery.CodeConciliation;
                sceneryModel.State = scenery.State;
                sceneryModel.CodeFormat = "ESC_" + scenery.Code;
                sceneryModel.StateFormat = scenery.State ? "Activo" : "Inactivo";
                sceneryModel.CreationDate = scenery.CreationDate;
                sceneryModel.UpdateDate = scenery.UpdateDate;
                sceneryModel.CreationDateFormat = scenery.CreationDate.ToString("dd/MM/yyyy");
                sceneryModel.UpdateDateFormat = scenery.UpdateDate.ToString("dd/MM/yyyy");

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
            sceneryModel.CodeConciliation = scenery.CodeConciliation;
            sceneryModel.State = scenery.State;
            sceneryModel.CodeFormat = "ESC_" + scenery.Code;
            sceneryModel.StateFormat = scenery.State ? "Activo" : "Inactivo";
            sceneryModel.CreationDate = scenery.CreationDate;
            sceneryModel.UpdateDate = scenery.UpdateDate;
            sceneryModel.CreationDateFormat = scenery.CreationDate.ToString("dd/MM/yyyy");
            sceneryModel.UpdateDateFormat = scenery.UpdateDate.ToString("dd/MM/yyyy");

            return sceneryModel;
        }

        public async Task<SceneryCreateViewModel> GetSceneryFormatCreate(modScenarios.Scenery scenery)
        {

            SceneryCreateViewModel sceneryModel = new SceneryCreateViewModel();

            sceneryModel.Code = scenery.Code;
            sceneryModel.Name = scenery.Name;
            sceneryModel.Impact = scenery.Impact;
            sceneryModel.Conciliation = scenery.Conciliation;
            sceneryModel.CodeConciliation = scenery.CodeConciliation;
            sceneryModel.State = scenery.State;
            sceneryModel.CodeFormat = "ESC_" + scenery.Code;
            sceneryModel.CreationDate = scenery.CreationDate;
            sceneryModel.UpdateDate = scenery.UpdateDate;


            return sceneryModel;
        }


        public async Task<Scenery> GetSceneryByCode(int code)
        {
            var response = await _stageService.SelectByIdStage(new StageModel { Code = code });
            var scenery = await MapperToScenery(response);
            return scenery;
        }

        public async Task<List<SceneryViewModel>> GetFilterScenarios(FilterViewModel filterModel)
        {
            List<modScenarios.Scenery> allScenarios = await GetScenarios();
            List<SceneryViewModel> scenariosFilter = await GetScenariosFormat(allScenarios);

            if (filterModel.ColumValue == null || filterModel.ColumValue == "" || filterModel.ValueFilter == null || filterModel.ValueFilter == "")
            {
                return scenariosFilter;
            }
            else
            {
                scenariosFilter = await applyFilter(filterModel, scenariosFilter);
            }

            return scenariosFilter;
        }

        private async Task<List<SceneryViewModel>> applyFilter(FilterViewModel filterModel, List<SceneryViewModel> allScenarios)
        {
            Console.WriteLine(filterModel.TypeRow.ToString());

            var property = typeof(SceneryViewModel).GetProperty(filterModel.ColumValue);

            List<SceneryViewModel> scenariosFilter = new List<SceneryViewModel>();
            if (filterModel.TypeRow == "Select")
            {
                foreach (SceneryViewModel scenery in allScenarios)
                {
                    if (property.GetValue(scenery).ToString().ToUpper() == filterModel.ValueFilter.ToUpper())
                    {
                        scenariosFilter.Add(scenery);
                    }
                }
            }
            else
            {
                foreach (SceneryViewModel scenery in allScenarios)
                {
                    if (property.GetValue(scenery).ToString().ToUpper().Contains(filterModel.ValueFilter.ToUpper()))
                    {
                        scenariosFilter.Add(scenery);
                    }
                }
            }

            return scenariosFilter;
        }

        public List<SceneryViewModel> GetFilterPagination(List<SceneryViewModel> inicialScenarios, PaginationViewModel paginationViewModel, int totalData)
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

            List<SceneryViewModel> scenariosFilterPagination = inicialScenarios.GetRange(index, count);

            return scenariosFilterPagination;
        }

        public async Task<List<SelectListItem>> GetImpact()
        {
            List<SelectListItem> impact = new List<SelectListItem>().ToList();
            impact.Add(new SelectListItem("CLIENTE", "CLIENTE"));
            impact.Add(new SelectListItem("COMPAÑIA", "COMPAÑIA"));
            impact.Add(new SelectListItem("CONFIGURACIÓN", "CONFIGURACIÓN"));

            return impact;
        }

        public async Task<List<modConciliation.Conciliation>> GetConciliations()
        {
            List<modConciliation.Conciliation> conciliations = await conciliationsServices.GetConciliations();
            List<modConciliation.Conciliation> conciliationActives = new List<modConciliation.Conciliation>();

            foreach (var conciliation in conciliations)
            {
                if(conciliation.State == true)
                {
                    conciliationActives.Add(conciliation);
                }
            }
            return conciliationActives;
        }

        private async Task<List<Scenery>> MapperScenery(List<StageModel> stageModel)
        {
            return await Task.Run(() =>
            {
                List<Scenery> scenarios = new List<Scenery>();
                if (stageModel.Count > 0)
                {
                    foreach (var scenery in stageModel)
                    {
                        scenarios.Add(MapperToScenery(scenery).Result);
                    }
                }
                return scenarios;
            });
        }

        private async Task<Scenery> MapperToScenery(StageModel stage)
        {
            return await Task.Run(() =>
            {
                Scenery model = new Scenery
                {
                    Code = stage.Code,
                    Name = stage.Name,
                    Impact = stage.Impact,
                    Conciliation = stage.ConciliationName,
                    CodeConciliation = stage.Conciliation,
                    CreationDate = stage.CreationDate,
                    UpdateDate = stage.ModifieldDate,
                    UserOwner = stage.ModifieldBy,
                    State = stage.State,
                    StateConciliation = stage.StateConciliation,
                };
                return model;
            });
        }

        public async Task<string> InsertScenery(Scenery request)
        {
            StageModel stage = new StageModel
            {
                Name = request.Name,
                Impact = request.Impact,
                Conciliation = request.CodeConciliation,
                CreationDate = DateTime.Now,
                ModifieldDate = DateTime.Now,
                ModifieldBy = "CreateToUserDev",
                State = request.State,
            };

            var response = await _stageService.InsertStage(stage);

            return response.Equals(1) ? "Escenario creado correctamente" : "Error creando el escenario";
        }

        public async Task<string> UpdateScenery(Scenery scenery)
        {
            var mapping = await MapperUpdateScenery(scenery);
            var response = await _stageService.UpdateStage(mapping);

            return response.Equals(1) ? "Escenario actualizada correctamente" : "Error actualizando el escenario";
        }

        public async Task<string> ActiveScenery(Scenery scenery)
        {
            var mapping = await MapperActiveScenery(scenery);
            var response = await _stageService.UpdateStage(mapping);

            return response.Equals(1) ? "Escenario activo correctamente" : "Error actualizando el escenario";
        }

        public async Task<string> DesactiveScenery(Scenery scenery)
        {
            var mapping = await MapperDesactiveScenery(scenery);
            var response = await _stageService.UpdateStage(mapping);

            return response.Equals(1) ? "Escenario desactivo correctamente" : "Error actualizando el escenario";
        }

        private async Task<StageModel> MapperUpdateScenery(Scenery scenery)
        {
            return await Task.Run(() =>
            {
                StageModel model = new StageModel
                {
                    Code = scenery.Code,
                    Name = scenery.Name,
                    Impact = scenery.Impact,
                    Conciliation = scenery.CodeConciliation,
                    CreationDate = scenery.CreationDate,
                    ModifieldDate = DateTime.Now,
                    ModifieldBy = "CreateToUserDev",
                    State = scenery.State,
                };
                return model;
            });
        }

        private async Task<StageModel> MapperActiveScenery(Scenery scenery)
        {
            return await Task.Run(() =>
            {
                StageModel model = new StageModel
                {
                    Code = scenery.Code,
                    Name = scenery.Name,
                    Impact = scenery.Impact,
                    Conciliation = scenery.CodeConciliation,
                    CreationDate = scenery.CreationDate,
                    ModifieldDate = DateTime.Now,
                    ModifieldBy = "CreateToUserDev",
                    State = true,
                };
                return model;
            });
        }

        private async Task<StageModel> MapperDesactiveScenery(Scenery scenery)
        {
            return await Task.Run(() =>
            {
                StageModel model = new StageModel
                {
                    Code = scenery.Code,
                    Name = scenery.Name,
                    Impact = scenery.Impact,
                    Conciliation = scenery.CodeConciliation,
                    CreationDate = scenery.CreationDate,
                    ModifieldDate = DateTime.Now,
                    ModifieldBy = "CreateToUserDev",
                    State = false,
                };
                return model;
            });
        }
    }
}
