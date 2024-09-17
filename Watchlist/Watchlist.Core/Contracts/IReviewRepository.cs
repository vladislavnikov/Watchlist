using Watchlist.Infrastructure.Data.Models;

namespace Watchlist.Core.Contracts
{
    public interface IReviewRepository
    {
        Task<ICollection<Review>> GetMediaItemReviews(int mediaId);
        Task<ICollection<Review>> GetReviewsOfUser(string userId);
        Task CreateReviewAsync(Review model);
        Task UpdateReviewAsync(int reviewId, Review model);
        Task DeleteReviewAsync(int reviewId);
    }
}
