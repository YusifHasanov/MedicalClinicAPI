using Entities.Entities;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace DataAccess.Concrete
{
    public abstract class GenericRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        protected readonly DbEntity dbEntity;

        public GenericRepository(DbEntity dbEntity)
        {
            this.dbEntity = dbEntity;
        }
        public DbSet<T> Table => dbEntity.Set<T>();

        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public void Delete(int id)
        {
            Table.Where(e => e.Id == id).ExecuteDelete();
        }

        public void Delete(T entity)
        {
            dbEntity.Remove(entity);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter)
        {
            return Table.Where(filter);
        }

        public IQueryable<T> GetAll()
        {
            return Table;
        }

        public T GetById(int id)
        {
            return Table.Find(id);
        }

        public T GetOne(Expression<Func<T, bool>> filter)
        {
            return Table.FirstOrDefault(filter);
        }


        public async Task SaveChangesAsync()
        {
            await dbEntity.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            Table.Update(entity);
        }

        public async Task Dispose()
        {
            await dbEntity.DisposeAsync();
        }


    }
}
