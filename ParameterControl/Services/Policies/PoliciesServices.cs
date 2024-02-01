using ParameterControl.Models.Filter;
using ParameterControl.Models.Pagination;
using ParameterControl.Models.Policy;
using ParameterControl.Policy.Entities;
using ParameterControl.Policy.Impl;
using ParameterControl.Policy.Interfaces;
using System.Data;
using System.Runtime.CompilerServices;
using modPolicy = ParameterControl.Models.Policy;

namespace ParameterControl.Services.Policies
{
    public class PoliciesServices : IPoliciesServices
    {
        private List<modPolicy.Policy> policies = new List<modPolicy.Policy>();
        private readonly IPolicyService _policyService;

        public PoliciesServices(IConfiguration configuration)
        {
            _policyService = new PolicyService(configuration);
            policies = new List<modPolicy.Policy>()
            {
                new modPolicy.Policy(){
                    Code = 1,
                    Name = "Politica_1",
                    Description = "Descripcion ejemplo asdasdasdasdasdasdasdasdasdadasdasdasdasdads",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new modPolicy.Policy(){
                    Code = 2,
                    Name = "Name",
                    Description = "Description",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new modPolicy.Policy(){
                    Code = 3,
                    Name = "Politica_1",
                    Description = "Descripcion ejemplo",
                    State = true,
                    CreationDate = DateTime.Parse("2024-02-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new modPolicy.Policy(){
                    Code = 4,
                    Name = "Name",
                    Description = "Description",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new modPolicy.Policy(){
                    Code = 5,
                    Name = "Politica_1",
                    Description = "Descripcion ejemplo",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new modPolicy.Policy(){
                    Code = 6,
                    Name = "Name",
                    Description = "Description",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new modPolicy.Policy(){
                    Code = 7,
                    Name = "Politica_1",
                    Description = "Descripcion ejemplo",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new modPolicy.Policy(){
                    Code = 8,
                    Name = "Name",
                    Description = "Description",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                }
            };
        }

        public async Task<List<modPolicy.Policy>> GetPoliciesFake()
        {
            return policies;
        }

        public async Task<List<modPolicy.Policy>> GetPolicies()
        {
            var collectionPolicies = await _policyService.SelectAllPolicy();
            var response = await MapperPolicy(collectionPolicies);
            return response;
        }

        public async Task<int> CountPolicies()
        {
            var collectionPolicies = await _policyService.SelectAllPolicy();
            var response = await MapperPolicy(collectionPolicies);
            return response.Count();
        }

        public async Task<List<modPolicy.Policy>> GetPoliciesPagination(PaginationViewModel pagination)
        {
            try
            {
                var response = await _policyService.SelectPaginatorPolicy(pagination.Page, pagination.RecordsPage);
                var result = await MapperPolicy(response);

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
                policyModel.State = policy.State;
                policyModel.CodeFormat = "PO_" + policy.Code;
                policyModel.StateFormat = policy.State ? "Activo" : "Inactivo";
                policyModel.CreationDate = policy.CreationDate;
                policyModel.UpdateDate = policy.UpdateDate;
                policyModel.CreationDateFormat = policy.CreationDate.ToString("dd/MM/yyyy");
                policyModel.UpdateDateFormat = policy.UpdateDate.ToString("dd/MM/yyyy");

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
            policyModel.State = policy.State;
            policyModel.CodeFormat = "PO_" + policy.Code;
            policyModel.StateFormat = policy.State ? "Activo" : "Inactivo";
            policyModel.CreationDate = policy.CreationDate;
            policyModel.UpdateDate = policy.UpdateDate;
            policyModel.CreationDateFormat = policy.CreationDate.ToString("dd/MM/yyyy");
            policyModel.UpdateDateFormat = policy.UpdateDate.ToString("dd/MM/yyyy");

            return policyModel;
        }

        public async Task<PolicyCreateViewModel> GetPolicyFormatCreate(modPolicy.Policy policy)
        {

            PolicyCreateViewModel policyModel = new PolicyCreateViewModel();

            policyModel.Code = policy.Code;
            policyModel.Name = policy.Name;
            policyModel.Description = policy.Description;
            policyModel.State = policy.State;
            policyModel.CodeFormat = "PO_" + policy.Code;
            policyModel.CreationDate = policy.CreationDate;
            policyModel.UpdateDate = policy.UpdateDate;

            return policyModel;
        }

        public async Task<modPolicy.Policy> GetPolicyByCode(int code)
        {
            var response = await _policyService.SelectByIdPolicy(new PolicyModel { Code = code });
            var policy = await MapperToPolicy(response);
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
                    ModifieldBy = "CreateToUserDev",
                    State = request.State,
                };

                var response = await _policyService.InsertPolicy(policy);

                return response.Equals(1) ? "Politica creada correctamente" : "Error creando la politica";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<PolicyViewModel>> GetFilterPolicies(FilterViewModel filterModel)
        {
            List<modPolicy.Policy> allPolicies = await GetPolicies();
            List<PolicyViewModel> policiesFilter = await GetPolicesFormat(allPolicies);

            if(filterModel.ColumValue == null || filterModel.ColumValue == "" || filterModel.ValueFilter == null || filterModel.ValueFilter == "")
            {
                return policiesFilter;
            }
            else
            {
                policiesFilter = await applyFilter(filterModel, policiesFilter);
            }

            return policiesFilter;
        }

        private async Task<List<PolicyViewModel>> applyFilter(FilterViewModel filterModel, List<PolicyViewModel> allPolicies)
        {
            Console.WriteLine(filterModel.TypeRow.ToString());

            var property = typeof(PolicyViewModel).GetProperty(filterModel.ColumValue);

            List<PolicyViewModel> policiesFilter = new List<PolicyViewModel>();
            if (filterModel.TypeRow == "Select") {
                foreach (PolicyViewModel policy in allPolicies)
                {
                    if (property.GetValue(policy).ToString().ToUpper() == filterModel.ValueFilter.ToUpper())
                    {
                        policiesFilter.Add(policy);
                    }
                }
            }
            else
            {
                foreach (PolicyViewModel policy in allPolicies)
                {
                    if (property.GetValue(policy).ToString().ToUpper().Contains(filterModel.ValueFilter.ToUpper()))
                    {
                        policiesFilter.Add(policy);
                    }
                }
            }
           
            return policiesFilter;
        }

        public List<PolicyViewModel> GetFilterPagination(List<PolicyViewModel> inicialPolicies, PaginationViewModel paginationViewModel, int totalData)
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

            List<PolicyViewModel> policiesFilterPagination = inicialPolicies.GetRange(index, count);

            return policiesFilterPagination;
        }

        private async Task<List<modPolicy.Policy>> MapperPolicy(List<PolicyModel> policyModel)
        {
            return await Task.Run(() =>
            {
                List<modPolicy.Policy> policies = new List<modPolicy.Policy>();
                if (policyModel.Count > 0)
                {                    
                    foreach (var policy in policyModel)
                    {
                        policies.Add(MapperToPolicy(policy).Result);
                    }
                }
                return policies;
            });
        }

        private async Task<modPolicy.Policy> MapperToPolicy(PolicyModel policy)
        {
            return await Task.Run(() =>
            {
                modPolicy.Policy model = new modPolicy.Policy
                {
                    Code = Convert.ToInt32(policy.Code),
                    Name = policy.Name,
                    Description = policy.Description,
                    Objetive = policy.Objetive,
                    CreationDate = policy.CreationDate,
                    UpdateDate = policy.ModifieldDate,
                    UserOwner = policy.ModifieldBy,
                    State = policy.State,
                };
                return model;
            });
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
                    ModifieldBy = "CreateToUserDev",
                    State = request.State
                };

                var response = await _policyService.InsertPolicy(policy);

                return response.Equals(1) ? "Politica creada correctamente" : "Error creando la politica";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> UpdatePolicy(modPolicy.Policy policy)
        {
            var mapping = await MapperUpdatePolicy(policy);
            var response = await _policyService.UpdatePolicy(mapping);

            return response.Equals(1) ? "Politica actualizada correctamente" : "Error actualizando la politica";
        }

        public async Task<string> ActivePolicy(modPolicy.Policy policy)
        {
            var mapping = await MapperActiveePolicy(policy);
            var response = await _policyService.UpdatePolicy(mapping);

            return response.Equals(1) ? "Politica actualizada correctamente" : "Error actualizando la politica";
        }

        public async Task<string> DesactivePolicy(modPolicy.Policy policy)
        {
            var mapping = await MapperDesactiveePolicy(policy);
            var response = await _policyService.UpdatePolicy(mapping);

            return response.Equals(1) ? "Politica actualizada correctamente" : "Error actualizando la politica";
        }

        private async Task<PolicyModel> MapperUpdatePolicy(modPolicy.Policy policy)
        {
            return await Task.Run(() =>
            {
                PolicyModel model = new PolicyModel
                {
<<<<<<< .mine
                    Code = policy.Code,
=======
                    Code = policy.Code.ToString(),
>>>>>>> .theirs
                    Name = policy.Name,
                    Description = policy.Description,
                    Objetive = policy.Objetive,
                    CreationDate = policy.CreationDate,
                    ModifieldDate = DateTime.Now,
                    ModifieldBy = "CreateToUserDev",
                    State = policy.State
                };
                return model;
            });
        }

        private async Task<PolicyModel> MapperActiveePolicy(modPolicy.Policy policy)
        {
            return await Task.Run(() =>
            {
                PolicyModel model = new PolicyModel
                {
                    Code = policy.Code.ToString(),
                    Name = policy.Name,
                    Description = policy.Description,
                    Objetive = policy.Objetive,
                    CreationDate = policy.CreationDate,
                    ModifieldDate = DateTime.Now,
                    ModifieldBy = "CreateToUserDev",
                    State = true
                };
                return model;
            });
        }

        private async Task<PolicyModel> MapperDesactiveePolicy(modPolicy.Policy policy)
        {
            return await Task.Run(() =>
            {
                PolicyModel model = new PolicyModel
                {
                    Code = policy.Code.ToString(),
                    Name = policy.Name,
                    Description = policy.Description,
                    Objetive = policy.Objetive,
                    CreationDate = policy.CreationDate,
                    ModifieldDate = DateTime.Now,
                    ModifieldBy = "CreateToUserDev",
                    State = false
                };
                return model;
            });
        }
    }
}
