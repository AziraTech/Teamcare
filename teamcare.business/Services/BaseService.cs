using System;
using System.Security.Claims;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.data.Entities;

namespace teamcare.business.Services
{
    public class BaseService : IBaseService
    {
        private readonly IAuditService _auditService;

        public BaseService(IAuditService auditService)
        {
            _auditService = auditService;
        }

        public async Task<AuditModel> RecordAuditEntry(AuditModel audit)
        {       
            return await _auditService.AddAsync(audit,new Guid());
        }
    }
}