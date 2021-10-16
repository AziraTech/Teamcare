using System.Security.Claims;
using teamcare.data.Data;
using teamcare.data.Entities.ServiceUsers;

namespace teamcare.data.Repositories
{
    public class HealthMedicationRepository : Repository<HealthMedication>, IHealthMedicationRepository
    {
        public HealthMedicationRepository(TeamcareDbContext dbContext, ClaimsPrincipal user) : base(dbContext, user)
        {
            
        }
    }
}