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
                Id = fragrance.Id,
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
                SearchTerm = query.SearchTerm
            });
        }

        [HttpGet]
        public IActionResult Delete(int fragranceId)
        {
            var fragrance = this.data.Fragrances.FirstOrDefault(f => f.Id == fragranceId);

            if(fragrance == null )
            {
                return NotFound();
            }

            return View(new FragranceListingViewModel { Id = fragranceId });
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int fragranceId)
        {
            var fragrance = this.data.Fragrances.FirstOrDefault(f => f.Id == fragranceId);

            if (fragrance == null)
            {
                return NotFound();
            }

            this.data.Fragrances.Remove(fragrance);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
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

        [HttpGet]
        public IActionResult Edit(int fragranceId)
        {
            var fragrance = this.data.Fragrances.FirstOrDefault(f => f.Id == fragranceId);

            if(fragrance == null)
            {
                return NotFound();
            }

            return View(new AddFragranceFormModel
            {
                Id = fragrance.Id,
                Name = fragrance.Name,
                Year = fragrance.Year,
                Description = fragrance.Description,
                Type = fragrance.Type,
                ImageUrl = fragrance.ImageUrl,
                CategoryId = fragrance.CategoryId
            });
        }

        [HttpPost]
        public IActionResult Edit(AddFragranceFormModel fragranceModel)
        {
            var fragrance = this.data.Fragrances.FirstOrDefault(f => f.Id == fragranceModel.Id);

            if (fragrance == null)
            {
                return NotFound();
            }

            if(!ModelState.IsValid)
            {
                return View(fragrance);
            }

            fragrance.Name = fragranceModel.Name;
            fragrance.Year = fragranceModel.Year;
            fragrance.Description = fragranceModel.Description;
            fragrance.Type = fragranceModel.Type;
            fragrance.ImageUrl = fragranceModel.ImageUrl;

            this.data.SaveChanges();

            return Redirect($"/Fragrances/Details?fragranceId={fragrance.Id}");
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
