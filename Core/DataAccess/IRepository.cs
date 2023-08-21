
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter, bool track = false);
        public IQueryable<T> GetAll(bool track = false);
        public Task<T> GetOneAsync(Expression<Func<T, bool>> filter);
        public Task<T> GetByIdAsync(int id);
        public void Update(T entity);

        public void Delete(int id);
        public void Delete(T entity);
        public Task AddAsync(T entity);
        public Task SaveChangesAsync();
    }
}
