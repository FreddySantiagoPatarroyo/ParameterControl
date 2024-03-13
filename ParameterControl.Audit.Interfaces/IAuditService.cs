using ParameterControl.Audit.Entities;

namespace ParameterControl.Audit.Interfaces
{
    public interface IAuditService<T>
    {
        Task InsertAudit(T entity, AuditModel auditModel);
    }
}
