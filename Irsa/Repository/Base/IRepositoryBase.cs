using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Irsa.Repository.Base
{
    public interface IRepositoryBase<T>
    {
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<ICollection<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
        Task<T> CreateAsync(T entity, bool save);
        Task<T> UpdateAsync(T entity, bool save);
        Task<int> DeleteAsync(T entity);
        Task<int> SaveAsync();
    }
}
