using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.data.Repositories;

namespace teamcare.business.Services
{
	public interface IBloodPressureReadingService : IService<BloodPressureReadingModel>
	{
        Task<IEnumerable<BloodPressureReadingModel>> ListAllSortedFiltered(Guid serviceuser, string daterange);

    }
}
