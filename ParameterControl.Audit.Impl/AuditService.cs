using Microsoft.Extensions.Configuration;
using ParameterControl.Audit.DataAccess;
using ParameterControl.Audit.Entities;
using ParameterControl.Audit.Interfaces;
using System.Data;
using System.Text.Json.Nodes;

namespace ParameterControl.Audit.Impl
{
    public class AuditService : IAuditService
    {
        private readonly SetAudit _setAudit;

        public AuditService(IConfiguration configuration)
        {
            _setAudit = new SetAudit(configuration);
        }

        public async Task InsertAudit(AuditModel auditModel)
        {
            try
            {
                await Task.Run(() =>
                {
                    return _setAudit.InsertAudit(auditModel);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
