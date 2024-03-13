using ParameterControl.Audit.Entities;
using ParameterControl.Audit.Impl;
using ParameterControl.Audit.Interfaces;
using ParameterControl.User.Impl;
using ParameterControl.User.Interfaces;

namespace ParameterControl.Services.Util
{
    public class AuditServices
    {
        private readonly IAuditService _auditService;

        public AuditServices(IConfiguration configuration)
        {
            _auditService = new AuditService(configuration);
        }

        public async Task InsertAudit(AuditModel auditModel)
        {
            try
            {
                await _auditService.InsertAudit(auditModel);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
