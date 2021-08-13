using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using teamcare.data.Entities;

namespace teamcare.data.Repositories
{
	public interface IAuditRepository : IRepository<Audit>
	{
		Task<bool> CreateAuditRecord(Audit audit);
		Task<List<Audit>> GetAllAudit();
	}
}
