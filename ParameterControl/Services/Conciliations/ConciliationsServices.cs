using Newtonsoft.Json.Linq;
using ParameterControl.Models.Conciliation;
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
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica1",
                    Required = "Si",
                    State = true
                },
                new Conciliation(){
                    Id = "2",
                     Code = "COD_001",
                    Name = "Conciliacion_1",
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica1",
                    Required = "No",
                    State = false
                },
                new Conciliation(){
                    Id = "3",
                    Code = "COD_001",
                    Name = "Conciliacion_1",
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica1",
                    Required = "Si",
                    State = true
                },
                new Conciliation(){
                    Id = "4",
                     Code = "COD_001",
                    Name = "Conciliacion_1",
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica1",
                    Required = "Si",
                    State = false
                },
                new Conciliation(){
                    Id = "5",
                     Code = "COD_001",
                    Name = "Conciliacion_1",
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica1",
                    Required = "Si",
                    State = true
                },
                new Conciliation(){
                    Id = "6",
                     Code = "COD_001",
                    Name = "Conciliacion_1",
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica1",
                    Required = "Si",
                    State = false
                },
                new Conciliation(){
                    Id = "7",
                     Code = "COD_001",
                    Name = "Conciliacion_1",
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica1",
                    Required = "No",
                    State = true
                },
                new Conciliation(){
                    Id = "8",
                     Code = "COD_001",
                    Name = "Conciliacion_1",
                    Package = "paqueteEjemplo",
                    Email = "ejemplo@gmail.com",
                    Destination = "ejemploDestino",
                    Policies = "Politica1",
                    Required = "Si",
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
