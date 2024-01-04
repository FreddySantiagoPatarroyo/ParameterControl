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
                    Package = "algo",
                    Policies = "algo",
                    Description = "Descripcion ejemplo",
                    Conciliation_ = "111",
                    Result = false,
                    State = true
                },
                new Conciliation(){
                    Id = "2",
                    Code = "COD_006",
                    Name = "Name",
                    Package = "algo",
                    Policies = "algo",
                    Description = "Description",
                    Conciliation_ = "222",
                    Result = false,
                    State = false
                },
                new Conciliation(){
                    Id = "3",
                    Code = "COD_001",
                    Name = "Conciliacion_1",
                    Package = "algo",
                    Policies = "algo",
                    Description = "Descripcion ejemplo",
                    Conciliation_ = "333",
                    Result = false,
                    State = true
                },
                new Conciliation(){
                    Id = "4",
                    Code = "COD_006",
                    Name = "Name",
                    Package = "algo",
                    Policies = "algo",
                    Description = "Description",
                    Conciliation_ = "111",
                    Result = false,
                    State = false
                },
                new Conciliation(){
                    Id = "5",
                    Code = "COD_001",
                    Name = "Conciliacion_1",
                    Package = "algo",
                    Policies = "algo",
                    Description = "Descripcion ejemplo",
                    Conciliation_ = "123",
                    Result = false,
                    State = true
                },
                new Conciliation(){
                    Id = "6",
                    Code = "COD_006",
                    Name = "Name",
                    Package = "algo",
                    Policies = "algo",
                    Description = "Description",
                    Conciliation_ = "321",
                    Result = false,
                    State = false
                },
                new Conciliation(){
                    Id = "7",
                    Code = "COD_001",
                    Name = "Conciliacion_1",
                    Package = "algo",
                    Policies = "algo",
                    Description = "Descripcion ejemplo",
                    Conciliation_ = "234",
                    Result = false,
                    State = true
                },
                new Conciliation(){
                    Id = "8",
                    Code = "",
                    Name = "Name",
                    Package = "algo",
                    Policies = "algo",
                    Description = "Description",
                    Conciliation_ = "444",
                    Result = false,
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
