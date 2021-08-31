using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using teamcare.business.Models;

namespace teamcare.business.Services
{
    public interface IService<T> where T : BaseModel
    {
        Task<T> GetByIdAsync(Guid id,Guid uid);
        Task<IEnumerable<T>> ListAllAsync(Guid id);
        Task<T> AddAsync(T model,Guid id);
        Task<T> UpdateAsync(T model,Guid id);
        Task DeleteAsync(T model,Guid id);
    }
}
