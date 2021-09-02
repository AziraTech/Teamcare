using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using teamcare.data.Data;
using teamcare.data.Entities.ServiceUsers;

namespace teamcare.data.Repositories
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(TeamcareDbContext dbContext, ClaimsPrincipal user) : base(dbContext, user)
        {
        }

    }
}