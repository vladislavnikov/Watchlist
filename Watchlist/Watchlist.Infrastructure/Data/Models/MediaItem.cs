using System.ComponentModel.DataAnnotations;

namespace Watchlist.Infrastructure.Data.Models
{
    public class MediaItem
    {
        public MediaItem()
        {
            Reviews = new List<Review>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [ValidReleaseYear]
        public int ReleaseYear { get; set; }

        public int DirectorId { get; set; }
        public Director Director { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
