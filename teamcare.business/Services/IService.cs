using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using teamcare.business.Models;

namespace teamcare.business.Services
{
    public interface IService<T> where T : BaseModel
    {
        Task<T> GetByIdAsync(Guid id,T model);
        Task<IEnumerable<T>> ListAllAsync(T model);
        Task<T> AddAsync(T model);
        Task<T> UpdateAsync(T model);
        Task DeleteAsync(T model);
    }
}
