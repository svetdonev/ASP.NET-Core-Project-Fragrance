using System.Collections.Generic;

namespace FragranceProject.Models.Fragrances
{
    public class AllFragrancesQueryModel
    {
        public string CategoryName { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public string SearchTerm { get; set; }
        public FragranceSorting Sorting { get; set; }
        public IEnumerable<FragranceListingViewModel> Fragrances { get; set; }
    }
}
