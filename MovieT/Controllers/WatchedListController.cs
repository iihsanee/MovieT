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
                var gebruikersnaam = HttpContext.Session.GetString("Gebruiker");
                if (gebruikersnaam == null)
                    return RedirectToAction("Login", "User");

                var user = _userService.GetByNaam(gebruikersnaam);
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

                var gebruikersnaam = HttpContext.Session.GetString("Gebruiker");
                var user = _userService.GetByNaam(gebruikersnaam!);
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
    }
}