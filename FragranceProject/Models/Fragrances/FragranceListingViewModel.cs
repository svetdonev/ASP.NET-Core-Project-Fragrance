namespace FragranceProject.Models.Fragrances
{
    public class FragranceListingViewModel
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Year { get; set; }
        public int Milliliters { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
    }
}
