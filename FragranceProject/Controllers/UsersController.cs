using Microsoft.AspNetCore.Mvc;

namespace FragranceProject.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
