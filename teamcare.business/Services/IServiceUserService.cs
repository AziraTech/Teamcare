using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using teamcare.business.Models;

namespace teamcare.business.Services
{
    public interface IServiceUserService : IService<ServiceUserModel>
    {
        Task<IEnumerable<ServiceUserModel>> ListAllSortedFiltered(int sortBy, string filterBy,bool isArchive);
        Task<ServiceUserModel> ArchiveUnArchiveUser(int reasonid, Guid userid,int type);
    }
}
