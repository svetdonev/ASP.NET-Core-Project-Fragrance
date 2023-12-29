using FragranceProject.Models.Fragrances;
using Microsoft.AspNetCore.Mvc;

namespace FragranceProject.Controllers
{
    public class FragrancesController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddFragranceFormModel fragrance)
        {
            return View();
        }
    }
}
