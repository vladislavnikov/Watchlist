using Microsoft.EntityFrameworkCore;
using Watchlist.Core.Contracts;
using Watchlist.Infrastructure.Data.Models;

namespace Watchlist.Core.Repository
{
    public class ShowRepository : IShowRepository
    {
        private readonly ApplicationDbContext _context;
        public ShowRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddShowToUserCollectionAsync(int showId, string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            var movie = await _context.Shows.FindAsync(showId);

            _context.UserShows.Add(new UserShow { UserId = userId, ShowId = showId });
            await _context.SaveChangesAsync();
        }

        public async Task CreateShowAsync(Show model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteShowAsync(int showId)
        {
            var show = await _context.Shows.FirstOrDefaultAsync(s => s.Id == showId);
            _context.Shows.Remove(show);
            await _context.SaveChangesAsync();
        }

        public ICollection<Show> GetAllShows()
        {
            return _context.Shows.ToList();
        }

        public Show GetShow(int showId)
        {
            return _context.Shows.FirstOrDefault(m => m.Id == showId);
        }

        public ICollection<Show> GetUserShows(string userId)
        {
            return _context.UserShows
                .Where(um => um.UserId == userId)
                .Select(um => um.Show)
                .ToList();
        }

        public async Task RemoveShowToUserCollectionAsync(int showId, string userId)
        {
            var userShow = await _context.UserShows
                .FirstOrDefaultAsync(um => um.UserId == userId && um.ShowId == showId);

            _context.UserShows.Remove(userShow);
            await _context.SaveChangesAsync();
        }

        public bool ShowExistsByTitle(string title)
        {
            var show = _context.Shows.FirstOrDefault(m => m.Title == title);

            return show != null;
        }

        public async Task UpdateShowAsync(int showId, Show model)
        {
            var showToUpdate = GetShow(showId);

            showToUpdate.Title = model.Title;
            showToUpdate.Description = model.Description;
            showToUpdate.ReleaseYear = model.ReleaseYear;
            showToUpdate.ImageUrl = model.ImageUrl;
            showToUpdate.Seasons = model.Seasons;

            await _context.SaveChangesAsync();
        }
    }
}
