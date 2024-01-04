using FragranceProject.Data;
using FragranceProject.Data.Models;
using FragranceProject.Models.Fragrances;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FragranceProject.Controllers
{
    public class FragrancesController : Controller
    {
        private readonly FragranceDbContext data;
        public FragrancesController(FragranceDbContext data)
            => this.data = data;
        public IActionResult Add()
        {
            return View(new AddFragranceFormModel
            {
                Categories = this.GetFragranceCategories()
            });
        }

        [HttpPost]
        public IActionResult Add(AddFragranceFormModel fragrance)
        {
            if (!this.data.Categories.Any(f => f.Id == fragrance.CategoryId))
            {
                this.ModelState.AddModelError(nameof(fragrance.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                fragrance.Categories = this.GetFragranceCategories();

                return View(fragrance);
            }

            var fragranceData = new Fragrance
            {
                Name = fragrance.Name,
                Milliliters = fragrance.Milliliters,
                Year = fragrance.Year,
                Description = fragrance.Description,
                Type = fragrance.Type,
                ImageUrl = fragrance.ImageUrl,
                CategoryId = fragrance.CategoryId
            };

            this.data.Fragrances.Add(fragranceData);
            this.data.SaveChanges();

            return RedirectToAction("All");
        }

        public IActionResult All()
        {
            var fragrances = this.data
                .Fragrances
                .OrderByDescending(f => f.Id)
                .Select(f => new FragranceListingViewModel
                {
                    Id = f.Id,
                    Name = f.Name,
                    ImageUrl = f.ImageUrl,
                    Year = f.Year,
                    Milliliters = f.Milliliters,
                    Type = f.Type,
                    Category = f.Category.Name
                })
                .ToList();

            return View(fragrances);
        }

        public IActionResult Details(int fragranceId)
        {
            var fragrance = this.data.Fragrances.Include(f => f.Category).FirstOrDefault(f => f.Id == fragranceId);

            return View(new FragranceListingViewModel
            {
                Id = fragrance.Id,
                Name = fragrance.Name,
                ImageUrl = fragrance.ImageUrl,
                Description = fragrance.Description,
                Year = fragrance.Year,
                Milliliters = fragrance.Milliliters,
                Type = fragrance.Type,
                Category = fragrance.Category.Name,

            });
        }

        private IEnumerable<FragranceCategoryViewModel> GetFragranceCategories()
            => this.data.Categories
                .Select(f => new FragranceCategoryViewModel
                {
                    Id = f.Id,
                    Name = f.Name,
                })
                .ToList();
    }
}
