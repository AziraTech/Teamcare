using System.Security.Claims;
using teamcare.data.Data;
using teamcare.data.Entities.SkillAssessments;

namespace teamcare.data.Repositories
{
    public class AssessmentRepository : Repository<Assessment>, IAssessmentRepository
    {
        public AssessmentRepository(TeamcareDbContext dbContext, ClaimsPrincipal user) : base(dbContext, user)
        {
            
        }
    }
}