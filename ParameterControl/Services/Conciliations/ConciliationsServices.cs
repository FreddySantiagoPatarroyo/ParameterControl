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
using modPolicy = ParameterControl.Models.Policy;

namespace ParameterControl.Services.Conciliations
{
    public class ConciliationsServices : IConciliationsServices
    {
        private List<modConciliation.Conciliation> conciliations = new List<modConciliation.Conciliation>();
        private readonly IPoliciesServices policiesServices;
        private readonly IConciliationService conciliationServices;

        public ConciliationsServices(
            IPoliciesServices policiesServices,
            IConfiguration configuration
        )
        {
            conciliationServices = new ConciliationService(configuration);
            conciliations = new List<modConciliation.Conciliation>()
            {
                new modConciliation.Conciliation(){
                    Code = 1,
                    Name = "Conciliacion_1",
                    Description = "Descriptionasdasdasdasdasdasdasdasdasdasd",
                    Email = "ejemplo@gmail.com",
                    Destination = 123,
                    Policy = "Politica1",
                    Required = true,
                    ControlType = "Voz",
                    OperationType = "Model",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new modConciliation.Conciliation(){
                    Code = 2,
                    Name = "Conciliacion_1",
                    Description = "Description",
                    Email = "ejemplo@gmail.com",
                    Destination = 145,
                    Policy = "Politica2",
                    Required = false,
                    ControlType = "Voz",
                    OperationType = "Model",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new modConciliation.Conciliation(){
                    Code = 3,
                    Name = "Conciliacion_1",
                    Description = "Description",
                    Email = "ejemplo@gmail.com",
                    Destination = 213,
                    Policy = "Politica1",
                    Required = true,
                    ControlType = "Voz",
                    OperationType = "Model",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new modConciliation.Conciliation(){
                    Code = 4,
                    Name = "Conciliacion_1",
                    Description = "Description",
                    Email = "ejemplo@gmail.com",
                    Destination = 123,
                    Policy = "Politica1",
                    Required = false,
                    ControlType = "Voz",
                    OperationType = "Model",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = "User1"
                },
                new modConciliation.Conciliation(){
                    Code = 5,
                    Name = "Conciliacion_1",
                    Description = "Description",
                    Email = "ejemplo@gmail.com",
                    Destination = 123,
                    Policy = "Politica1",
                    Required = true,
                    ControlType = "Voz",
                    OperationType = "Model",
                    State = true,
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
                conciliationModel.Email = conciliation.Email;
                conciliationModel.Destination = conciliation.Destination;
                conciliationModel.Policy = conciliation.Policy;
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
            conciliationModel.Email = conciliation.Email;
            conciliationModel.Destination = conciliation.Destination;
            conciliationModel.Policy = conciliation.Policy;
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
            conciliationModel.Email = conciliation.Email;
            conciliationModel.Destination = conciliation.Destination;
            conciliationModel.Policy = conciliation.Policy;
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
            var response = await conciliationServices.SelectByIdConciliation(new ConciliationModel { Code = code });
            var conciliation = await MapperToConciliation(response);
            return conciliation;
        }

        public async Task<List<SelectListItem>> GetOperationsType()
        {
            List<SelectListItem> operationsType = new List<SelectListItem>().ToList();
            operationsType.Add(new SelectListItem("Model", "1"));
            operationsType.Add(new SelectListItem("Fija", "2"));
            return operationsType;
        }

		public async Task<List<modPolicy.Policy>> GetPolicies()
        {
            List<modPolicy.Policy> policies = await policiesServices.GetPolicies();
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
                    Code = Convert.ToInt32(conciliation.Code),
                    Name = conciliation.ConciliationName,
                    Description = conciliation.Description                     
                };
                return model;
            });
        }

        public async Task<string> UpdateConciliation(modConciliation.Conciliation conciliation)
        {
            var mapping = await MapperUpdateConciliation(conciliation);
            var response = await conciliationServices.UpdateConciliation(mapping);

            return response.Equals(1) ? "Conciliacion actualizada correctamente" : "Error actualizando la conciliacion";
        }

        private async Task<ConciliationModel> MapperUpdateConciliation(modConciliation.Conciliation conciliation)
        {
            return await Task.Run(() =>
            {
                ConciliationModel model = new ConciliationModel
                {
                    Code = conciliation.Code,
                    ConciliationName = conciliation.Name,
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
