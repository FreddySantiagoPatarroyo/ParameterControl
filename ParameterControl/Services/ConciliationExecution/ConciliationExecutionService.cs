using ParameterControl.Services.ConciliationExecution;
using ParameterControl.Services.Conciliations;
using modConciliation = ParameterControl.Models.Conciliation;

namespace ParameterControl.Services.ConciliationExecition
{
    public class ConciliationExecutionService : IConciliationExecutionService
    {
        private readonly IConciliationsServices conciliationsService;

        public ConciliationExecutionService(
            IConfiguration configuration,
            IConciliationsServices conciliationsService
        )
        {
            this.conciliationsService = conciliationsService;
        }

        public async Task<List<modConciliation.Conciliation>> GetConciliationsActives()
        {
            var collectionConciliations = await conciliationsService.GetConciliations();
            var response = new List<modConciliation.Conciliation>();

            foreach (var conciliation in collectionConciliations)
            {
                if (conciliation.State == true)
                {
                    response.Add(conciliation);
                }
            }

            return response;
        }

        public async Task<modConciliation.Conciliation> GetConciliationByCode(int code)
        {
            var conciliation = await conciliationsService.GetConciliationsByCode(code);
            return conciliation;
        }
    }
}
