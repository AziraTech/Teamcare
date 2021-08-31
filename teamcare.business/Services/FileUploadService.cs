using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
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

        public Task<FileUploadModel> GetByIdAsync(Guid id, Guid uid)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FileUploadModel>> ListAllAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<FileUploadModel> AddAsync(FileUploadModel model, Guid id)
        {
            await RecordAuditEntry(new AuditModel { Action = "AddFile", Details = "service call for add file", UserReference = "",CreatedBy=id });

            try
            {
                var containerClient = new BlobContainerClient(_azureStorageOptions.ConnectionString, _azureStorageOptions.Container);
                
                await containerClient.CreateIfNotExistsAsync();
                model.BlobName ??= Guid.NewGuid().ToString();
                var fileExtension = Path.GetExtension(model.FileName);
                var blobName = $"/{FolderNames.Temporary}/{model.BlobName}{fileExtension}";
                var uploadResponse = await containerClient.UploadBlobAsync(blobName, BinaryData.FromBytes(model.DocumentBytes));

                var blobClient = containerClient.GetBlobClient(blobName);
                var blobHttpHeaders = new BlobHttpHeaders { ContentType = model.ContentType };
                blobClient.SetHttpHeaders(blobHttpHeaders);

                var document = await _documentUploadRepository.AddAsync(new DocumentUpload
                {
                    FileSizeInBytes = model.FileSizeInBytes,
                    BlobName = blobName,
                    FileName = model.FileName,
                    IsTemporary = model.IsTemporary,
                    FileExtension = fileExtension,
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

        public async Task<FileUploadModel> UpdateAsync(FileUploadModel model, Guid id)
        {
            return await AddAsync(model,id);
        }

        public Task DeleteAsync(FileUploadModel model,Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<FileUploadModel> MoveBlobAsync(FileUploadModel model, Guid id)
        {
            await RecordAuditEntry(new AuditModel { Action = "MoveBlobFile for "+ model.BlobName, Details = "service call for move blob file", UserReference = "",CreatedBy=id });

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

        public async Task<byte[]> GetBlobAsync(FileUploadModel model, Guid id)
        {
            await RecordAuditEntry(new AuditModel { Action = "GetBlob for " + model.BlobName, Details = "service call for get blob", UserReference = "",CreatedBy=id });

            try
            {
                var destinationFolder = string.IsNullOrWhiteSpace(model.DestinationFolder) ? string.Empty : $"{model.DestinationFolder}/";

                BlobServiceClient blobServiceClient = new BlobServiceClient(_azureStorageOptions.ConnectionString);
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(model.BlobName.Split("/").Last());
                BlobClient blob = containerClient.GetBlobClient(model.BlobName);
                if (await blob.ExistsAsync())
                {
                    BlobDownloadInfo blobdata = await blob.DownloadAsync();
                    // Create random data to write to the file.
                    byte[] dataArray = new byte[10000000];
                    new Random().NextBytes(dataArray);

                    using (FileStream fileStream = new FileStream(model.FileName, FileMode.Create))
                    {
                        await blobdata.Content.CopyToAsync(fileStream);
                        for (int i = 0; i < dataArray.Length; i++) { fileStream.WriteByte(dataArray[i]); }
                        fileStream.Seek(0, SeekOrigin.Begin);
                        for (int i = 0; i < fileStream.Length; i++) { if (dataArray[i] != fileStream.ReadByte()) { } }
                    }
                    return dataArray;
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}
