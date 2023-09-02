
 
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Entities.Dto.Response;
using Entities.Entities;

namespace Business.Abstract
{
    public interface IImageService  
    {
 
 
        public Task<Image> DeleteAsync(int id);
 
        public Task<Image> AddAsync(CreateImage entity);
    }
}
