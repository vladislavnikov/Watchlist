using System.ComponentModel.DataAnnotations;
using Watchlist.Infrastructure.Data.Models.Enums;

namespace Watchlist.Infrastructure.Data.Models
{
    public class Movie : MediaItem
    {
        public Movie()
        {
            UserMovies = new List<UserMovie>();
        }
        public Genre Genre { get; set; }

        [Required]
        public int DurationMins { get; set; }

        public ICollection<UserMovie> UserMovies { get; set; }
    }
}
