using Microsoft.AspNetCore.Mvc;

namespace MovieT.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Film");
        }
    }
}