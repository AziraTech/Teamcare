
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using teamcare.business.Models;

namespace teamcare.business.Services
{
    public interface IServiceUserLogService : IService<ServiceUserLogModel>
    {
        Task<IEnumerable<ServiceUserLogModel>> ListAllSortedFiltered(Guid? filterByserviceuser, bool IsApprove, string daterange);
        Task<ServiceUserLogModel> UpdateLogByParam(int type,Guid id,bool Status,String logtext,Guid UserId);

    }
}


