using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Repository.Interface;

namespace VGProducts.Business.Implementation
{
    public class ReviewBusiness : IReviewBusiness
    {
        private readonly IReviewRepository reviewRepository;

        public ReviewBusiness(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        public async Task<string> AddOrUpdateReview(AddReviewDto addReviewDto)
        {
            return await reviewRepository.AddOrUpdateReview(addReviewDto);
        }

        public async Task<List<ReviewDto>> GetAllReviewAsync()
        {
            return await reviewRepository.GetAllReviewAsync();
        }
    }
}
