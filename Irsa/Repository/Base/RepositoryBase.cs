using Irsa.EFCore;
using Irsa.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Irsa.Repository.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext _repositoryContext { get; set; }
        private DbSet<T> _entity;

        public RepositoryBase(RepositoryContext RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
            _entity = _repositoryContext.Set<T>();
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _entity.ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            var obj = await _entity.FindAsync(id);
            if (obj != null)
                _repositoryContext.Entry(obj).State = EntityState.Detached;
            return obj;

        }
        public async Task<ICollection<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {

            return await _entity.Where(expression).AsNoTracking().ToListAsync();
        }

        public async Task<T> CreateAsync(T entity, bool save)
        {
            this._entity.Add(entity);
            //_repositoryContext.Entry(entity).State = EntityState.Added;
            if (save)
                await SaveAsync();
            return entity;

        }

        public async Task<T> UpdateAsync(T entity, bool save)
        {
            this._entity.Update(entity);
            //_repositoryContext.Entry(entity).State = EntityState.Modified;
            if (save)
                await SaveAsync();
            return entity;
        }

        public async Task<int> DeleteAsync(T entity)
        {
            this._entity.Remove(entity);
            return await SaveAsync();
        }




        public async Task<int> SaveAsync()
        {
            return await _repositoryContext.SaveChangesAsync();
        }
        public void Save()
        {
            _repositoryContext.SaveChanges();
        }
    }
}
