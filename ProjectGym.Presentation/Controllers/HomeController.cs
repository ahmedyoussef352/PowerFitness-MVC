using Microsoft.AspNetCore.Mvc;

namespace ProjectGym.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Home";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
