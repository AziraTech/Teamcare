using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.data.Entities.ServiceUsers;
using teamcare.data.Repositories;

namespace teamcare.business.Services
{
    public class HealthMedicationService : IHealthMedicationService, IService<HealthMedicationModel>
    {
        private readonly IHealthMedicationRepository _HealthMedicationRepository;
        private readonly IMapper _mapper;

        public HealthMedicationService(IHealthMedicationRepository HealthMedicationRepository, IMapper mapper)
        {
            _HealthMedicationRepository = HealthMedicationRepository;
            _mapper = mapper;

        }

        public async Task<HealthMedicationModel> GetByIdAsync(Guid id)
        {
            var result = await _HealthMedicationRepository.GetByIdAsync(id);
            return _mapper.Map<HealthMedication, HealthMedicationModel>(result);
        }

        public async Task<IEnumerable<HealthMedicationModel>> ListAllAsync()
        {
            var result = await _HealthMedicationRepository.ListAllAsync();
            return _mapper.Map<List<HealthMedication>, List<HealthMedicationModel>>(result);
        }

        public async Task<HealthMedicationModel> AddAsync(HealthMedicationModel model)
        {
            var mapped = _mapper.Map<HealthMedicationModel, HealthMedication>(model);
            var result = await _HealthMedicationRepository.AddAsync(mapped);
            return _mapper.Map<HealthMedication, HealthMedicationModel>(result);
        }

        public async Task<HealthMedicationModel> UpdateAsync(HealthMedicationModel model)
        {
            var mapped = _mapper.Map<HealthMedicationModel, HealthMedication>(model);
            var result = await _HealthMedicationRepository.UpdateAsync(mapped);
            return _mapper.Map<HealthMedication, HealthMedicationModel>(result);
        }

        public async Task DeleteAsync(HealthMedicationModel model)
        {
            var result = _mapper.Map<HealthMedicationModel, HealthMedication>(model);
            await _HealthMedicationRepository.DeleteAsync(result);
        }
       

    }
}