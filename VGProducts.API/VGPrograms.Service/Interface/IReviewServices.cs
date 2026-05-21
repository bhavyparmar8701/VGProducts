using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.DTOs;

namespace VGProducts.Service.Interface
{
    public interface IReviewServices
    {
        Task<string> AddOrUpdateReview( AddReviewDto addReviewDto);
        Task<List<ReviewDto>> GetAllReviewAsync();
    }
}
