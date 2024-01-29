using AutoMapper;
using Microsoft.Extensions.Configuration;
using ParameterControl.Policy.DataAccess;
using ParameterControl.Policy.Entities;
using ParameterControl.Policy.Impl;
using ParameterControl.Services.Policies;

namespace ParameterControl.Test
{
    [TestFixture]
    public class Tests
    {
        private IConfiguration _configuration;
        private IPoliciesServices _policiesServices;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.test.json", optional:false, reloadOnChange:true)
                .AddJsonFile($"appsettings.test.json", optional:true, reloadOnChange:true)
                .Build();

            _configuration = builder;
            _policiesServices = new PoliciesServices(_configuration,_mapper);
        }

        [Test]
        public async Task Test1()
        {
            try
            {
                var policy = new Models.Policy.Policy
                {
                    Name = "PO_AIC_430",
                    Description = "Conciliacion Mandante Bonos Prepagos",
                    Objetive = "Mitigar pérdidas de ingresos por inconsistencias en el aprovisionamiento de las ofertas prepago sin costo en la plataforma YELLOWBRICK",
                    Conciliation = 1
                };
                var response = await _policiesServices.InsertPolicy(policy);
                
                Assert.Equals(response,1);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Test]
        public void Test2()
        {
            try
            {
                var response = _policiesServices.GetPoliciesPagination(new Models.Pagination.PaginationViewModel { Page = 1, RecordsPage = 2 });

                Assert.Pass();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Test]
        public void Test3()
        {
            try
            {
                var policy = new PolicyModel
                {
                    Name = "PO_AIC_427",
                    Description = "Test_Description",
                    ModifieldBy = "Test-Dev",
                    Objetive = "Mitigar pérdidas de ingresos por inconsistencias en el aprovisionamiento de las ofertas"
                };

                var setPolicy = new SetPolicy(_configuration);
                var response = setPolicy.InsertPolicy(policy);

                Assert.Pass();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}