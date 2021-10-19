using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using teamcare.business.Models;
using teamcare.common.Configuration;
using teamcare.data.Entities;
using teamcare.data.Entities.Documents;
using teamcare.data.Repositories;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Linq;

namespace teamcare.business.Services
{
    public class DocumentUploadService : BaseService, IDocumentUploadService
    {
        private readonly IMapper _mapper;
        private readonly IDocumentUploadRepository _documentUploadRepository;
        private readonly IAuditService _auditService;

        public DocumentUploadService(IAuditService auditService, IMapper mapper,
            IDocumentUploadRepository documentUploadRepository) : base(auditService)
        {
            _mapper = mapper;
            _documentUploadRepository = documentUploadRepository;
            _auditService = auditService;
        }

        public async Task<DocumentUploadModel> GetByIdAsync(Guid id)
        {

            var document = await _documentUploadRepository.GetByIdAsync(id);
            return _mapper.Map<DocumentUpload, DocumentUploadModel>(document);
        }

        public async Task<DocumentUploadModel> GetByContactIdAsync(Guid id)
        {
            var documents = await _documentUploadRepository.ListAllAsync();
            var document = documents.ToList().FirstOrDefault(x => x.DocumentType == 1 && x.ContactId == id);
            return _mapper.Map<DocumentUpload, DocumentUploadModel>(document);
        }
        public async Task<DocumentUploadModel> GetByResidenceIdAsync(Guid id)
        {
            var documents = await _documentUploadRepository.ListAllAsync();
            var document = documents.ToList().FirstOrDefault(x => x.DocumentType == 1 && x.ResidenceId == id);
            return _mapper.Map<DocumentUpload, DocumentUploadModel>(document);
        }

        public async Task<DocumentUploadModel> GetByServiceUserDocIdAsync(Guid id)
        {
            var documents = await _documentUploadRepository.ListAllAsync();
            var document = documents.ToList().FirstOrDefault(x => x.DocumentType == 1 && x.ServiceUserDocumentId == id);
            return _mapper.Map<DocumentUpload, DocumentUploadModel>(document);
        }

        
        public async Task<IEnumerable<DocumentUploadModel>> ListAllAsync()
        {
            var listUsers = await _documentUploadRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<DocumentUpload>, IEnumerable<DocumentUploadModel>>(listUsers);
        }

        public async Task<DocumentUploadModel> AddAsync(DocumentUploadModel model)
        {
            var result = _mapper.Map<DocumentUploadModel, DocumentUpload>(model);
            var document = await _documentUploadRepository.AddAsync(result);
            return _mapper.Map<DocumentUpload, DocumentUploadModel>(document);
        }

        public async Task<DocumentUploadModel> UpdateAsync(DocumentUploadModel model)
        {
            var result = _mapper.Map<DocumentUploadModel, DocumentUpload>(model);
            var document = await _documentUploadRepository.UpdateAsync(result);
            return _mapper.Map<DocumentUpload, DocumentUploadModel>(document);
        }

        public async Task DeleteAsync(DocumentUploadModel model)
        {
            var result = _mapper.Map<DocumentUploadModel, DocumentUpload>(model);
            await _documentUploadRepository.DeleteAsync(result);            
        }
    }
}