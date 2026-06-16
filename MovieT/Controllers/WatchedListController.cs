using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MovieT.ViewModels;
using DAL.Repositories;
using serviceLibary.Services;

namespace MovieT.Controllers
{
    public class WatchedListController : Controller
    {
        private readonly WatchedListService _watchedListService;
        private readonly UserService _userService;

        public WatchedListController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("ConnectionString 'DefaultConnection' not found");
            _watchedListService = new WatchedListService(new WatchedListRepository(connectionString));
            _userService = new UserService(new UserRepository(connectionString));
        }

        public IActionResult Index()
        {
            try
            {
                var email = HttpContext.Session.GetString("Gebruiker");
                if (email == null)
                    return RedirectToAction("Login", "User");

                var user = _userService.GetByEmail(email);
                if (user == null)
                    return RedirectToAction("Login", "User");

                var list = _watchedListService.GetByUser(user.Id);
                var viewModels = list.Select(x => new WatchedListViewModel
                {
                    FilmId = x.FilmId,
                    SerieId = x.SerieId,
                    Title = x.Title,
                    Type = x.Type
                }).ToList();

                return View(viewModels);
            }
            catch (Exception)
            {
                TempData["Foutmelding"] = "Er is een fout opgetreden bij het ophalen van je watchedlist.";
                return View("Error");
            }
        }

        public IActionResult Add(int? filmId, int? serieId, string title, string type)
        {
            try
            {
                if (HttpContext.Session.GetString("Gebruiker") == null)
                {
                    TempData["Foutmelding"] = "Je moet eerst inloggen of een account aanmaken.";
                    return RedirectToAction("Login", "User");
                }

                var email = HttpContext.Session.GetString("Gebruiker");
                var user = _userService.GetByEmail(email!);
                if (user == null)
                    return RedirectToAction("Login", "User");

                _watchedListService.Add(user.Id, filmId, serieId);
                return RedirectToAction("Index", "WatchingList");
            }
            catch (Exception)
            {
                TempData["Foutmelding"] = "Er is een fout opgetreden bij het toevoegen aan je watchedlist.";
                return View("Error");
            }
        }

        public IActionResult Remove(int? filmId, int? serieId)
        {
            try
            {
                var email = HttpContext.Session.GetString("Gebruiker");
                if (email == null)
                    return RedirectToAction("Login", "User");

                var user = _userService.GetByEmail(email);
                if (user == null)
                    return RedirectToAction("Login", "User");

                _watchedListService.Remove(user.Id, filmId, serieId);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Foutmelding"] = "Er is een fout opgetreden bij het verwijderen.";
                return View("Error");
            }
        }
    }
}