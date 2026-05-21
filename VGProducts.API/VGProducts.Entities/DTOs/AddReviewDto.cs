using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGProducts.Entities.DTOs
{
    public class AddReviewDto
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public decimal Reting { get; set; }
        public string Comment { get; set; }
    }
}
