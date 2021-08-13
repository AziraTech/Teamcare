using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teamcare.data.Data;
using teamcare.data.Entities;

namespace teamcare.data.Repositories
{
    public class ServiceUserRepository : Repository<ServiceUser>, IServiceUserRepository
    {
        public ServiceUserRepository(TeamcareDbContext dbContext) : base(dbContext)
        {
        }

    }
}