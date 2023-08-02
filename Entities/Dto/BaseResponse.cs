using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class BaseResponse<T> 
        where T : BaseDto,new()
    {
        public bool HasError { get; set; }
        public string Message { get; set; }

        public T? Response { get; set; }

        public ICollection<T>? ResponseList { get; set; }
    }
}
