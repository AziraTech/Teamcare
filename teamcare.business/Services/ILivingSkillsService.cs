using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.data.Repositories;

namespace teamcare.business.Services
{
	public interface ILivingSkillService : IService<LivingSkillsModel>
	{
        Task<IEnumerable<LivingSkillsModel>> ListByGroupId(Guid id);

    }
}
