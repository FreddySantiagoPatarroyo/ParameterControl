using Newtonsoft.Json.Linq;
using ParameterControl.Models.Conciliation;
using ParameterControl.Models.Filter;
using ParameterControl.Models.Policy;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

namespace ParameterControl.Services.Conciliations
{
    public class ConciliationsServices : IConciliationsServices
    {
        private List<Conciliation> conciliations = new List<Conciliation>();
        public ConciliationsServices()
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
                    Required = "Si",
                    Conciliation_ = "Conciliation",
                    State = true
                },
                new Conciliation(){
                    Id = "2",
                    Code = "COD_001",
                    Name = "Conciliacion_1",
                    Description = "Description",
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica1",
                    Required = "No",
                    Conciliation_ = "Conciliation",
                    State = false
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
                    Required = "Si",
                    Conciliation_ = "Conciliation",
                    State = true
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
                    Required = "Si",
                    Conciliation_ = "Conciliation",
                    State = false
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
                    Required = "Si",
                    Conciliation_ = "Conciliation",
                    State = true
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
                    Required = "Si",
                    Conciliation_ = "Conciliation",
                    State = false
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
                    Required = "No",
                    Conciliation_ = "Conciliation",
                    State = true
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
                    Required = "Si",
                    Conciliation_ = "Conciliation",
                    State = false
                }
            };  
        }

        public async Task<List<Conciliation>> GetConciliations()
        {
            return conciliations;
        }

        public async Task<Conciliation> GetConciliationsById(string id)
        {
            Conciliation conciliation = conciliations.Find(conciliation => conciliation.Id == id);
            return conciliation;
        }
        public async Task<List<string>> GetPolicies()
        {
            List<string> policies = new List<string>()
           {
               "Politica1",
               "Politica2",
               "Politica3"
           };
            return policies;
        }

        public async Task<List<Conciliation>> GetFilterConciliations(FilterViewModel filterModel)
        {
            List<Conciliation> ConciliationsFilter = new List<Conciliation>();

            if ((filterModel.ColumValue == null || filterModel.ColumValue == "" || filterModel.ValueFilter == null || filterModel.ValueFilter == ""))
            {
                ConciliationsFilter = conciliations;
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

            return ConciliationsFilter;
        }

        private List<Conciliation> applyFilter(FilterViewModel filterModel)
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

        public async Task<List<string>> GetRequired()
        {
            List<string> required = new List<string>()
           {
               "Si",
               "No"
           };
            return required;
        }

    }
}
