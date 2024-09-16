using System.ComponentModel.DataAnnotations;

namespace Watchlist.Infrastructure.Data.Models
{
    public class Show : MediaItem
    {
        public Show()
        {
            UserShows = new List<UserShow>();
        }

        [Required]
        public int Seasons { get; set; }

        public ICollection<UserShow> UserShows { get; set; }
    }
}
