using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Service.Interface;

namespace VGProducts.Service.Implementation
{
    public class ReviewServices : IReviewServices
    {
        private readonly IReviewBusiness reviewBusiness;

        public ReviewServices(IReviewBusiness reviewBusiness)
        {
            this.reviewBusiness = reviewBusiness;
        }

        public async Task<string> AddOrUpdateReview(AddReviewDto addReviewDto)
        {
            return await reviewBusiness.AddOrUpdateReview(addReviewDto);
        }

        public async Task<List<ReviewDto>> GetAllReviewAsync()
        {
            return await reviewBusiness.GetAllReviewAsync();
        }
    }
}
