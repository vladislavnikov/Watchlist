namespace Watchlist.Core.DTOs.Show
{
    public class CreateShowDto
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int DirectorId { get; set; }

        public int ReleaseYear { get; set; }

        public string ImageUrl { get; set; }

        public int Seasons { get; set; }
    }
}
