namespace Watchlist.Infrastructure.Data.Models
{
    public class UserMovie
    {
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; }

        public int MovieId { get; set; } 
        public Movie Movie { get; set; } = null!;
    }
}
