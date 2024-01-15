using Newtonsoft.Json.Linq;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Policy;
using ParameterControl.Models.Rows;
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

        public PoliciesServices(IConfiguration configuration)
        {
            _policyService = new PolicyService(configuration);
            policies = new List<modPolicy.Policy>()
            {
                new modPolicy.Policy(){
                    Id = "1",
                    Code = "COD_001",
                    Name = "Politica_1",
                    Description = "Descripcion ejemplo",
                    Conciliation = 123,
                    ControlType = "Emial",
                    OperationType = "OperationType_1asdasdasdasdasdadasdasdasdad",
                    State = true
                },
                new modPolicy.Policy(){
                    Id = "2",
                    Code = "COD_006",
                    Name = "Name",
                    Description = "Description",
                    Conciliation = 123,
                    ControlType = "Voz",
                    OperationType = "OperationType",
                    State = false
                },
                new modPolicy.Policy(){
                    Id = "3",
                    Code = "COD_001",
                    Name = "Politica_1",
                    Description = "Descripcion ejemplo",
                    Conciliation = 123,
                    ControlType = "Emial",
                    OperationType = "OperationType_1asdasdasdasdasdadasdasdasdad",
                    State = true
                },
                new modPolicy.Policy(){
                    Id = "4",
                    Code = "COD_006",
                    Name = "Name",
                    Description = "Description",
                    Conciliation = 123,
                    ControlType = "Voz",
                    OperationType = "OperationType",
                    State = false
                },
                new modPolicy.Policy(){
                    Id = "5",
                    Code = "COD_001",
                    Name = "Politica_1",
                    Description = "Descripcion ejemplo",
                    Conciliation = 123,
                    ControlType = "Emial",
                    OperationType = "OperationType_1asdasdasdasdasdadasdasdasdad",
                    State = true
                },
                new modPolicy.Policy(){
                    Id = "6",
                    Code = "COD_006",
                    Name = "Name",
                    Description = "Description",
                    Conciliation = 123,
                    ControlType = "Voz",
                    OperationType = "OperationType",
                    State = false
                },
                new modPolicy.Policy(){
                    Id = "7",
                    Code = "COD_001",
                    Name = "Politica_1",
                    Description = "Descripcion ejemplo",
                    Conciliation = 123,
                    ControlType = "Emial",
                    OperationType = "OperationType_1asdasdasdasdasdadasdasdasdad",
                    State = true
                },
                new modPolicy.Policy(){
                    Id = "8",
                    Code = "",
                    Name = "Name",
                    Description = "Description",
                    Conciliation = 123,
                    ControlType = "Voz",
                    OperationType = "OperationType",
                    State = false
                }
            };
        }

        public async Task<List<modPolicy.Policy>> GetPolicies()
        {
            return policies;
        }

        public List<PolicyViewModel> GetPolicesFormatTable(List<modPolicy.Policy> policies)
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

        public async Task<List<string>> GetOperationsType()
        {
           List<string> operationsType = new List<string>()
           {
               "Movil",
               "Fija"
           };
            return operationsType;
        }

        public async Task<List<PolicyViewModel>> GetFilterPolicies(FilterViewModel filterModel)
        {
            List<PolicyViewModel> policiesFilter = new List<PolicyViewModel>();

            if((filterModel.ColumValue == null || filterModel.ColumValue == "" || filterModel.ValueFilter == null || filterModel.ValueFilter == ""))
            {
                policiesFilter = GetPolicesFormatTable(policies);
            }
            else
            {
                switch (filterModel.ColumValue)
                {
                    case "Code":
                        policiesFilter = applyFilter(filterModel);
                        break;
                    case "Name":
                        policiesFilter = applyFilter(filterModel);
                        break;
                    case "Description":
                        policiesFilter = applyFilter(filterModel);
                        break;
                    case "Conciliation":
                        policiesFilter = applyFilter(filterModel);
                        break;
                    case "ControlType":
                        policiesFilter = applyFilter(filterModel);
                        break;
                    case "OperationType":
                        policiesFilter = applyFilter(filterModel);
                        break;
                    default:
                        break;
                }
            }

            return policiesFilter;
        }

        private List<PolicyViewModel> applyFilter(FilterViewModel filterModel)
        {
            var property = typeof(PolicyViewModel).GetProperty(filterModel.ColumValue);

            List<PolicyViewModel> policiesFilter = new List<PolicyViewModel>();

            List<PolicyViewModel> Policies = GetPolicesFormatTable(policies);

            foreach (PolicyViewModel policy in Policies)
            {
                if (property.GetValue(policy).ToString().ToUpper().Contains(filterModel.ValueFilter.ToUpper()))
                {
                    policiesFilter.Add(policy);
                }
            }

            return policiesFilter;
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
    }
}
