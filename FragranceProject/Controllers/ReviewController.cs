using FragranceProject.Models.Fragrances;
using Microsoft.AspNetCore.Mvc;

namespace FragranceProject.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult Create(FragranceListingViewModel fragrance)
        {
            return View();
        }
    }
}
