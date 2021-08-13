using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using teamcare.data.Entities;

namespace teamcare.data.Repositories
{
	public interface IRepository<T> where T : BaseEntity
	{
        //Task<T> GetByIdAsync(ISpecification<T> spec);
        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> ListAllAsync();
        //Task<List<T>> ListAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        //Task<int> CountAsync(ISpecification<T> spec);
    }
}
