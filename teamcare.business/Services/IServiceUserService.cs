using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.data.Entities;

namespace teamcare.business.Services
{
    public interface IServiceUserService : IService<ServiceUserModel>
    {
        List<ServiceUserModel> ListAllSorted(int sortBy, List<ServiceUserModel> listOfUser);

        List<ServiceUserModel> ListAllFiltered(string filterBy, List<ServiceUserModel> listOfUser);
    }
}
