using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Identity;

namespace FragranceProject.Data.Models
{
    public class User : IdentityUser
    {
        public string Avatar { get; set; }

        [MaxLength(25)]
        public string FirstName { get; set; }

        [MaxLength(25)]
        public string LastName { get; set; }

        [MaxLength(6)]
        public string Gender { get; set; }

        public DateTime RegisteredOn { get; init; } = DateTime.UtcNow;

        [MaxLength(300)]
        public string AboutMe { get; set; }

        public ICollection<Fragrance> Fragrances { get; set; } = new List<Fragrance>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        //public ICollection<UserMovie> UserMovies { get; set; } = new List<UserMovie>();
        public ICollection<UserReview> UserReviews { get; set; } = new List<UserReview>();
    }
}
