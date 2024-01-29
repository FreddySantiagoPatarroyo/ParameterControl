using Microsoft.AspNetCore.Mvc.Rendering;
using ParameterControl.Models.Conciliation;
using ParameterControl.Models.Filter;
using ParameterControl.Services.Policies;
using modConciliation = ParameterControl.Models.Conciliation;
using modPolicy = ParameterControl.Models.Policy;

namespace ParameterControl.Services.Conciliations
{
    public class ConciliationsServices : IConciliationsServices
    {
        private List<Conciliation> conciliations = new List<Conciliation>();
        private readonly IPoliciesServices policiesServices;

        public ConciliationsServices(
            IPoliciesServices policiesServices
        )
        {
            conciliations = new List<Conciliation>()
            {
                new Conciliation(){
                    Code = 1,
                    Name = "Conciliacion_1",
                    Description = "Description",
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica1",
                    Required = true,
                    Conciliation_ = "Conciliation",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Conciliation(){
                    Code = 2,
                    Name = "Conciliacion_1",
                    Description = "Description",
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica2",
                    Required = false,
                    Conciliation_ = "Conciliation",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Conciliation(){
                    Code = 3,
                    Name = "Conciliacion_1",
                    Description = "Description",
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica1",
                    Required = true,
                    Conciliation_ = "Conciliation",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Conciliation(){
                    Code = 4,
                    Name = "Conciliacion_1",
                    Description = "Description",
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica1",
                    Required = false,
                    Conciliation_ = "Conciliation",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Conciliation(){
                    Code = 5,
                    Name = "Conciliacion_1",
                    Description = "Description",
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica1",
                    Required = true,
                    Conciliation_ = "Conciliation",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Conciliation(){
                    Code = 6,
                    Name = "Conciliacion_1",
                    Description = "Description",
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica1",
                    Required = true,
                    Conciliation_ = "Conciliation",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Conciliation(){
                    Code = 7,
                    Name = "Conciliacion_1",
                    Description = "Description",
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica1",
                    Required = false,
                    Conciliation_ = "Conciliation",
                    State = true,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                },
                new Conciliation(){
                    Code = 8,
                    Name = "Conciliacion_1",
                    Description = "Description",
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica1",
                    Required = true,
                    Conciliation_ = "Conciliation",
                    State = false,
                    CreationDate = DateTime.Parse("2024-01-10"),
                    UpdateDate = DateTime.Parse("2023-11-09"),
                    UserOwner = 1
                }
            };
            this.policiesServices = policiesServices;
        }

        public async Task<List<modConciliation.Conciliation>> GetConciliations()
        {
            return conciliations;
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
                conciliationModel.RequiredFormat = conciliation.Required ? "Si" : "No";
                conciliationModel.State = conciliation.State;
                conciliationModel.CodeFormat = "CO_" + conciliation.Code;
                conciliationModel.StateFormat = conciliation.State ? "Activo" : "Inactivo";
                conciliationModel.CreationDate = conciliation.CreationDate;
                conciliationModel.UpdateDate = conciliation.UpdateDate;

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
            conciliationModel.RequiredFormat = conciliation.Required ? "Si" : "No";
            conciliationModel.State = conciliation.State;
            conciliationModel.CodeFormat = "CO_" + conciliation.Code;
            conciliationModel.StateFormat = conciliation.State ? "Activo" : "Inactivo";
            conciliationModel.CreationDate = conciliation.CreationDate;
            conciliationModel.UpdateDate = conciliation.UpdateDate;


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
            conciliationModel.RequiredFormat = conciliation.Required ? "Si" : "No";
            conciliationModel.State = conciliation.State;
            conciliationModel.CodeFormat = "CO_" + conciliation.Code;
            conciliationModel.CreationDate = conciliation.CreationDate;
            conciliationModel.UpdateDate = conciliation.UpdateDate;


            return conciliationModel;
        }

        public async Task<modConciliation.Conciliation> GetConciliationsByCode(int code)
        {
            Conciliation conciliation = conciliations.Find(conciliation => conciliation.Code == code);
            return conciliation;
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
            List<modConciliation.Conciliation> ConciliationsFilter = new List<modConciliation.Conciliation>();

            if ((filterModel.ColumValue == null || filterModel.ColumValue == "" || filterModel.ValueFilter == null || filterModel.ValueFilter == ""))
            {
                ConciliationsFilter = await GetConciliations();
            }
            else
            {
                switch (filterModel.ColumValue)
                {
                    case "Code":
                        ConciliationsFilter = applyFilter(filterModel);
                        break;
                    case "Name":
                        ConciliationsFilter = applyFilter(filterModel);
                        break;
                    case "Description":
                        ConciliationsFilter = applyFilter(filterModel);
                        break;
                    case "Conciliation_":
                        ConciliationsFilter = applyFilter(filterModel);
                        break;
                    case "Required":
                        ConciliationsFilter = applyFilter(filterModel);
                        break;
                    default:
                        break;
                }
            }

            return await GetConciliationsFormat(ConciliationsFilter);
        }

        private List<modConciliation.Conciliation> applyFilter(FilterViewModel filterModel)
        {
            var property = typeof(Conciliation).GetProperty(filterModel.ColumValue);

            List<Conciliation> ConciliationsFilter = new List<Conciliation>();

            foreach (Conciliation Conciliation in conciliations)
            {
                if (property.GetValue(Conciliation).ToString().ToUpper().Contains(filterModel.ValueFilter.ToUpper()))
                {
                    ConciliationsFilter.Add(Conciliation);
                }
            }
            return ConciliationsFilter;
        }
    }
}
