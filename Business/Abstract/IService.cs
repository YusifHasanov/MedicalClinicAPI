

using Core.Entities;
using Entities.Dto;

namespace Business.Abstract
{
    public interface IService<T,U,C,R> 
        where T : BaseEntity, new()  
        where U : BaseDto, new()
        where C : BaseDto, new()
        where R : BaseDto, new()

    { 
        public Task<IQueryable<R>> GetAll(); 
        public Task<R> GetById(int id); 
        public Task<T> DeleteAsync(int id);
        public Task<T> IsExistAsync(int id);
        public Task SaveChangesAsync();
        public Task<T> UpdateAsync(int id, U entity);
        public Task<T> AddAsync(C entity);
    }
}
