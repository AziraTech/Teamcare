using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.data.Entities;
using teamcare.data.Repositories;

namespace teamcare.business.Services
{
	public class AuditService : IAuditService, IService<AuditModel>
	{
        private readonly IAuditRepository _auditRepository;
        private readonly IMapper _mapper;
		public AuditService(IAuditRepository auditRepository, IMapper mapper)
        {
            _auditRepository = auditRepository;
            _mapper = mapper;
        }
        
        public Task<AuditModel> GetByIdAsync(Guid id,AuditModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AuditModel>> ListAllAsync(AuditModel model)
        {
            var result = await _auditRepository.GetAllAudit();
            return _mapper.Map<List<Audit>, List<AuditModel>>(result);
        }

        public async Task<AuditModel> AddAsync(AuditModel model)
        {
            var mapped = _mapper.Map<AuditModel, Audit>(model);
            var result = await _auditRepository.AddAsync(mapped);
            return _mapper.Map<Audit, AuditModel>(result);
        }

        public Task<AuditModel> UpdateAsync(AuditModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(AuditModel model)
        {
            throw new NotImplementedException();
        }
    }
}