using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FragranceProject.Models.Fragrances
{
    public class AddFragranceFormModel
    {
        public string Name { get; init; }
        public int Milliliters { get; init; }
        public int Year { get; init; }
        public string Description { get; init; }
        public string Type { get; init; }

        [Display(Name="Image URL")]
        public string ImageUrl { get; init; }
        public int CategoryId { get; init; }
    }
}
