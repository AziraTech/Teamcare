using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using teamcare.data.Data;
using teamcare.data.Entities;

namespace teamcare.data.Repositories
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(TeamcareDbContext dbContext, ClaimsPrincipal user) : base(dbContext, user)
		{
		}
    }
}

