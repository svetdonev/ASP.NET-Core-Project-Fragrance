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
            var viewModel = new IndexViewModel
            {
                FragrancesCount = data.Fragrances.Count(),
                //CommentsCount = data.Comments.Count(),
                //UsersCount= data.Users.Count(),
            };

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
