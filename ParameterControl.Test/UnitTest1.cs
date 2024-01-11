using Microsoft.Extensions.Configuration;
using ParameterControl.Policy.DataAccess;
using ParameterControl.Policy.Entities;
using ParameterControl.Policy.Impl;

namespace ParameterControl.Test
{
    [TestFixture]
    public class Tests
    {
        private IConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.test.json", optional:false, reloadOnChange:true)
                .AddJsonFile($"appsettings.test.json", optional:true, reloadOnChange:true)
                .Build();

            _configuration = builder;
        }

        [Test]
        public void Test1()
        {
            try
            {
                var policy = new PolicyModel
                {
                    Code = "Test-003",
                    Name = "Test-Policy",
                    Description = "Test_Description",
                    ModifieldBy = "Test-Dev"
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