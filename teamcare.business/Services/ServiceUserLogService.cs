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

    public class ServiceUserLogService : BaseService, IServiceUserLogService
    {        
        private readonly IServiceUserLogRepository _repository;
        private readonly IMapper _mapper;

        public ServiceUserLogService(IAuditService auditService,
                                     IServiceUserLogRepository repository, 
                                     IMapper mapper) : base(auditService)
        {            
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceUserLogModel> AddAsync(ServiceUserLogModel model)
        {
            await RecordAuditEntry(new AuditModel { Action = "AddServiceUserLog", Details = "service call for add service user log", UserReference = "", CreatedBy = model.CreatedBy });

            var mapped = _mapper.Map<ServiceUserLogModel, ServiceUserLog>(model);
            var result = await _repository.AddAsync(mapped);
            return _mapper.Map<ServiceUserLog, ServiceUserLogModel>(result);
        }

        public async Task DeleteAsync(ServiceUserLogModel model)
        {
            await RecordAuditEntry(new AuditModel { Action = "DeleteServiceUserLog for" + model.Id, Details = "service call for delete service user log", UserReference = "", CreatedBy = model.CreatedBy });            
            var result = _mapper.Map<ServiceUserLogModel, ServiceUserLog>(model);
            await _repository.DeleteAsync(result);
        }

        public async Task<ServiceUserLogModel> GetByIdAsync(Guid id, ServiceUserLogModel model)
        {
            await RecordAuditEntry(new AuditModel { Action = "GetServiceUserLog for " + id, Details = "service call for get details service user log", UserReference = "", CreatedBy = model.CreatedBy });
            var result = await _repository.GetByIdAsync(id);
            return _mapper.Map<ServiceUserLog, ServiceUserLogModel>(result);
        }

        public async Task<IEnumerable<ServiceUserLogModel>> ListAllAsync(ServiceUserLogModel model)
        {
            await RecordAuditEntry(new AuditModel { Action = "GetAllServiceUserLog", Details = "service call for get all service user log", UserReference = "", CreatedBy = model.CreatedBy });

            var listresidence = await _repository.ListAllAsync();
            var mapperlist = _mapper.Map<IEnumerable<ServiceUserLog>, IEnumerable<ServiceUserLogModel>>(listresidence);
            mapperlist = mapperlist.OrderBy(r => r.CreatedOn).ToList();
            return mapperlist;
        }

        public async Task<ServiceUserLogModel> UpdateAsync(ServiceUserLogModel model)
        {
            await RecordAuditEntry(new AuditModel { Action = "UpdateServiceUserLog", Details = "service call for update service user log", UserReference = "", CreatedBy = model.CreatedBy });

            var mapped = _mapper.Map<ServiceUserLogModel, ServiceUserLog>(model);
            var result = await _repository.UpdateAsync(mapped);
            return _mapper.Map<ServiceUserLog, ServiceUserLogModel>(result);
        }
    }
}
