using System.Security.Claims;
using teamcare.data.Data;
using teamcare.data.Entities.SkillAssessments;

namespace teamcare.data.Repositories
{
    public class SkillGroupsRepository : Repository<SkillGroup>, ISkillGroupsRepository
    {
        public SkillGroupsRepository(TeamcareDbContext dbContext, ClaimsPrincipal user) : base(dbContext, user)
        {
            
        }
    }
}