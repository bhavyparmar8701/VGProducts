using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGProducts.Entities.common
{
    public class PaginatedResponse<T>
    {
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage => (int)Math.Ceiling((double)TotalCount / PageSize);
        public List<T> Data { get; set; }
        public PaginatedResponse(List<T> data,int pageNumber,int pageSize,int count)
        {
            this.Data = data;   
            this.PageSize = pageSize;
            this.TotalCount = count;
            this.CurrentPage = pageNumber;
        }
    }
}
