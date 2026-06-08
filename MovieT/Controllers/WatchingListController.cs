using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MovieT.ViewModels;
using DAL.Repositories;
using serviceLibary.Services;

namespace MovieT.Controllers
{
    public class WatchingListController : Controller
    {
        private readonly WatchingListService _watchingListService;
        private readonly UserService _userService;

        public WatchingListController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("ConnectionString 'DefaultConnection' not found");
            _watchingListService = new WatchingListService(new WatchingListRepository(connectionString));
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

                var list = _watchingListService.GetByUser(user.Id);
                var viewModels = list.Select(x => new WatchingListViewModel
                {
                    UserId = x.UserId,
                    FilmId = x.FilmId,
                    SerieId = x.SerieId,
                    Title = x.Title,
                    Type = x.Type
                }).ToList();

                return View(viewModels);
            }
            catch (Exception)
            {
                TempData["Foutmelding"] = "Er is een fout opgetreden bij het ophalen van je watchinglist.";
                return View("Error");
            }
        }

        public IActionResult AddFilm(int userId, int filmId, string title)
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

                _watchingListService.Add(user.Id, filmId, null);
                return RedirectToAction("Index", "Film");
            }
            catch (Exception)
            {
                TempData["Foutmelding"] = "Er is een fout opgetreden bij het toevoegen van de film aan je watchinglist.";
                return View("Error");
            }
        }

        public IActionResult AddSerie(int userId, int serieId, string title)
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

                _watchingListService.Add(user.Id, null, serieId);
                return RedirectToAction("Index", "Serie");
            }
            catch (Exception)
            {
                TempData["Foutmelding"] = "Er is een fout opgetreden bij het toevoegen van de serie aan je watchinglist.";
                return View("Error");
            }
        }

        public IActionResult MoveToWatched(int? filmId, int? serieId, string title, string type)
        {
            try
            {
                var gebruikersnaam = HttpContext.Session.GetString("Gebruiker");
                if (gebruikersnaam == null)
                    return RedirectToAction("Login", "User");

                var user = _userService.GetByNaam(gebruikersnaam);
                if (user == null)
                    return RedirectToAction("Login", "User");


                return RedirectToAction("Add", "WatchedList", new
                {
                    filmId = filmId,
                    serieId = serieId,
                    title = title,
                    type = type
                });
            }
            catch (Exception)
            {
                TempData["Foutmelding"] = "Er is een fout opgetreden bij het verplaatsen naar je watchedlist.";
                return View("Error");
            }
        }
    }
}