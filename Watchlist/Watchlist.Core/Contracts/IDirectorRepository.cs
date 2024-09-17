using Watchlist.Infrastructure.Data.Models;

namespace Watchlist.Core.Contracts
{
    public interface IDirectorRepository
    {
        ICollection<Director> GetAllDirectors();
        Director GetDirector(int directorId);
        bool DirectorExistsByName(string name);
        Task CreateDirectorAsync(Director director);
        Task UpdateDirectorAsync(int directorId, Director director);
        Task DeleteDirectorAsync(int directorId);
    }
}
