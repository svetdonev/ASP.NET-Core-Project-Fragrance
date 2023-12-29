using System.Collections.Generic;

namespace FragranceProject.Data.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Fragrance> Fragrances { get; set; } = new List<Fragrance>();
    }
}
