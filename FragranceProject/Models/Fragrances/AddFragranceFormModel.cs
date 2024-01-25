using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static FragranceProject.Data.DataConstants;

namespace FragranceProject.Models.Fragrances
{
    public class AddFragranceFormModel
    {
        public string Id { get; set; }
        [Required]
        [StringLength(FragranceNameMaxLength, MinimumLength = FragranceNameMinLength, ErrorMessage = "The Name field must be between {2} and {1} characters!")]
        public string Name { get; init; }

        [Range(FragranceYearMinValue, FragranceYearMaxValue, ErrorMessage = "The Year field must be between {1} and {2} characters!")]
        public int Year { get; init; }

        [Required]
        [StringLength(FragranceDescriptionMaxLength, MinimumLength = FragranceDescriptionMinLength, ErrorMessage = "The Description field must be between {2} and {1} characters!")]
        public string Description { get; init; }

        [Required]
        [StringLength(FragranceTypeMaxLength, MinimumLength = FragranceTypeMinLength, ErrorMessage = "The Type field must be between {2} and {1} characters!")]
        public string Type { get; init; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }
        public IEnumerable<FragranceCategoryViewModel> Categories { get; set; }
    }
}