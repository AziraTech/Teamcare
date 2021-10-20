using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.data.Entities.ServiceUsers;
using teamcare.data.Repositories;

namespace teamcare.business.Services
{
    public class WeightReadingService : IWeightReadingService, IService<WeightReadingModel>
    {
        private readonly IWeightReadingRepository _weightReadingRepository;
        private readonly IMapper _mapper;

        public WeightReadingService(IWeightReadingRepository weightReadingRepository, IMapper mapper)
        {
            _weightReadingRepository = weightReadingRepository;
            _mapper = mapper;

        }

        public async Task<WeightReadingModel> GetByIdAsync(Guid id)
        {
            var result = await _weightReadingRepository.GetByIdAsync(id);
            return _mapper.Map<WeightReading, WeightReadingModel>(result);
        }

        public async Task<IEnumerable<WeightReadingModel>> ListAllAsync()
        {
            var result = await _weightReadingRepository.ListAllAsync();
            return _mapper.Map<List<WeightReading>, List<WeightReadingModel>>(result);
        }

        public async Task<WeightReadingModel> AddAsync(WeightReadingModel model)
        {
            var mapped = _mapper.Map<WeightReadingModel, WeightReading>(model);
            var result = await _weightReadingRepository.AddAsync(mapped);
            return _mapper.Map<WeightReading, WeightReadingModel>(result);
        }

        public async Task<WeightReadingModel> UpdateAsync(WeightReadingModel model)
        {
            var mapped = _mapper.Map<WeightReadingModel, WeightReading>(model);
            var result = await _weightReadingRepository.UpdateAsync(mapped);
            return _mapper.Map<WeightReading, WeightReadingModel>(result);
        }

        public async Task DeleteAsync(WeightReadingModel model)
        {
            var result = _mapper.Map<WeightReadingModel, WeightReading>(model);
            await _weightReadingRepository.DeleteAsync(result);
        }

        public async Task<IEnumerable<WeightReadingModel>> ListFiltered(Guid id, string daterange)
        {

            var bloodreadingdata = await ListAllAsync();

            var finaldata = bloodreadingdata.Where(x => x.ServiceUserId == id).OrderByDescending(x => x.TestDate).ToList();

            if (daterange != null)
            {
                string[] date = daterange.Split('-');
               
                    finaldata = finaldata.Where(r => r.TestDate.Date >= DateTime.ParseExact(date[0].Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date && r.TestDate.Date <= DateTime.ParseExact(date[1].Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date).ToList();
            }
            return finaldata;
        }

    }
}