namespace Watchlist.Infrastructure.Data.Models
{
    public class UserShow
    {
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        public int ShowId { get; set; } 
        public Show Show { get; set; } = null!;
    }
}
