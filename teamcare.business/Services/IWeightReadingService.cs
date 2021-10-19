using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.data.Repositories;

namespace teamcare.business.Services
{
	public interface IWeightReadingService : IService<WeightReadingModel>
	{
        Task<IEnumerable<WeightReadingModel>> ListFiltered(Guid serviceuser, string daterange);

    }
}
