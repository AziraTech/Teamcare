using System.Security.Claims;
using teamcare.data.Data;
using teamcare.data.Entities;

namespace teamcare.data.Repositories
{
    public class ResidenceRepository : Repository<Residence>, IResidenceRepository
    {
        public ResidenceRepository(TeamcareDbContext dbContext, ClaimsPrincipal user) : base(dbContext, user)
        {
            
        }
    }
}