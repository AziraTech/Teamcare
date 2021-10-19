﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using teamcare.business.Models;

namespace teamcare.business.Services
{
    public interface IDocumentUploadService : IService<DocumentUploadModel>
    {        
        Task<DocumentUploadModel> GetByContactIdAsync(Guid id);

        Task<DocumentUploadModel> GetByResidenceIdAsync(Guid id);
        Task<DocumentUploadModel> GetByServiceUserDocIdAsync(Guid id);
    }
}
