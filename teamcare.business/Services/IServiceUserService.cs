﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.data.Entities;

namespace teamcare.business.Services
{
    public interface IServiceUserService : IService<ServiceUserModel>
    {
        Task<IEnumerable<ServiceUserModel>> ListAllSortedFiltered(int sortBy, string filterBy);

        
    }
}
