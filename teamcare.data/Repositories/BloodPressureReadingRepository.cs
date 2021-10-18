using System.Security.Claims;
using teamcare.data.Data;
using teamcare.data.Entities.ServiceUsers;

namespace teamcare.data.Repositories
{
    public class BloodPressureReadingRepository : Repository<BloodPressureReading>, IBloodPressureReadingRepository
    {
        public BloodPressureReadingRepository(TeamcareDbContext dbContext, ClaimsPrincipal user) : base(dbContext, user)
        {
            
        }
    }
}