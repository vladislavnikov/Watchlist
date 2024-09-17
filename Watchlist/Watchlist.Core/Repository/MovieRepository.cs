using Microsoft.EntityFrameworkCore;
using Watchlist.Core.Contracts;
using Watchlist.Infrastructure.Data.Models;

namespace Watchlist.Core.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;
        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddMovieToUserCollectionAsync(int movieId, string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            var movie = await _context.Movies.FindAsync(movieId);

            _context.UserMovies.Add(new UserMovie { UserId = userId, MovieId = movieId });
            await _context.SaveChangesAsync();
        }

        public async Task CreateMovieAsync(Movie model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovieAsync(int movieId)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        public ICollection<Movie> GetAllMovies()
        {
            return _context.Movies.ToList();
        }

        public Movie GetMovie(int movieId)
        {
            return _context.Movies.FirstOrDefault(m => m.Id == movieId);
        }

        public ICollection<Movie> GetUserMovies(string userId)
        {
            return _context.UserMovies
                .Where(um => um.UserId == userId)
                .Select(um => um.Movie)
                .ToList();
        }

        public bool MovieExistsByTitle(string title)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Title == title);

            return movie != null;
        }

        public async Task RemoveMovieToUserCollectionAsync(int movieId, string userId)
        {
            var userMovie = await _context.UserMovies
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId);

            _context.UserMovies.Remove(userMovie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMovieAsync(int movieId, Movie model)
        {
            var movieToUpdate = GetMovie(movieId);

            movieToUpdate.Title = model.Title;
            movieToUpdate.Description = model.Description;
            movieToUpdate.ReleaseYear = model.ReleaseYear;
            movieToUpdate.ImageUrl = model.ImageUrl;
            movieToUpdate.Genre = model.Genre;

            await _context.SaveChangesAsync();
        }
    }
}
