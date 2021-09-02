using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using teamcare.data.Data;
using teamcare.data.Entities;

namespace teamcare.data.Repositories
{
	public class AuditRepository : Repository<Audit>, IAuditRepository
	{
		private readonly TeamcareDbContext _context;
		public AuditRepository(TeamcareDbContext dbContext, ClaimsPrincipal user) : base(dbContext, user)
		{
			_context = dbContext;
		}
		public async Task<bool> CreateAuditRecord(Audit audit)
		{
			try
			{
				_context.Add(audit);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<List<Audit>> GetAllAudit()
		{
			return await _context.Audit.ToListAsync();
		}
	}
}
