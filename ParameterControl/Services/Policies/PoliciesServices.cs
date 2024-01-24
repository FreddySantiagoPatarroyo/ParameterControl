using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Models.Policy;
using ParameterControl.Models.Rows;
using ParameterControl.Policy.Entities;
using ParameterControl.Policy.Impl;
using ParameterControl.Policy.Interfaces;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using ent = ParameterControl.Policy.Entities;
using modPolicy = ParameterControl.Models.Policy;

namespace ParameterControl.Services.Policies
{
    public class PoliciesServices: IPoliciesServices
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
                    Id = "1",
                    Code = "COD_001",
                    Name = "Politica_1",
                    Description = "Descripcion ejemplo",
                    Conciliation = 123,
                    ControlType = "Voz",
                    OperationType = "OperationType_1asdasdasdasdasdadasdasdasdad",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new modPolicy.Policy(){
                    Id = "2",
                    Code = "COD_006",
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
                    Id = "3",
                    Code = "COD_001",
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
                    Id = "4",
                    Code = "COD_006",
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
                    Id = "5",
                    Code = "COD_001",
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
                    Id = "6",
                    Code = "COD_006",
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
                    Id = "7",
                    Code = "COD_001",
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
                    Id = "8",
                    Code = "",
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
            List<PolicyViewModel> policiesModel  = new List<PolicyViewModel>();

            foreach (modPolicy.Policy policy in policies)
            {
                PolicyViewModel policyModel = new PolicyViewModel();

                policyModel.Id = policy.Id;
                policyModel.Code = policy.Code;
                policyModel.Name = policy.Name; 
                policyModel.Description = policy.Description;
                policyModel.Conciliation = policy.Conciliation;
                policyModel.ControlType = policy.ControlType;
                policyModel.OperationType = policy.OperationType;
                policyModel.State = policy.State;
                policyModel.StateFormat = policy.State ? "Activo" : "Inactivo";

                policiesModel.Add(policyModel);
            }

            return policiesModel;
        }

        public async Task<modPolicy.Policy> GetPolicyById(string id)
        {
            modPolicy.Policy policy = policies.Find(policy => policy.Id == id);
            return policy;
        }

        public async Task<string> InsertPolicy(modPolicy.Policy request)
        {
            try
            {
                var policy = new ent.PolicyModel
                {
                    Code = request.Code,
                    Name = request.Name,
                    Description = request.Description,
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

            if((filterModel.ColumValue == null || filterModel.ColumValue == "" || filterModel.ValueFilter == null || filterModel.ValueFilter == ""))
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
