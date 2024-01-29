using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Models.Policy;
using ParameterControl.Policy.Entities;
using ParameterControl.Policy.Impl;
using modPolicy = ParameterControl.Models.Policy;

namespace ParameterControl.Services.Policies
{
    public class PoliciesServices : IPoliciesServices
    {
        private List<modPolicy.Policy> policies = new List<modPolicy.Policy>();
        private readonly PolicyService _policyService;
        private readonly IMapper _mapper;

        public PoliciesServices(IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _policyService = new PolicyService(configuration);
            policies = new List<modPolicy.Policy>()
            {
                new modPolicy.Policy(){
                    Code = 1,
                    Name = "Politica_1",
                    Description = "Descripcion ejemplo asdasdasdasdasdasdasdasdasdadasdasdasdasdads",
                    Conciliation = 123,
                    ControlType = "Voz",
                    OperationType = "OperationType_1asdasdasdasdasdadasdasdasdad",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new modPolicy.Policy(){
                    Code = 2,
                    Name = "Name",
                    Description = "Description",
                    Conciliation = 123,
                    ControlType = "Voz",
                    OperationType = "OperationType",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new modPolicy.Policy(){
                    Code = 3,
                    Name = "Politica_1",
                    Description = "Descripcion ejemplo",
                    Conciliation = 123,
                    ControlType = "Emial",
                    OperationType = "OperationType_1asdasdasdasdasdadasdasdasdad",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new modPolicy.Policy(){
                    Code = 4,
                    Name = "Name",
                    Description = "Description",
                    Conciliation = 123,
                    ControlType = "Voz",
                    OperationType = "OperationType",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new modPolicy.Policy(){
                    Code = 5,
                    Name = "Politica_1",
                    Description = "Descripcion ejemplo",
                    Conciliation = 123,
                    ControlType = "Emial",
                    OperationType = "OperationType_1asdasdasdasdasdadasdasdasdad",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new modPolicy.Policy(){
                    Code = 6,
                    Name = "Name",
                    Description = "Description",
                    Conciliation = 123,
                    ControlType = "Voz",
                    OperationType = "OperationType",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new modPolicy.Policy(){
                    Code = 7,
                    Name = "Politica_1",
                    Description = "Descripcion ejemplo",
                    Conciliation = 123,
                    ControlType = "Emial",
                    OperationType = "OperationType_1asdasdasdasdasdadasdasdasdad",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new modPolicy.Policy(){
                    Code = 8,
                    Name = "Name",
                    Description = "Description",
                    Conciliation = 123,
                    ControlType = "Voz",
                    OperationType = "OperationType",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                }
            };
        }

        public async Task<List<modPolicy.Policy>> GetPolicies()
        {
            return policies;
        }

        public async Task<List<modPolicy.Policy>> GetPoliciesPagination(PaginationViewModel pagination)
        {
            try
            {
                var response = await _policyService.SelectPaginatorPolicy(pagination.Page, pagination.RecordsPage);
                var result = _mapper.Map<List<modPolicy.Policy>>(response);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<PolicyViewModel>> GetPolicesFormat(List<modPolicy.Policy> policies)
        {
            List<PolicyViewModel> policiesModel = new List<PolicyViewModel>();

            foreach (modPolicy.Policy policy in policies)
            {
                PolicyViewModel policyModel = new PolicyViewModel();

                policyModel.Code = policy.Code;
                policyModel.Name = policy.Name;
                policyModel.Description = policy.Description;
                policyModel.Conciliation = policy.Conciliation;
                policyModel.ControlType = policy.ControlType;
                policyModel.OperationType = policy.OperationType;
                policyModel.State = policy.State;
                policyModel.CodeFormat = "PO_" + policy.Code;
                policyModel.StateFormat = policy.State ? "Activo" : "Inactivo";
                policyModel.CreationDate = policy.CreationDate;
                policyModel.UpdateDate = policy.UpdateDate;

                policiesModel.Add(policyModel);
            }

            return policiesModel;
        }

        public async Task<PolicyViewModel> GetPolicyFormat(modPolicy.Policy policy)
        {

            PolicyViewModel policyModel = new PolicyViewModel();

            policyModel.Code = policy.Code;
            policyModel.Name = policy.Name;
            policyModel.Description = policy.Description;
            policyModel.Conciliation = policy.Conciliation;
            policyModel.ControlType = policy.ControlType;
            policyModel.OperationType = policy.OperationType;
            policyModel.State = policy.State;
            policyModel.CodeFormat = "PO_" + policy.Code;
            policyModel.StateFormat = policy.State ? "Activo" : "Inactivo";
            policyModel.CreationDate = policy.CreationDate;
            policyModel.UpdateDate = policy.UpdateDate;

            return policyModel;
        }

        public async Task<PolicyCreateViewModel> GetPolicyFormatCreate(modPolicy.Policy policy)
        {

            PolicyCreateViewModel policyModel = new PolicyCreateViewModel();

            policyModel.Code = policy.Code;
            policyModel.Name = policy.Name;
            policyModel.Description = policy.Description;
            policyModel.Conciliation = policy.Conciliation;
            policyModel.ControlType = policy.ControlType;
            policyModel.OperationType = policy.OperationType;
            policyModel.State = policy.State;
            policyModel.CodeFormat = "PO_" + policy.Code;
            policyModel.CreationDate = policy.CreationDate;
            policyModel.UpdateDate = policy.UpdateDate;

            return policyModel;
        }

        public async Task<modPolicy.Policy> GetPolicyByCode(int code)
        {
            modPolicy.Policy policy = policies.Find(policy => policy.Code == code);
            return policy;
        }

        public async Task<string> InsertPolicy(modPolicy.Policy request)
        {
            try
            {
                var policy = new PolicyModel
                {
                    Name = request.Name,
                    Description = request.Description,
                    Objetive = request.Objetive,
                    CreationDate = DateTime.Now,
                    ModifieldDate = DateTime.Now,
                    ModifieldBy = "CreateToUserDev"
                };

                var response = await _policyService.InsertPolicy(policy);

                return response.Equals(1) ? "Politica creada correctamente" : "Error creando la politica";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<SelectListItem>> GetOperationsType()
        {
            List<SelectListItem> operationsType = new List<SelectListItem>().ToList();
            operationsType.Add(new SelectListItem("Model", "1"));
            operationsType.Add(new SelectListItem("Fija", "2"));
            return operationsType;
        }

        public async Task<List<PolicyViewModel>> GetFilterPolicies(FilterViewModel filterModel)
        {
            List<modPolicy.Policy> policiesFilter = new List<modPolicy.Policy>();

            if ((filterModel.ColumValue == null || filterModel.ColumValue == "" || filterModel.ValueFilter == null || filterModel.ValueFilter == ""))
            {
                policiesFilter = await GetPolicies();
            }
            else
            {
                switch (filterModel.ColumValue)
                {
                    case "Code":
                        policiesFilter = await applyFilter(filterModel, policiesFilter);
                        break;
                    case "Name":
                        policiesFilter = await applyFilter(filterModel, policiesFilter);
                        break;
                    case "Description":
                        policiesFilter = await applyFilter(filterModel, policiesFilter);
                        break;
                    case "Conciliation":
                        policiesFilter = await applyFilter(filterModel, policiesFilter);
                        break;
                    case "ControlType":
                        policiesFilter = await applyFilter(filterModel, policiesFilter);
                        break;
                    case "OperationType":
                        policiesFilter = await applyFilter(filterModel, policiesFilter);
                        break;
                    case "StateFormat":
                        policiesFilter = await applyFilter(filterModel, policiesFilter);
                        break;
                    default:
                        break;
                }
            }

            return await GetPolicesFormat(policiesFilter);
        }

        private async Task<List<modPolicy.Policy>> applyFilter(FilterViewModel filterModel, List<modPolicy.Policy> listPolicies)
        {
            var property = typeof(modPolicy.Policy).GetProperty(filterModel.ColumValue);

            List<modPolicy.Policy> policiesFilter = new List<modPolicy.Policy>();

            List<modPolicy.Policy> Policies = await GetPolicies();

            foreach (modPolicy.Policy policy in Policies)
            {
                if (property.GetValue(policy).ToString().ToUpper().Contains(filterModel.ValueFilter.ToUpper()))
                {
                    policiesFilter.Add(policy);
                }
            }
            return policiesFilter;
        }
    }
}
