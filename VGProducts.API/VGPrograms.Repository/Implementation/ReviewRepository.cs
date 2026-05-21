using Microsoft.EntityFrameworkCore;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Model;
using VGProducts.Repository.DataAccess;
using VGProducts.Repository.Interface;

namespace VGProducts.Repository.Implementation
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ReviewRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<string> AddOrUpdateReview(AddReviewDto addReviewDto)
        {
            var product = await applicationDbContext.Product.FirstOrDefaultAsync(p => p.ProductId == addReviewDto.ProductId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            var existingReview = await applicationDbContext.Review.FirstOrDefaultAsync(r => r.ProductId == addReviewDto.ProductId && r.UserId == addReviewDto.UserId);
            if (existingReview == null)
            {
                var review = new Review
                {
                    ProductId = addReviewDto.ProductId,
                    UserId = addReviewDto.UserId,
                    Rating = addReviewDto.Reting,
                    Comment = addReviewDto.Comment,
                    CreatedAt = DateTime.UtcNow, 
                    IsDeleted = false
                };
                await applicationDbContext.Review.AddAsync(review);
                product.Reting = ((product.Reting * product.ReviewCount) + addReviewDto.Reting) / (product.ReviewCount + 1);
                product.ReviewCount += 1;
            }
            else
            {
                var oldreview = existingReview.Rating;

                existingReview.Rating = addReviewDto.Reting;
                existingReview.Comment = addReviewDto.Comment;

                product.Reting = ((product.Reting * product.ReviewCount) - oldreview + addReviewDto.Reting) / product.ReviewCount;
            }
            await applicationDbContext.SaveChangesAsync();
            return "Review added successfully";
        }

        public async Task<List<ReviewDto>> GetAllReviewAsync()
        {
            var review = await applicationDbContext.Review.Include(r => r.User).ToListAsync();
            return review.Select(r => new ReviewDto
            {
                ProductId = r.ProductId,
                UserId = r.UserId,
                Rating = r.Rating,
                Comment = r.Comment,
                FirstName = r.User.FirstName,
                LastName = r.User.LastName
            }).ToList();
        }
    }
}
