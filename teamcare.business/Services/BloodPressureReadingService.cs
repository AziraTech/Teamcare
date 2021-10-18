using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.data.Entities.ServiceUsers;
using teamcare.data.Repositories;

namespace teamcare.business.Services
{
    public class BloodPressureReadingService : IBloodPressureReadingService, IService<BloodPressureReadingModel>
    {
        private readonly IBloodPressureReadingRepository _BloodPressureReadingRepository;
        private readonly IMapper _mapper;

        public BloodPressureReadingService(IBloodPressureReadingRepository BloodPressureReadingRepository, IMapper mapper)
        {
            _BloodPressureReadingRepository = BloodPressureReadingRepository;
            _mapper = mapper;

        }

        public async Task<BloodPressureReadingModel> GetByIdAsync(Guid id)
        {
            var result = await _BloodPressureReadingRepository.GetByIdAsync(id);
            return _mapper.Map<BloodPressureReading, BloodPressureReadingModel>(result);
        }

        public async Task<IEnumerable<BloodPressureReadingModel>> ListAllAsync()
        {
            var result = await _BloodPressureReadingRepository.ListAllAsync();
            return _mapper.Map<List<BloodPressureReading>, List<BloodPressureReadingModel>>(result);
        }

        public async Task<BloodPressureReadingModel> AddAsync(BloodPressureReadingModel model)
        {
            var mapped = _mapper.Map<BloodPressureReadingModel, BloodPressureReading>(model);
            var result = await _BloodPressureReadingRepository.AddAsync(mapped);
            return _mapper.Map<BloodPressureReading, BloodPressureReadingModel>(result);
        }

        public async Task<BloodPressureReadingModel> UpdateAsync(BloodPressureReadingModel model)
        {
            var mapped = _mapper.Map<BloodPressureReadingModel, BloodPressureReading>(model);
            var result = await _BloodPressureReadingRepository.UpdateAsync(mapped);
            return _mapper.Map<BloodPressureReading, BloodPressureReadingModel>(result);
        }

        public async Task DeleteAsync(BloodPressureReadingModel model)
        {
            var result = _mapper.Map<BloodPressureReadingModel, BloodPressureReading>(model);
            await _BloodPressureReadingRepository.DeleteAsync(result);
        }
       

    }
}