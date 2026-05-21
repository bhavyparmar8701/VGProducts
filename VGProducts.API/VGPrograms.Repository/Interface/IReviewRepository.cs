using VGProducts.Entities.DTOs;

namespace VGProducts.Repository.Interface
{
    public interface IReviewRepository
    {
        Task<string> AddOrUpdateReview( AddReviewDto dto);
        Task<List<ReviewDto>> GetAllReviewAsync();
    }
}
