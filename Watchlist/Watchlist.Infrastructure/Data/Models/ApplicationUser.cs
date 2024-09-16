using Microsoft.AspNetCore.Identity;

namespace Watchlist.Infrastructure.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Reviews = new List<Review>();
            UserShows = new List<UserShow>();
            UserMovies = new List<UserMovie>();
        }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<UserShow> UserShows { get; set; }
        public ICollection<UserMovie> UserMovies { get; set; }
    }
}
