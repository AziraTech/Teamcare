using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.data.Repositories;

namespace teamcare.business.Services
{
	public interface IAssessmentSkillService : IService<AssessmentSkillModel>
	{
        Task SaveAssessmentSkillList(Guid AssessmentId, IEnumerable<AssessmentSkillModel> model);

    }
}
