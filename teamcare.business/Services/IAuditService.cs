using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.data.Repositories;

namespace teamcare.business.Services
{
	public interface IAuditService : IService<AuditModel>
	{
        void Execute(Func<IAuditRepository, Task> databaseWork);
        Task<IEnumerable<AuditModel>> ListAllSortedFiltered(Guid? filterByserviceuser, string daterange);

    }
}
