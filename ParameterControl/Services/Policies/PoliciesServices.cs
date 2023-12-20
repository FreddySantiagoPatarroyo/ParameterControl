using Newtonsoft.Json.Linq;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Policy;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

namespace ParameterControl.Services.Policies
{
    public class PoliciesServices: IPoliciesServices
    {
        private List<Policy> policies = new List<Policy>();
        public PoliciesServices()
        {
            policies = new List<Policy>()
            {
                new Policy(){
                    Id = "1",
                    Code = "COD_001",
                    Name = "Politica_1",
                    Description = "Descripcion ejemplo",
                    Conciliation = 123,
                    ControlType = "Emial",
                    OperationType = "OperationType_1asdasdasdasdasdadasdasdasdad",
                    State = true
                },
                new Policy(){
                    Id = "2",
                    Code = "COD_006",
                    Name = "Name",
                    Description = "Description",
                    Conciliation = 123,
                    ControlType = "Voz",
                    OperationType = "OperationType",
                    State = false
                },
                new Policy(){
                    Id = "3",
                    Code = "COD_001",
                    Name = "Politica_1",
                    Description = "Descripcion ejemplo",
                    Conciliation = 123,
                    ControlType = "Emial",
                    OperationType = "OperationType_1asdasdasdasdasdadasdasdasdad",
                    State = true
                },
                new Policy(){
                    Id = "4",
                    Code = "COD_006",
                    Name = "Name",
                    Description = "Description",
                    Conciliation = 123,
                    ControlType = "Voz",
                    OperationType = "OperationType",
                    State = false
                },
                new Policy(){
                    Id = "5",
                    Code = "COD_001",
                    Name = "Politica_1",
                    Description = "Descripcion ejemplo",
                    Conciliation = 123,
                    ControlType = "Emial",
                    OperationType = "OperationType_1asdasdasdasdasdadasdasdasdad",
                    State = true
                },
                new Policy(){
                    Id = "6",
                    Code = "COD_006",
                    Name = "Name",
                    Description = "Description",
                    Conciliation = 123,
                    ControlType = "Voz",
                    OperationType = "OperationType",
                    State = false
                },
                new Policy(){
                    Id = "7",
                    Code = "COD_001",
                    Name = "Politica_1",
                    Description = "Descripcion ejemplo",
                    Conciliation = 123,
                    ControlType = "Emial",
                    OperationType = "OperationType_1asdasdasdasdasdadasdasdasdad",
                    State = true
                },
                new Policy(){
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

        public async Task<List<Policy>> GetPolicies()
        {
            return policies;
        }

        public async Task<Policy> GetPolicyById(string id)
        {
            Policy policy = policies.Find(policy => policy.Id == id);
            return policy;
        }

        public async Task<List<Policy>> GetFilterPolicies(FilterViewModel filterModel)
        {
            List<Policy> policiesFilter = new List<Policy>();

            if((filterModel.Filter.Value == null || filterModel.Filter.Value == "" || filterModel.ValueFilter == null || filterModel.ValueFilter == ""))
            {
                policiesFilter = policies;
            }
            else
            {
                switch (filterModel.Filter.Value)
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

        public List<Policy> applyFilter(FilterViewModel filterModel)
        {
            var property = typeof(Policy).GetProperty(filterModel.Filter.Value);

            List<Policy> policiesFilter = new List<Policy>();

            foreach (Policy policy in policies)
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
