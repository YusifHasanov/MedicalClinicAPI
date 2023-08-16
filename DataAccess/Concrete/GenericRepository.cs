
using Core.DataAccess;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace  DataAccess.Concrete
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

        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter, bool track)
        {
            return track ? Table.Where(filter) : Table.Where(filter).AsNoTracking();
        }

 
        public IQueryable<T> GetAll(bool track)
        {
            return track ? Table : Table.AsNoTracking();
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
