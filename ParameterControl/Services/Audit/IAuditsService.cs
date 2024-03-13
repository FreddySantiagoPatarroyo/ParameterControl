
namespace ParameterControl.Services.Audit
{
    public interface IAuditsService
    {
        Task InsertAudit(Models.Audit.Audit request);
    }
}
