
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCore.Models.Database.Repository.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByFieldAsync(string fieldName, object value);
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(int id);
    }
}
