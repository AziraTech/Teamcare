using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using teamcare.business.Models;
using teamcare.data.Entities;
using teamcare.data.Repositories;

namespace teamcare.business.Services
{
    public class ResidenceService : BaseService, IResidenceService
    {
        private readonly IMapper _mapper;
        private readonly IResidenceRepository _repository;

        public ResidenceService(IMapper mapper, IAuditService auditService, IResidenceRepository repository) : base(auditService)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ResidenceModel> GetByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);
            return _mapper.Map<Residence, ResidenceModel>(result);
        }

        public async Task<IEnumerable<ResidenceModel>> ListAllAsync()
        {
            var listresidence = await _repository.ListAllAsync();
            //return result.Select(i => _mapper.Map<Residence, ResidenceModel>(i));
            return _mapper.Map<IEnumerable<Residence>, IEnumerable<ResidenceModel>>(listresidence);

        }

        public async Task<ResidenceModel> AddAsync(ResidenceModel model)
        {
            var mapped = _mapper.Map<ResidenceModel, Residence>(model);
            var result = await _repository.AddAsync(mapped);
            return _mapper.Map<Residence, ResidenceModel>(result);
        }

        public async Task<ResidenceModel> UpdateAsync(ResidenceModel model)
        {
            var mapped = _mapper.Map<ResidenceModel, Residence>(model);
            var result = await _repository.UpdateAsync(mapped);
            return _mapper.Map<Residence, ResidenceModel>(result);
        }

        public Task DeleteAsync(ResidenceModel entity)
        {
            throw new NotImplementedException();
        }
    }
}