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
            await RecordAuditEntry(new AuditModel { Action = "GetDocument for " + id, Details = "service call for get details document", UserReference = "" });

            var document = await _documentUploadRepository.GetByIdAsync(id);
            return _mapper.Map<DocumentUpload, DocumentUploadModel>(document);
        }

        public async Task<IEnumerable<DocumentUploadModel>> ListAllAsync()
        {
            await RecordAuditEntry(new AuditModel { Action = "GetAllDocuments", Details = "service call for get all document", UserReference = "" });

            var listUsers = await _documentUploadRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<DocumentUpload>, IEnumerable<DocumentUploadModel>>(listUsers);
        }

        public async Task<DocumentUploadModel> AddAsync(DocumentUploadModel model)
        {
            await RecordAuditEntry(new AuditModel { Action = "AddDocuments", Details = "service call for add document", UserReference = "" });

            var result = _mapper.Map<DocumentUploadModel, DocumentUpload>(model);
            var document = await _documentUploadRepository.AddAsync(result);
            return _mapper.Map<DocumentUpload, DocumentUploadModel>(document);
        }

        public async Task<DocumentUploadModel> UpdateAsync(DocumentUploadModel model)
        {
            await RecordAuditEntry(new AuditModel { Action = "UpdateDocuments", Details = "service call for update document", UserReference = "" });

            var result = _mapper.Map<DocumentUploadModel, DocumentUpload>(model);
            var document = await _documentUploadRepository.UpdateAsync(result);
            return _mapper.Map<DocumentUpload, DocumentUploadModel>(document);
        }

        public async Task DeleteAsync(DocumentUploadModel model)
        {
            await RecordAuditEntry(new AuditModel { Action = "DeleteDocuments for" + model.Id, Details = "service call for delete document", UserReference = "" });

            throw new NotImplementedException();
        }
    }
}