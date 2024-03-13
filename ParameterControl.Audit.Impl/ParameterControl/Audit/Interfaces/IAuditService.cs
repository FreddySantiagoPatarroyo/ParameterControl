using ParameterControl.Audit.Entities;
using ParameterControl.Audit.Impl;

namespace ParameterControl.Audit.Interfaces
{
    public interface IAuditService
    {
        Task InsertAudit(AuditModel auditModel);
    }
}