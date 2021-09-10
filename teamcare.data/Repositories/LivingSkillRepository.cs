using System.Security.Claims;
using teamcare.data.Data;
using teamcare.data.Entities.SkillAssessments;

namespace teamcare.data.Repositories
{
    public class LivingSkillRepository : Repository<LivingSkill>, ILivingSkillRepository
    {
        public LivingSkillRepository(TeamcareDbContext dbContext, ClaimsPrincipal user) : base(dbContext, user)
        {
            
        }
    }
}