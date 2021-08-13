using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.common.Configuration;
using teamcare.common.ReferenceData;
using teamcare.data.Entities.Documents;
using teamcare.data.Repositories;

namespace teamcare.business.Services
{
	public class FileUploadService : BaseService, IFileUploadService
	{
        private readonly AzureStorageSettings _azureStorageOptions;
        private readonly IMapper _mapper;
        private readonly IDocumentUploadRepository _documentUploadRepository;
        private readonly IAuditService _auditService;

        public FileUploadService(IAuditService auditService, IMapper mapper,
            IDocumentUploadRepository documentUploadRepository, IOptions<AzureStorageSettings> azureStorageOptions) : base(auditService)
        {
            _azureStorageOptions = azureStorageOptions.Value;
            _mapper = mapper;
            _documentUploadRepository = documentUploadRepository;
            _auditService = auditService;
        }

        public Task<FileUploadModel> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FileUploadModel>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<FileUploadModel> AddAsync(FileUploadModel model)
        {
            try
            {
                var containerClient = new BlobContainerClient(_azureStorageOptions.ConnectionString, _azureStorageOptions.Container);
                await containerClient.CreateIfNotExistsAsync();
                model.BlobName ??= Guid.NewGuid().ToString();
                var blobName = $"/{FolderNames.Temporary}/{model.BlobName}";
                var uploadResponse = await containerClient.UploadBlobAsync(blobName, BinaryData.FromBytes(model.DocumentBytes));

                var document = await _documentUploadRepository.AddAsync(new DocumentUpload
                {
                    FileSizeInBytes = model.FileSizeInBytes,
                    BlobName = blobName,
                    FileName = model.FileName,
                    IsTemporary = model.IsTemporary,
                    FileExtension = Path.GetExtension(model.FileName),
                    ContentType = model.ContentType
                });

                model.Id = document.Id;
                model.DocumentBytes = null;

                return model;
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        public async Task<FileUploadModel> UpdateAsync(FileUploadModel model)
        {
            return await AddAsync(model);
        }

        public Task DeleteAsync(FileUploadModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<FileUploadModel> MoveBlobAsync(FileUploadModel model)
        {
            try
            {
                var containerClient = new BlobContainerClient(_azureStorageOptions.ConnectionString, _azureStorageOptions.Container);
                var sourceBlob = containerClient.GetBlobClient(model.BlobName);

                if (sourceBlob.Exists())
                {
                    if (await sourceBlob.ExistsAsync())
                    {
                        var destinationFolder = string.IsNullOrWhiteSpace(model.DestinationFolder)
                            ? string.Empty
                            : $"{model.DestinationFolder}/";
                        var destBlob = containerClient.GetBlobClient($"/{destinationFolder}{model.BlobName.Split("/").Last()}");

                        // Start the copy operation.
                        await destBlob.StartCopyFromUriAsync(sourceBlob.Uri);
                        model.BlobName = destBlob.Name;

                        return model;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}
