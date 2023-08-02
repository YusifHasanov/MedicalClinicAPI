 
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IRepository<T> where T : BaseEntity, new()
    {


        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter);
        public IQueryable<T> GetAll();
        public T  GetOne (Expression<Func<T, bool>> filter);
        public T GetById(int id);

        public void Update(T entity);

        public void Delete(int id); 
        public void Delete(T entity);
        public Task AddAsync(T entity);
        public Task SaveChangesAsync();
    }
}
