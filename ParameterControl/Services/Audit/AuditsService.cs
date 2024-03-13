using ParameterControl.Audit.Entities;
using ParameterControl.Audit.Impl;
using ParameterControl.Audit.Interfaces;
using ParameterControl.Models.Audit;
using ParameterControl.Services.Util;
using ParameterControl.Stage.Impl;
using System.Security.Policy;

using modAudit = ParameterControl.Models.Audit;

namespace ParameterControl.Services.Audit
{
    public class AuditsService : IAuditsService
    {
        private readonly IAuditService _auditService;

        public AuditsService(IConfiguration configuration)
        {
            _auditService = new AuditService(configuration);
        }

        public async Task InsertAudit(modAudit.Audit request)
        {
            AuditModel audit = new AuditModel()
            {
                ModifieldDate = request.ModifieldDate,
                UserCode = request.UserCode,
                Action = request.Action,
                Component = request.Component,
                BeforeValue = request.BeforeValue,
            };

            await _auditService.InsertAudit(audit);
        }
    }
}
