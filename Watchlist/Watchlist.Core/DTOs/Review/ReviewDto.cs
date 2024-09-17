using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Watchlist.Infrastructure.Data.Models;

namespace Watchlist.Core.DTOs.Review
{
    public class ReviewDto
    {
        public int Id { get; set; }

        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; }

        public int MediaItemId { get; set; }

        public MediaItem MediaItem { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
