using Microsoft.EntityFrameworkCore;
using Watchlist.Core.Contracts;
using Watchlist.Infrastructure.Data.Models;

namespace Watchlist.Core.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext context;
        public ReviewRepository(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public async Task CreateReviewAsync(Review model)
        {
            await context.Reviews.AddAsync(model);
            await context.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(int reviewId)
        {
            var review = await context.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId);

            context.Reviews.Remove(review);
            await context.SaveChangesAsync();

        }

        public async Task<ICollection<Review>> GetMediaItemReviews(int mediaId)
        {
            return await context.Reviews
               .Where(r => r.MediaItemId == mediaId)
               .ToListAsync();
        }

        public async Task<ICollection<Review>> GetReviewsOfUser(string userId)
        {
            return await context.Reviews
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task UpdateReviewAsync(int reviewId, Review model)
        {
            var existingReview = await context.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId);

            existingReview.Rating = model.Rating;
            existingReview.Comment = model.Comment;

            context.Reviews.Update(existingReview);
            await context.SaveChangesAsync();

        }
    }
}
