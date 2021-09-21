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
            var mapperlist = _mapper.Map<IEnumerable<Residence>, IEnumerable<ResidenceModel>>(listresidence);
            mapperlist = mapperlist.OrderBy(r => r.Name).ToList();
            return mapperlist;
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

        public async Task DeleteAsync(ResidenceModel model)
        {
            var result = _mapper.Map<ResidenceModel, Residence>(model);
            await _repository.DeleteAsync(result);
        }
    }
}