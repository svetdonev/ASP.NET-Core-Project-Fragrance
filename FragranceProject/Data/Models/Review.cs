using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace FragranceProject.Data.Models
{
    public class Review
    {
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string Content { get; set; }

        public DateTime CreatedOn { get; init; }

        [Required]
        public string AuthorId { get; set; }

        public User Author { get; set; }

        [Required]
        public string FragranceId { get; set; }

        public Fragrance Fragrance { get; set; }

        public ICollection<UserReview> UserReviews { get; set; } = new List<UserReview>();
    }
}
