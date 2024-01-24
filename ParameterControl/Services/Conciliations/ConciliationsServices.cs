using MessagePack;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using ParameterControl.Models.Conciliation;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Policy;
using ParameterControl.Policy.Interfaces;
using ParameterControl.Services.Policies;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
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
                    Id = "1",
                    Code = "COD_001",
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
                    Id = "2",
                    Code = "COD_001",
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
                    Id = "3",
                    Code = "COD_001",
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
                    Id = "4",
                    Code = "COD_001",
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
                    Id = "5",
                    Code = "COD_001",
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
                    Id = "6",
                    Code = "COD_001",
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
                    Id = "7",
                    Code = "COD_001",
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
                    Id = "8",
                    Code = "COD_001",
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

                conciliationModel.Id = conciliation.Id;
                conciliationModel.Code = conciliation.Code;
                conciliationModel.Name = conciliation.Name;
                conciliationModel.Description = conciliation.Description;
                conciliationModel.Conciliation_ = conciliation.Conciliation_;
                conciliationModel.Package = conciliation.Package;
                conciliationModel.Email = conciliation.Email;
                conciliationModel.Description = conciliation.Description;
                conciliationModel.Policies = conciliation.Policies;
                conciliationModel.Required = conciliation.Required;
                conciliationModel.RequiredFormat = conciliation.Required ? "Si" : "No";
                conciliationModel.State = conciliation.State;
                conciliationModel.StateFormat = conciliation.State ? "Activo" : "Inactivo";

                conciliationsModel.Add(conciliationModel);
            }

            return conciliationsModel;
        }

        public async Task<modConciliation.Conciliation> GetConciliationsById(string id)
        {
            Conciliation conciliation = conciliations.Find(conciliation => conciliation.Id == id);
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
