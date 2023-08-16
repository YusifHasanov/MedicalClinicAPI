using DataAccess.Abstract;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class TherapyRepository : GenericRepository<Therapy>, ITherapyRepository
    {
        public TherapyRepository(DbEntity dbEntity) : base(dbEntity)
        {
        }
    }
}
