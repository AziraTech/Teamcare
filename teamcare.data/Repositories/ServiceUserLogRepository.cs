using System;
using System.Collections.Generic;
using System.Text;
using teamcare.data.Data;
using teamcare.data.Entities.ServiceUsers;
using teamcare.data.Repositories;

namespace teamcare.data.Repositories
{
    public class ServiceUserLogRepository : Repository<ServiceUserLog>, IServiceUserLogRepository
    {
        public ServiceUserLogRepository(TeamcareDbContext context) : base(context)
        {

        }
    }
}

