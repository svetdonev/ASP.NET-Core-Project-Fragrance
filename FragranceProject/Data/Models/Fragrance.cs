﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static FragranceProject.Data.DataConstants;

namespace FragranceProject.Data.Models
{
    public class Fragrance
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(FragranceNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [MaxLength(FragranceDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(FragranceTypeMaxLength)]
        public string Type { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; init; }
        public ICollection<Review> Reviews { get; init; } = new List<Review>();
    }
}