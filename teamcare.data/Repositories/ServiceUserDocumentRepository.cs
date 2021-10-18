using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using teamcare.data.Data;
using teamcare.data.Entities;
using teamcare.data.Entities.ServiceUsers;

namespace teamcare.data.Repositories
{
    public class ServiceUserDocumentRepository : Repository<ServiceUserDocument>, IServiceUserDocumentRepository
    {
        public ServiceUserDocumentRepository(TeamcareDbContext dbContext, ClaimsPrincipal user) : base(dbContext, user)
        {
        }

    }
}