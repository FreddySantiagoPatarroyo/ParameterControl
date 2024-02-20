


namespace ParameterControl.Services.ConciliationExecution
{
    public interface IConciliationExecutionService
    {
        Task<Models.Conciliation.Conciliation> GetConciliationByCode(int code);
        Task<List<Models.Conciliation.Conciliation>> GetConciliationsActives();
    }
}
