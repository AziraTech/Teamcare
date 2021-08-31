using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using teamcare.business.Models;

namespace teamcare.business.Services
{
	public interface IFileUploadService : IService<FileUploadModel>
    {
        Task<FileUploadModel> MoveBlobAsync(FileUploadModel model,Guid id);
        Task<byte[]> GetBlobAsync(FileUploadModel model,Guid id);
    }
}
