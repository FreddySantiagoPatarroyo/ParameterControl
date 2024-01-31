using Microsoft.AspNetCore.Mvc.Rendering;
using ParameterControl.Conciliation.Entities;
using ParameterControl.Conciliation.Impl;
using ParameterControl.Conciliation.Interfaces;
using ParameterControl.Models.Conciliation;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Policy;
using ParameterControl.Policy.Entities;
using ParameterControl.Services.Policies;
using modConciliation = ParameterControl.Models.Conciliation;
using modPolicy = ParameterControl.Models;

namespace ParameterControl.Services.Conciliations
{
    public class ConciliationsServices : IConciliationsServices
    {
        private List<modPolicy.Conciliation.Conciliation> conciliations = new List<modPolicy.Conciliation.Conciliation>();
        private readonly IPoliciesServices policiesServices;
        private readonly IConciliationService conciliationServices;

        public ConciliationsServices(
            IPoliciesServices policiesServices,
            IConfiguration configuration
        )
        {
            conciliationServices = new ConciliationService(configuration);
            conciliations = new List<modPolicy.Conciliation.Conciliation>()
            {
                new modPolicy.Conciliation.Conciliation(){
                    Code = 1,
                    Name = "Conciliacion_1",
                    Description = "Descriptionasdasdasdasdasdasdasdasdasdasd",
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica1",
                    Required = true,
                    Conciliation_ = "Conciliation",
                    ControlType = "Voz",
                    OperationType = "Model",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new modPolicy.Conciliation.Conciliation(){
                    Code = 2,
                    Name = "Conciliacion_1",
                    Description = "Description",
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica2",
                    Required = false,
                    Conciliation_ = "Conciliation",
                    ControlType = "Voz",
                    OperationType = "Model",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new modPolicy.Conciliation.Conciliation(){
                    Code = 3,
                    Name = "Conciliacion_1",
                    Description = "Description",
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica1",
                    Required = true,
                    Conciliation_ = "Conciliation",
                    ControlType = "Voz",
                    OperationType = "Model",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new modPolicy.Conciliation.Conciliation(){
                    Code = 4,
                    Name = "Conciliacion_1",
                    Description = "Description",
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica1",
                    Required = false,
                    Conciliation_ = "Conciliation",
                    ControlType = "Voz",
                    OperationType = "Model",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new modPolicy.Conciliation.Conciliation(){
                    Code = 5,
                    Name = "Conciliacion_1",
                    Description = "Description",
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica1",
                    Required = true,
                    Conciliation_ = "Conciliation",
                    ControlType = "Voz",
                    OperationType = "Model",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new modPolicy.Conciliation.Conciliation(){
                    Code = 6,
                    Name = "Conciliacion_1",
                    Description = "Description",
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica1",
                    Required = true,
                    Conciliation_ = "Conciliation",
                    ControlType = "Voz",
                    OperationType = "Model",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new modPolicy.Conciliation.Conciliation(){
                    Code = 7,
                    Name = "Conciliacion_1",
                    Description = "Description",
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica1",
                    Required = false,
                    Conciliation_ = "Conciliation",
                    ControlType = "Voz",
                    OperationType = "Model",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new modPolicy.Conciliation.Conciliation(){
                    Code = 8,
                    Name = "Conciliacion_1",
                    Description = "Description",
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica1",
                    Required = true,
                    Conciliation_ = "Conciliation",
                    ControlType = "Voz",
                    OperationType = "Model",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
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

        public async Task<List<ConciliationViewModel>> GetConciliationsFormat(List<modConciliation.Conciliation> conciliations)
        {
            List<ConciliationViewModel> conciliationsModel = new List<ConciliationViewModel>();

            foreach (modConciliation.Conciliation conciliation in conciliations)
            {
                ConciliationViewModel conciliationModel = new ConciliationViewModel();

                conciliationModel.Code = conciliation.Code;
                conciliationModel.Name = conciliation.Name;
                conciliationModel.Description = conciliation.Description;
                conciliationModel.Conciliation_ = conciliation.Conciliation_;
                conciliationModel.Package = conciliation.Package;
                conciliationModel.Email = conciliation.Email;
                conciliationModel.Destination = conciliation.Destination;
                conciliationModel.Policies = conciliation.Policies;
                conciliationModel.Required = conciliation.Required;
                conciliationModel.ControlType = conciliation.ControlType;
                conciliationModel.OperationType = conciliation.OperationType;
                conciliationModel.RequiredFormat = conciliation.Required ? "Si" : "No";
                conciliationModel.State = conciliation.State;
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
            conciliationModel.Conciliation_ = conciliation.Conciliation_;
            conciliationModel.Package = conciliation.Package;
            conciliationModel.Email = conciliation.Email;
            conciliationModel.Destination = conciliation.Destination;
            conciliationModel.Policies = conciliation.Policies;
            conciliationModel.Required = conciliation.Required;
            conciliationModel.ControlType = conciliation.ControlType;
            conciliationModel.OperationType = conciliation.OperationType;
            conciliationModel.RequiredFormat = conciliation.Required ? "Si" : "No";
            conciliationModel.State = conciliation.State;
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
            conciliationModel.Conciliation_ = conciliation.Conciliation_;
            conciliationModel.Package = conciliation.Package;
            conciliationModel.Email = conciliation.Email;
            conciliationModel.Destination = conciliation.Destination;
            conciliationModel.Policies = conciliation.Policies;
            conciliationModel.Required = conciliation.Required;
            conciliationModel.ControlType = conciliation.ControlType;
            conciliationModel.OperationType = conciliation.OperationType;
            conciliationModel.RequiredFormat = conciliation.Required ? "Si" : "No";
            conciliationModel.State = conciliation.State;
            conciliationModel.CodeFormat = "CO_" + conciliation.Code;
            conciliationModel.CreationDate = conciliation.CreationDate;
            conciliationModel.UpdateDate = conciliation.UpdateDate;

            return conciliationModel;
        }

        public async Task<modConciliation.Conciliation> GetConciliationsByCode(int code)
        {
            modConciliation.Conciliation conciliation = conciliations.Find(conciliation => conciliation.Code == code);
            return conciliation;
        }
        public async Task<List<modPolicy.Policy.Policy>> GetPolicies()
        {
            List<modPolicy.Policy.Policy> policies = await policiesServices.GetPolicies();
            return policies;
        }

        public async Task<List<SelectListItem>> GetRequired()
        {
            List<SelectListItem> required = new List<SelectListItem>().ToList();
            required.Add(new SelectListItem("Si", "True"));
            required.Add(new SelectListItem("No", "False"));
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
            Console.WriteLine(filterModel.TypeRow.ToString());

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

        public async Task<string> InsertConciliation(modConciliation.Conciliation request)
        {
            ConciliationModel conciliation = new ConciliationModel
            {
            };

            var response = await conciliationServices.InsertConciliation(conciliation);

            return response.Equals(1) ? "Conciliacion creada correctamente" : "Error creando la conciliacion";
        }

        private async Task<List<modPolicy.Conciliation.Conciliation>> MapperConciliation(List<ConciliationModel> conciliationModel)
        {
            return await Task.Run(() =>
            {
                List<modPolicy.Conciliation.Conciliation> conciliations = new List<modPolicy.Conciliation.Conciliation>();
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

        private async Task<modPolicy.Conciliation.Conciliation> MapperToConciliation(ConciliationModel conciliation)
        {
            return await Task.Run(() =>
            {
                modPolicy.Conciliation.Conciliation model = new modPolicy.Conciliation.Conciliation
                {
                    Code = Convert.ToInt32(conciliation.Id),
                    Name = conciliation.ConciliationName,
                    Description = conciliation.Description                     
                };
                return model;
            });
        }

        public async Task<string> UpdateConciliation(modPolicy.Conciliation.Conciliation conciliation)
        {
            var mapping = await MapperUpdateConciliation(conciliation);
            var response = await policiesServices.UpdatePolicy(new modPolicy.Policy.Policy { });

            return response.Equals(1) ? "Conciliacion actualizada correctamente" : "Error actualizando la conciliacion";
        }

        private async Task<PolicyModel> MapperUpdateConciliation(modPolicy.Conciliation.Conciliation conciliation)
        {
            return await Task.Run(() =>
            {
                PolicyModel model = new PolicyModel
                {
                    Code = conciliation.Code.ToString(),
                    Name = conciliation.Name,
                    Description = conciliation.Description,
                    ModifieldBy = conciliation.UserOwner,
                    State = conciliation.State
                };
                return model;
            });
        }

        public async Task<int> CountConciliations()
        {
            return await conciliationServices.SelectCountConciliation();
        }
    }
}
