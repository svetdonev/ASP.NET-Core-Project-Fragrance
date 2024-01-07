using FragranceProject.Data;
using FragranceProject.Models;
using FragranceProject.Models.Fragrances;
using FragranceProject.Models.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace FragranceProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly FragranceDbContext data;
        public HomeController(FragranceDbContext data)
        {
            this.data = data;
        }

        public IActionResult Index() 
        {
            var totalFragrances = this.data.Fragrances.Count();

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
                .Take(8)
                .ToList();

            return View(new IndexViewModel
            {
                FragrancesCount = totalFragrances,
                Fragrances = fragrances
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
