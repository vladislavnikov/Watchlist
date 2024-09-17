using Watchlist.Infrastructure.Data.Models;

namespace Watchlist.Core.Contracts
{
    public interface IMovieRepository
    {
        ICollection<Movie> GetAllMovies();
        ICollection<Movie> GetUserMovies(string userId);
        Movie GetMovie(int movieId);
        Task AddMovieToUserCollectionAsync(int movieId, string userId);
        Task RemoveMovieToUserCollectionAsync(int movieId, string userId);
        Task CreateMovieAsync(Movie model);
        Task UpdateMovieAsync(int movieId, Movie model);
        Task DeleteMovieAsync(int movieId);
        bool MovieExistsByTitle(string title);
    }
}
