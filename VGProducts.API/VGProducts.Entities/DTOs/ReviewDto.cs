using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.Base;

namespace VGProducts.Entities.DTOs
{
    public class ReviewDto : BaseDataEntity
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public decimal Rating { get; set; }
        public string Comment { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
    }
}
