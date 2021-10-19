using System.Security.Claims;
using teamcare.data.Data;
using teamcare.data.Entities.ServiceUsers;

namespace teamcare.data.Repositories
{
    public class WeightReadingRepository : Repository<WeightReading>, IWeightReadingRepository
    {
        public WeightReadingRepository(TeamcareDbContext dbContext, ClaimsPrincipal user) : base(dbContext, user)
        {
            
        }
    }
}