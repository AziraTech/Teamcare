using System.Security.Claims;
using teamcare.data.Data;
using teamcare.data.Entities.SkillAssessments;

namespace teamcare.data.Repositories
{
    public class AssessmentSkillRepository : Repository<AssessmentSkill>, IAssessmentSkillRepository
    {
        public AssessmentSkillRepository(TeamcareDbContext dbContext, ClaimsPrincipal user) : base(dbContext, user)
        {
            
        }
    }
}