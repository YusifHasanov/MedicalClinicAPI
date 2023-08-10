using DataAccess.Abstract;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        public ImageRepository(DbEntity dbEntity) : base(dbEntity)
        {
        }
    }
}
