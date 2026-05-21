using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGProducts.Entities.Base
{
    public interface IBaseEntity
    {
        public Guid ID { get; set; }
    }
}
