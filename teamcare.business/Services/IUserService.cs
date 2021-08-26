using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using teamcare.business.Models;

namespace teamcare.business.Services
{
    public interface IUserService: IService<UserModel>
    {
        Task<Guid> GetUserGuidAsync(string PreferredUsername);
    }
}
