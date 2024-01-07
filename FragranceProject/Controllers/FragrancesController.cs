using FragranceProject.Data;
using FragranceProject.Data.Models;
using FragranceProject.Models.Fragrances;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            var totalFragrances = fragrancesQuery.Count();

            var fragrances = fragrancesQuery
                .Skip((query.CurrentPage - 1) * AllFragrancesQueryModel.FragrancesPerPage)
                .Take(AllFragrancesQueryModel.FragrancesPerPage)
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

            query.Fragrances = fragrances;
            query.Categories = categories;
            query.TotalFragrances = totalFragrances;


            return View(query);
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

        [HttpGet]
        public IActionResult Delete(int fragranceId)
        {
            var fragrance = this.data.Fragrances.FirstOrDefault(f => f.Id == fragranceId);

            if (fragrance == null)
            {
                return NotFound();
            }

            return View(new FragranceListingViewModel
            {
                Id = fragranceId,
            });
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int fragranceId)
        {
            var fragrance = this.data.Fragrances.FirstOrDefault(m => m.Id == fragranceId);

            if (fragrance == null)
            {
                return NotFound();
            }

            this.data.Fragrances.Remove(fragrance);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }
    }
}
