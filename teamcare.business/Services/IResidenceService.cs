using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.data.Entities;

namespace teamcare.business.Services
{
    public interface IResidenceService : IService<ResidenceModel>
    {
        Task<IEnumerable<ResidenceModel>> ListAllResidenceFiltered(bool isArchive);
        Task<ResidenceModel> ArchiveUnArchiveResidence(int reasonid, Guid userid, int type);
    }
}
