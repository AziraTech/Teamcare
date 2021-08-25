using System;
using System.Collections.Generic;
using System.Text;
using teamcare.data.Data;
using teamcare.data.Entities.Users;

namespace teamcare.data.Repositories
{
    public class FavouriteServiceUserRepository : Repository<FavouriteServiceUser>, IFavouriteServiceUserRepository
    {
        public FavouriteServiceUserRepository(TeamcareDbContext context) : base(context)
        {
        }
    }
}
