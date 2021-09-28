using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using teamcare.common.Helpers;
using teamcare.data.Data;
using teamcare.data.Entities;
using teamcare.data.Extensions;

namespace teamcare.data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly TeamcareDbContext _dbContext;
        private readonly ClaimsPrincipal _user;

        public Repository(TeamcareDbContext dbContext, ClaimsPrincipal user)
        {
            _dbContext = dbContext;
            _user = user;
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<List<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }


        public virtual async Task<T> AddAsync(T entity)
        {
            try
            {
                entity.CreatedBy = UserId;
                entity.CreatedOn = DateTimeOffset.UtcNow;
                _dbContext.Set<T>().Add(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            entity.UpdatedBy = UserId;
            entity.UpdatedOn = DateTimeOffset.UtcNow;
            _dbContext.DetachLocal(entity, entity.Id);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbContext.DetachLocal(entity, entity.Id);
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        private Guid? UserId
        {
            get
            {
                var userId = _user.GetClaimValue(common.ReferenceData.ClaimTypes.UserId);
                return userId == null ? (Guid?)null : new Guid(userId);
            }
        }

    }
}
