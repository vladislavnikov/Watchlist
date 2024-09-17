using Microsoft.EntityFrameworkCore;
using Watchlist.Core.Contracts;
using Watchlist.Infrastructure.Data.Models;

namespace Watchlist.Core.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext context;
        public MovieRepository(ApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task AddMovieToUserCollectionAsync(int movieId, string userId)
        {
            var user = await context.Users.FindAsync(userId);
            var movie = await context.Movies.FindAsync(movieId);

            context.UserMovies.Add(new UserMovie { UserId = userId, MovieId = movieId });
            await context.SaveChangesAsync();
        }

        public async Task CreateMovieAsync(Movie model)
        {
            context.Add(model);
            await context.SaveChangesAsync();
        }

        public async Task DeleteMovieAsync(int movieId)
        {
            var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);

            context.Movies.Remove(movie);
            await context.SaveChangesAsync();
        }

        public ICollection<Movie> GetAllMovies()
        {
            return context.Movies.ToList();
        }

        public Movie GetMovie(int movieId)
        {
            return context.Movies.FirstOrDefault(m => m.Id == movieId);
        }

        public ICollection<Movie> GetUserMovies(string userId)
        {
            return context.UserMovies
                .Where(um => um.UserId == userId)
                .Select(um => um.Movie)
                .ToList();
        }

        public bool MovieExistsByTitle(string title)
        {
            var movie = context.Movies.FirstOrDefault(m => m.Title == title);

            return movie != null;
        }

        public async Task RemoveMovieToUserCollectionAsync(int movieId, string userId)
        {
            var userMovie = await context.UserMovies
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId);

            context.UserMovies.Remove(userMovie);
            await context.SaveChangesAsync();
        }

        public async Task UpdateMovieAsync(int movieId, Movie model)
        {
            var movieToUpdate = GetMovie(movieId);

            movieToUpdate.Title = model.Title;
            movieToUpdate.Description = model.Description;
            movieToUpdate.ReleaseYear = model.ReleaseYear;
            movieToUpdate.ImageUrl = model.ImageUrl;
            movieToUpdate.Genre = model.Genre;

            await context.SaveChangesAsync();
        }
    }
}
