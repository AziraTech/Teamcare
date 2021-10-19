using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using teamcare.business.Models;
using teamcare.data.Entities;
using teamcare.data.Entities.ServiceUsers;
using teamcare.data.Repositories;

namespace teamcare.business.Services
{
    public class ServiceUserDocumentService : BaseService, IServiceUserDocumentService
    {
        private readonly IMapper _mapper;
        private readonly IServiceUserRepository _serviceUserRepository;
        private readonly IServiceUserDocumentRepository _serviceUserDocumentRepository;
        private readonly IAuditService _auditService;
        private readonly IDocumentUploadService _documentUploadService;


        public ServiceUserDocumentService(IAuditService auditService, IMapper mapper,
                                          IServiceUserRepository serviceUserRepository,
                                          IDocumentUploadService documentUploadService,
                                          IServiceUserDocumentRepository serviceUserDocumentRepository) : base(auditService)
        {
            _mapper = mapper;
            _serviceUserRepository = serviceUserRepository;
            _serviceUserDocumentRepository = serviceUserDocumentRepository;
            _auditService = auditService;
            _documentUploadService = documentUploadService;
        }

        public async Task<ServiceUsersDocumentsModel> GetByIdAsync(Guid id)
        {
            var serviceUserDocument = await _serviceUserDocumentRepository.GetByIdAsync(id);   
            return _mapper.Map<ServiceUserDocument, ServiceUsersDocumentsModel>(serviceUserDocument);
        }

        public async Task<IEnumerable<ServiceUsersDocumentsModel>> ListAllAsync()
        {
            var listDocs = await _serviceUserDocumentRepository.ListAllAsync();            
            return _mapper.Map<IEnumerable<ServiceUserDocument>, IEnumerable<ServiceUsersDocumentsModel>>(listDocs);
        }

        
        public async Task<ServiceUsersDocumentsModel> AddAsync(ServiceUsersDocumentsModel model)
        {
            var result = _mapper.Map<ServiceUsersDocumentsModel, ServiceUserDocument>(model);
            var user = await _serviceUserDocumentRepository.AddAsync(result);
            return _mapper.Map<ServiceUserDocument, ServiceUsersDocumentsModel>(user);
        }

        public async Task<ServiceUsersDocumentsModel> UpdateAsync(ServiceUsersDocumentsModel model)
        {

            var result = _mapper.Map<ServiceUsersDocumentsModel, ServiceUserDocument>(model);
            var serviceUser = await _serviceUserDocumentRepository.UpdateAsync(result);
            return _mapper.Map<ServiceUserDocument, ServiceUsersDocumentsModel>(serviceUser);
        }

        public async Task DeleteAsync(ServiceUsersDocumentsModel model)
        {            
            var lstDocUpload = await _documentUploadService.GetByServiceUserDocIdAsync(new Guid(model.Id.ToString()));
            if (lstDocUpload != null) { await _documentUploadService.DeleteAsync(lstDocUpload); }
            var result = _mapper.Map<ServiceUsersDocumentsModel, ServiceUserDocument>(model);
            await _serviceUserDocumentRepository.DeleteAsync(result);
        }

        
    }
}