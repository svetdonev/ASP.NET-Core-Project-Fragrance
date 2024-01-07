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

        public IActionResult All([FromQuery]AllFragrancesQueryModel query)
        {
            var fragrancesQuery = this.data.Fragrances.AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.CategoryName))
            {
                fragrancesQuery = fragrancesQuery.Where(f => f.Category.Name == query.CategoryName);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                fragrancesQuery = fragrancesQuery.Where(f => f.Name.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            fragrancesQuery = query.Sorting switch
            {
                FragranceSorting.Year => fragrancesQuery.OrderByDescending(f => f.Year),
                FragranceSorting.Name => fragrancesQuery.OrderBy(f => f.Name),
                FragranceSorting.DateCreated or _ => fragrancesQuery.OrderByDescending(f => f.Id)
            };

            var fragrances = fragrancesQuery
                .Select(f => new FragranceListingViewModel
                {
                    Id = f.Id,
                    Name = f.Name,
                    ImageUrl = f.ImageUrl,
                    Year = f.Year,
                    Type = f.Type,
                    Category = f.Category.Name
                })
                .ToList();

            var categories = this.data
                .Fragrances
                .Select(f => f.Category.Name)
                .Distinct()
                .ToList();

            return View(new AllFragrancesQueryModel
            {
                Categories = categories,
                Fragrances = fragrances,
                SearchTerm = query.SearchTerm,
                Sorting = query.Sorting
            });
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
