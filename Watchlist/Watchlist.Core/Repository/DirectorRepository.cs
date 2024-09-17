using Microsoft.EntityFrameworkCore;
using Watchlist.Core.Contracts;
using Watchlist.Infrastructure.Data.Models;

namespace Watchlist.Core.Repository
{
    public class DirectorRepository : IDirectorRepository
    {
        private readonly ApplicationDbContext _context;
        public DirectorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateDirectorAsync(Director director)
        {
            _context.Add(director);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDirectorAsync(int directorId)
        {
            var director = await _context.Directors.FirstOrDefaultAsync(d => d.Id == directorId);

            _context.Directors.Remove(director);
            await _context.SaveChangesAsync();
        }

        public bool DirectorExistsByName(string name)
        {
            var director = _context.Directors.FirstOrDefault(d => d.Name == name);

            return director != null;
        }

        public Director GetDirector(int directorId)
        {
            return _context.Directors.FirstOrDefault(d => d.Id == directorId);
        }

        public ICollection<Director> GetAllDirectors()
        {
            return _context.Directors.ToList();
        }

        public async Task UpdateDirectorAsync(int directorId, Director director)
        {
            var directorToUpdate = GetDirector(directorId);

            directorToUpdate.Name = director.Name;

            await _context.SaveChangesAsync();
        }
    }
}
