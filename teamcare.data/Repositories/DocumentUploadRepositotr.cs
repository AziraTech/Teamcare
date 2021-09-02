using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using teamcare.data.Data;
using teamcare.data.Entities;
using teamcare.data.Entities.Documents;

namespace teamcare.data.Repositories
{
    public class DocumentUploadRepository : Repository<DocumentUpload>, IDocumentUploadRepository
    {
        public DocumentUploadRepository(TeamcareDbContext dbContext, ClaimsPrincipal user) : base(dbContext, user)
        {
        }
    }
}