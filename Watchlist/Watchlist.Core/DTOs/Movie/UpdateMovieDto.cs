using Watchlist.Infrastructure.Data.Models.Enums;

namespace Watchlist.Core.DTOs.Movie
{
    public class UpdateMovieDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int DirectorId { get; set; }

        public int ReleaseYear { get; set; }

        public int DurationMins { get; set; }

        public string ImageUrl { get; set; }

        public Genre Genre { get; set; }
    }
}
