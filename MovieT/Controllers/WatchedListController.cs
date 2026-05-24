using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieT.ViewModels;
using DAL.Repositories;

namespace MovieT.Controllers
{
    public class WatchedListController : Controller
    {
        private readonly WatchedListRepository _watchedListRepo;
        private readonly UserRepository _userRepo;

        public WatchedListController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("ConnectionString 'DefaultConnection' not found");
            _watchedListRepo = new WatchedListRepository(connectionString);
            _userRepo = new UserRepository(connectionString);
        }

        public IActionResult Index()
        {
            try
            {
                var gebruikersnaam = HttpContext.Session.GetString("Gebruiker");
                if (gebruikersnaam == null)
                    return RedirectToAction("Login", "User");

                var user = _userRepo.GetByNaam(gebruikersnaam);
                if (user == null)
                    return RedirectToAction("Login", "User");

                var list = _watchedListRepo.GetByUser(user.Id);
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
                var user = _userRepo.GetByNaam(gebruikersnaam!);
                if (user == null)
                    return RedirectToAction("Login", "User");

                _watchedListRepo.Add(user.Id, filmId, serieId);
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