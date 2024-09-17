using Watchlist.Infrastructure.Data.Models.Enums;

namespace Watchlist.Core.DTOs.Movie
{
    public class MovieDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string DirectorName { get; set; }

        public int ReleaseYear { get; set; }

        public int DurationMins { get; set; }

        public string ImageUrl { get; set; }

        public string Genre { get; set; }
    }
}
