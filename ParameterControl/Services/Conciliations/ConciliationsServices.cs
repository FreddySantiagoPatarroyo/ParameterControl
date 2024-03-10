using Microsoft.AspNetCore.Mvc.Rendering;
using ParameterControl.Conciliation.Entities;
using ParameterControl.Conciliation.Impl;
using ParameterControl.Conciliation.Interfaces;
using ParameterControl.Models.Conciliation;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Services.Policies;
using ParameterControl.Stage.Impl;
using ParameterControl.Stage.Interfaces;
using ParameterControl.User.Impl;
using ParameterControl.User.Interfaces;
using modConciliation = ParameterControl.Models.Conciliation;
using modPolicy = ParameterControl.Models.Policy;

namespace ParameterControl.Services.Conciliations
{
    public class ConciliationsServices : IConciliationsServices
    {
        private List<modConciliation.Conciliation> conciliations = new List<modConciliation.Conciliation>();
        private readonly IPoliciesServices policiesServices;
        private readonly IConciliationService conciliationServices;
        private readonly IStageService _stageService;
        private readonly IUserService _userService;

        public ConciliationsServices(
            IPoliciesServices policiesServices,
            IConfiguration configuration
        )
        {
            _stageService = new StageService(configuration);
            _userService = new UserService(configuration);
            conciliationServices = new ConciliationService(configuration);
            conciliations = new List<modConciliation.Conciliation>()
            {
            new modConciliation.Conciliation(){
                    Code = 1,
                    Name = "Conciliacion_1",
                    Description = "Descriptionasdasdasdasdasdasdasdasdasdasd",
                    Email = "ejemplo@gmail.com",
                    Destination = "Destination",
                    Policy = "Politica1",
                    Required = "Si",
                    ControlType = "Voz",
                    OperationType = "Model",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                },
                new modConciliation.Conciliation(){
                    Code = 2,
                    Name = "Conciliacion_1",
                    Description = "Description",
                    Email = "ejemplo@gmail.com",
                    Destination = "Destination",
                    Policy = "Politica2",
                    Required = "Si",
                    ControlType = "Voz",
                    OperationType = "Model",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                }
            };
            this.policiesServices = policiesServices;
        }

        public async Task<List<modConciliation.Conciliation>> GetConciliations()
        {
            var collectionConciliations = await conciliationServices.SelectAllConciliation();
            var response = await MapperConciliation(collectionConciliations);
            return response;
        }

        public async Task<int> CountConciliations()
        {
            var collectionConciliations = await conciliationServices.SelectAllConciliation();
            return collectionConciliations.Count();
        }

        public async Task<List<modConciliation.Conciliation>> GetConciliationsPagination(PaginationViewModel pagination)
        {
            try
            {
                var response = await conciliationServices.SelectPaginatorConciliation(pagination.Page, pagination.RecordsPage);
                var result = await MapperConciliation(response);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ConciliationViewModel>> GetConciliationsFormat(List<modConciliation.Conciliation> conciliations)
        {
            List<ConciliationViewModel> conciliationsModel = new List<ConciliationViewModel>();

            foreach (modConciliation.Conciliation conciliation in conciliations)
            {
                ConciliationViewModel conciliationModel = new ConciliationViewModel();
                conciliationModel.Code = conciliation.Code;
                conciliationModel.Name = conciliation.Name;
                conciliationModel.Description = conciliation.Description;
                conciliationModel.Email = conciliation.Email;
                conciliationModel.Destination = conciliation.Destination;
                conciliationModel.Policy = conciliation.Policy;
                conciliationModel.PolicyCode = conciliation.PolicyCode;
                conciliationModel.Required = conciliation.Required;
                conciliationModel.ControlType = conciliation.ControlType;
                conciliationModel.OperationType = conciliation.OperationType;
                conciliationModel.RequiredFormat = conciliation.Required;
                conciliationModel.State = conciliation.State;
                conciliationModel.StatePolicy = conciliation.StatePolicy;
                conciliationModel.CodeFormat = "CO_" + conciliation.Code;
                conciliationModel.StateFormat = conciliation.State ? "Activo" : "Inactivo";
                conciliationModel.CreationDate = conciliation.CreationDate;
                conciliationModel.UpdateDate = conciliation.UpdateDate;
                conciliationModel.CreationDateFormat = conciliation.CreationDate.ToString("dd/MM/yyyy");
                conciliationModel.UpdateDateFormat = conciliation.UpdateDate.ToString("dd/MM/yyyy");

                conciliationsModel.Add(conciliationModel);
            }

            return conciliationsModel;
        }

        public async Task<ConciliationViewModel> GetConciliationFormat(modConciliation.Conciliation conciliation)
        {
            ConciliationViewModel conciliationModel = new ConciliationViewModel();
            conciliationModel.Code = conciliation.Code;
            conciliationModel.Name = conciliation.Name;
            conciliationModel.Description = conciliation.Description;
            conciliationModel.Email = conciliation.Email;
            conciliationModel.Destination = conciliation.Destination;
            conciliationModel.Policy = conciliation.Policy;
            conciliationModel.PolicyCode = conciliation.PolicyCode;
            conciliationModel.Required = conciliation.Required;
            conciliationModel.ControlType = conciliation.ControlType;
            conciliationModel.OperationType = conciliation.OperationType;
            conciliationModel.RequiredFormat = conciliation.Required;
            conciliationModel.State = conciliation.State;
            conciliationModel.StatePolicy = conciliation.StatePolicy;
            conciliationModel.CodeFormat = "CO_" + conciliation.Code;
            conciliationModel.StateFormat = conciliation.State ? "Activo" : "Inactivo";
            conciliationModel.CreationDate = conciliation.CreationDate;
            conciliationModel.UpdateDate = conciliation.UpdateDate;
            conciliationModel.CreationDateFormat = conciliation.CreationDate.ToString("dd/MM/yyyy");
            conciliationModel.UpdateDateFormat = conciliation.UpdateDate.ToString("dd/MM/yyyy");

            return conciliationModel;
        }

        public async Task<ConciliationCreateViewModel> GetConciliationFormatCreate(modConciliation.Conciliation conciliation)
        {
            ConciliationCreateViewModel conciliationModel = new ConciliationCreateViewModel();
            conciliationModel.Code = conciliation.Code;
            conciliationModel.Name = conciliation.Name;
            conciliationModel.Description = conciliation.Description;
            conciliationModel.Email = conciliation.Email;
            conciliationModel.Destination = conciliation.Destination;
            conciliationModel.Policy = conciliation.Policy;
            conciliationModel.PolicyCode = conciliation.PolicyCode;
            conciliationModel.Required = conciliation.Required;
            conciliationModel.ControlType = conciliation.ControlType;
            conciliationModel.OperationType = conciliation.OperationType;
            conciliationModel.RequiredFormat = conciliation.Required;
            conciliationModel.State = conciliation.State;
            conciliationModel.StatePolicy = conciliation.StatePolicy;
            conciliationModel.CodeFormat = "CO_" + conciliation.Code;
            conciliationModel.CreationDate = conciliation.CreationDate;
            conciliationModel.UpdateDate = conciliation.UpdateDate;

            return conciliationModel;
        }

        public async Task<modConciliation.Conciliation> GetConciliationsByCode(int code)
        {
            var response = await conciliationServices.SelectByIdConciliation(new ConciliationModel { Code = code });
            var conciliation = await MapperToConciliation(response);
            return conciliation;
        }

        public async Task<List<SelectListItem>> GetOperationsType()
        {
            List<SelectListItem> operationsType = new List<SelectListItem>().ToList();
            operationsType.Add(new SelectListItem("Model", "Model"));
            operationsType.Add(new SelectListItem("Fija", "Fija"));
            return operationsType;
        }

        public async Task<List<modPolicy.Policy>> GetPolicies()
        {
            List<modPolicy.Policy> policies = await policiesServices.GetPolicies();
            List<modPolicy.Policy> policiesActives = new List<modPolicy.Policy>();

            foreach (var policy in policies)
            {
                if (policy.State == true)
                {
                    policiesActives.Add(policy);
                }
            }

            return policiesActives;
        }

        public async Task<List<string>> GetDestinations()
        {
            List<modConciliation.Conciliation> Conciliations = await GetConciliations();
            List<string> Destinations = Conciliations.Select(conciliation => conciliation.Destination).ToList();
            List<string> DestinationsActive = new List<string>();

            foreach (var detination in Destinations)
            {
                if (detination != null && detination != string.Empty)
                {
                    if (!DestinationsActive.Contains(detination))
                    {
                        DestinationsActive.Add(detination);
                    }
                }
            }
            return DestinationsActive;
        }

        public async Task<List<SelectListItem>> GetRequired()
        {
            List<SelectListItem> required = new List<SelectListItem>().ToList();
            required.Add(new SelectListItem("Si", "SI"));
            required.Add(new SelectListItem("No", "NO"));
            return required;
        }

        public async Task<List<ConciliationViewModel>> GetFilterConciliations(FilterViewModel filterModel)
        {
            List<modConciliation.Conciliation> allConciliations = await GetConciliations();
            List<ConciliationViewModel> conciliationsFilter = await GetConciliationsFormat(allConciliations);

            if (filterModel.ColumValue == null || filterModel.ColumValue == "" || filterModel.ValueFilter == null || filterModel.ValueFilter == "")
            {
                return conciliationsFilter;
            }
            else
            {
                conciliationsFilter = await applyFilter(filterModel, conciliationsFilter);
            }

            return conciliationsFilter;
        }

        private async Task<List<ConciliationViewModel>> applyFilter(FilterViewModel filterModel, List<ConciliationViewModel> allConciliations)
        {
            var property = typeof(ConciliationViewModel).GetProperty(filterModel.ColumValue);

            List<ConciliationViewModel> conciliationsFilter = new List<ConciliationViewModel>();
            if (filterModel.TypeRow == "Select")
            {
                foreach (ConciliationViewModel conciliation in allConciliations)
                {
                    if (property.GetValue(conciliation).ToString().ToUpper() == filterModel.ValueFilter.ToUpper())
                    {
                        conciliationsFilter.Add(conciliation);
                    }
                }
            }
            else
            {
                foreach (ConciliationViewModel conciliation in allConciliations)
                {
                    if (property.GetValue(conciliation).ToString().ToUpper().Contains(filterModel.ValueFilter.ToUpper()))
                    {
                        conciliationsFilter.Add(conciliation);
                    }
                }
            }

            return conciliationsFilter;
        }

        public List<ConciliationViewModel> GetFilterPagination(List<ConciliationViewModel> inicialConciliations, PaginationViewModel paginationViewModel, int totalData)
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

            List<ConciliationViewModel> conciliationsFilterPagination = inicialConciliations.GetRange(index, count);

            return conciliationsFilterPagination;
        }

        private async Task<List<modConciliation.Conciliation>> MapperConciliation(List<ConciliationModel> conciliationModel)
        {
            return await Task.Run(() =>
            {
                List<modConciliation.Conciliation> conciliations = new List<modConciliation.Conciliation>();
                if (conciliationModel.Count > 0)
                {
                    foreach (var conciliation in conciliationModel)
                    {
                        conciliations.Add(MapperToConciliation(conciliation).Result);
                    }
                }
                return conciliations;
            });
        }

        private async Task<modConciliation.Conciliation> MapperToConciliation(ConciliationModel conciliation)
        {
            return await Task.Run(() =>
            {
                modConciliation.Conciliation model = new modConciliation.Conciliation
                {
                    Code = conciliation.Code,
                    Name = conciliation.ConciliationName,
                    Description = conciliation.Description,
                    Email = conciliation.Email,
                    Destination = conciliation.TargetTable,
                    Policy = conciliation.PolicyName,
                    PolicyCode = conciliation.PolicyId,
                    Required = conciliation.RequiredApproval,
                    ControlType = conciliation.AssignmentType,
                    OperationType = conciliation.OperationType,
                    CreationDate = conciliation.CreationDate,
                    UpdateDate = conciliation.ModifieldDate,
                    State = conciliation.State,
                    StatePolicy = conciliation.StatePolicy
                };
                return model;
            });
        }

        public async Task<bool> ValidateScenariosActivos(int codeConciliacion)
        {
            var Scenarios = await _stageService.SelectAllStage();

            foreach (var Scenary in Scenarios)
            {
                if (Scenary.Conciliation == codeConciliacion && Scenary.State == true)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<List<SelectListItem>> GetEmailUsers()
        {
            var collectionUsers = await _userService.SelectAllUser();
            var Emails = collectionUsers.Select(user => new SelectListItem(user.Email, user.Email)).ToList();

            return Emails;
        }


        public async Task<string> InsertConciliation(modConciliation.Conciliation request)
        {
            ConciliationModel conciliation = new ConciliationModel
            {
                ConciliationName = request.Name,
                Email = request.Email,
                TargetTable = request.Destination,
                AssignmentType = request.ControlType,
                OperationType = request.OperationType,
                PolicyId = request.PolicyCode,
                RequiredApproval = request.Required,
                CreationDate = DateTime.Now,
                ModifieldDate = DateTime.Now,
                State = request.State,
            };

            var response = await conciliationServices.InsertConciliation(conciliation);

            return response.Equals(1) ? "Conciliacion creada correctamente" : "Error creando la conciliacion";
        }

        public async Task<string> UpdateConciliation(modConciliation.Conciliation conciliation)
        {
            var mapping = await MapperUpdateConciliation(conciliation);
            var response = await conciliationServices.UpdateConciliation(mapping);

            return response.Equals(1) ? "Conciliacion actualizada correctamente" : "Error actualizando la conciliacion";
        }

        public async Task<string> ActiveConciliation(modConciliation.Conciliation conciliation)
        {
            var mapping = await MapperActiveConciliation(conciliation);
            var response = await conciliationServices.UpdateConciliation(mapping);

            return response.Equals(1) ? "Conciliacion activada correctamente" : "Error activando la conciliacion";
        }

        public async Task<string> DesactiveConciliation(modConciliation.Conciliation conciliation)
        {
            var mapping = await MapperDesctiveConciliation(conciliation);
            var response = await conciliationServices.UpdateConciliation(mapping);

            return response.Equals(1) ? "Conciliacion desactivada correctamente" : "Error desactivando la conciliacion";
        }

        private async Task<ConciliationModel> MapperUpdateConciliation(modConciliation.Conciliation conciliation)
        {
            return await Task.Run(() =>
            {
                ConciliationModel model = new ConciliationModel
                {
                    Code = conciliation.Code,
                    ConciliationName = conciliation.Name,
                    Email = conciliation.Email,
                    TargetTable = conciliation.Destination,
                    AssignmentType = conciliation.ControlType,
                    OperationType = conciliation.OperationType,
                    PolicyId = conciliation.PolicyCode,
                    RequiredApproval = conciliation.Required,
                    CreationDate = conciliation.CreationDate,
                    ModifieldDate = DateTime.Now,
                    State = conciliation.State,
                };
                return model;
            });
        }

        private async Task<ConciliationModel> MapperActiveConciliation(modConciliation.Conciliation conciliation)
        {
            return await Task.Run(() =>
            {
                ConciliationModel model = new ConciliationModel
                {
                    Code = conciliation.Code,
                    ConciliationName = conciliation.Name,
                    Email = conciliation.Email,
                    TargetTable = conciliation.Destination,
                    AssignmentType = conciliation.ControlType,
                    OperationType = conciliation.OperationType,
                    PolicyId = conciliation.PolicyCode,
                    RequiredApproval = conciliation.Required,
                    CreationDate = conciliation.CreationDate,
                    ModifieldDate = DateTime.Now,
                    State = true,
                };
                return model;
            });
        }

        private async Task<ConciliationModel> MapperDesctiveConciliation(modConciliation.Conciliation conciliation)
        {
            return await Task.Run(() =>
            {
                ConciliationModel model = new ConciliationModel
                {
                    Code = conciliation.Code,
                    ConciliationName = conciliation.Name,
                    Email = conciliation.Email,
                    TargetTable = conciliation.Destination,
                    AssignmentType = conciliation.ControlType,
                    OperationType = conciliation.OperationType,
                    PolicyId = conciliation.PolicyCode,
                    RequiredApproval = conciliation.Required,
                    CreationDate = conciliation.CreationDate,
                    ModifieldDate = DateTime.Now,
                    State = false,
                };
                return model;
            });
        }
    }
}