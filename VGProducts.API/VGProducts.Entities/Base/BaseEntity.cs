using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGProducts.Entities.Base
{
    public class BaseEntity : IBaseEntity
    {
        public Guid ID { get; set; }
    }
}
