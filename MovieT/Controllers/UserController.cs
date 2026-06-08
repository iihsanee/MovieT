using Microsoft.AspNetCore.Mvc;
using serviceLibary.Services;
using MovieT.ViewModels;
using System.Text.Json;
using DAL.Repositories;

namespace MovieT.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("ConnectionString 'DefaultConnection' not found");
            _userService = new UserService(new UserRepository(connectionString));
        }

        public IActionResult Index()
        {
            try
            {
                var gebruikersnaam = HttpContext.Session.GetString("Gebruiker");
                if (gebruikersnaam == null)
                    return RedirectToAction("Login");

                var user = _userService.GetByNaam(gebruikersnaam);
                if (user == null)
                    return RedirectToAction("Login");

                var watchingListJson = HttpContext.Session.GetString("WatchingList");
                var watchedListJson = HttpContext.Session.GetString("WatchedList");

                var watchingList = watchingListJson != null
                    ? JsonSerializer.Deserialize<List<WatchingListViewModel>>(watchingListJson) ?? new()
                    : new List<WatchingListViewModel>();

                var watchedList = watchedListJson != null
                    ? JsonSerializer.Deserialize<List<WatchedListViewModel>>(watchedListJson) ?? new()
                    : new List<WatchedListViewModel>();

                var viewModel = new UserViewModel
                {
                    Id = user.Id,
                    Gebruikersnaam = user.Gebruikersnaam,
                    WatchingList = watchingList,
                    WatchedList = watchedList
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                TempData["Foutmelding"] = "Er is een fout opgetreden bij het ophalen van je profiel.";
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult AanmeldFormulier()
        {
            return View("Index", new UserViewModel());
        }

        [HttpPost]
        public IActionResult AccountAanmaken(UserViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("Index", new UserViewModel());

            try
            {
                var fout = _userService.RegistreerGebruiker(viewModel.Gebruikersnaam, viewModel.Wachtwoord, viewModel.BevestigWachtwoord);
                if (fout != null)
                {
                    ModelState.AddModelError(string.Empty, fout);
                    return View("Index", new UserViewModel());
                }

                TempData["Melding"] = "Je account is aangemaakt! Log nu in.";
                return RedirectToAction("Login");
            }
            catch (Exception)
            {
                TempData["Foutmelding"] = "Er is een fout opgetreden bij het aanmaken van je account.";
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string gebruikersnaam, string wachtwoord)
        {
            var user = _userService.GetByNaam(gebruikersnaam);
            if (user == null)
            {
                TempData["Foutmelding"] = "Gebruikersnaam bestaat niet.";
                return View();
            }

            var loginResult = _userService.Login(gebruikersnaam, wachtwoord);
            if (!loginResult)
            {
                TempData["Foutmelding"] = "Wachtwoord is onjuist.";
                return View();
            }

            HttpContext.Session.SetString("Gebruiker", gebruikersnaam);
            return RedirectToAction("Index", "Home");
        }
    }
}