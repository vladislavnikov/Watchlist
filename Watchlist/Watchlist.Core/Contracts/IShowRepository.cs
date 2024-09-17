using Watchlist.Infrastructure.Data.Models;

namespace Watchlist.Core.Contracts
{
    public interface IShowRepository
    {
        ICollection<Show> GetAllShows();
        ICollection<Show> GetUserShows(string userId);
        Show GetShow(int showId);
        Task AddShowToUserCollectionAsync(int showId, string userId);
        Task RemoveShowToUserCollectionAsync(int showId, string userId);
        Task CreateShowAsync(Show model);
        Task UpdateShowAsync(int showId, Show model);
        Task DeleteShowAsync(int showId);
        bool ShowExistsByTitle(string title);
    }
}
