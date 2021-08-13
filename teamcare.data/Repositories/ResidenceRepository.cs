using teamcare.data.Data;
using teamcare.data.Entities;

namespace teamcare.data.Repositories
{
    public class ResidenceRepository : Repository<Residence>, IResidenceRepository
    {
        public ResidenceRepository(TeamcareDbContext context) : base(context)
        {
            
        }
    }
}