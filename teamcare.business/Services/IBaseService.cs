using System.Threading.Tasks;
using teamcare.business.Models;

namespace teamcare.business.Services
{ 
    public interface IBaseService
    {
        Task<AuditModel> RecordAuditEntry(AuditModel audit);
    }
}
