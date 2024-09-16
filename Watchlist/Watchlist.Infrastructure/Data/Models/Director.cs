using System.ComponentModel.DataAnnotations;

namespace Watchlist.Infrastructure.Data.Models
{
    public class Director
    {
        public Director()
        {
            MediaItems = new List<MediaItem>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        public ICollection<MediaItem> MediaItems { get; set; }
    }
}
