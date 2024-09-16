using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Watchlist.Infrastructure.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0, 10)]
        public int Rating { get; set; }

        [MaxLength(200)]
        public string? Comment { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int MediaItemId { get; set; }

        [ForeignKey("MediaItemId")]
        public MediaItem MediaItem { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
