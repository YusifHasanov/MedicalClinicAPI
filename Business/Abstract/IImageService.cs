
 
using Entities.Dto.Request.Create;
using Entities.Dto.Request.Update;
using Entities.Dto.Response;
using Entities.Entities;

namespace Business.Abstract
{
    public interface IImageService : IService<Image, UpdateImage,CreateImage,ImageResponse>
    {
    }
}
