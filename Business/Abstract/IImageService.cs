
 
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Entities.Dto.Response;
using Entities.Entities;

namespace Business.Abstract
{
    public interface IImageService  
    {
        public IQueryable<ImageResponse> GetAll();
        public Task<ImageResponse> GetByIdAsync(int id);
        public Task<Image> DeleteAsync(int id);
        public Task<Image> UpdateAsync(int id, UpdateImage entity);
        public Task<Image> AddAsync(CreateImage entity);
    }
}
