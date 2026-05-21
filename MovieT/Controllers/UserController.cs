using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using serviceLibary.Services;
using MovieT.ViewModels;
using System.Collections.Generic;
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
            var userRepo = new UserRepository(connectionString);
            _userService = new UserService(userRepo);
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
                    Gebruikersnaam = user.Naam,
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
                if (_userService.UsernameExists(viewModel.Gebruikersnaam))
                {
                    ModelState.AddModelError(string.Empty, "Deze gebruikersnaam is al in gebruik.");
                    return View("Index", new UserViewModel());
                }

                if (viewModel.Wachtwoord.Length < 8)
                {
                    ModelState.AddModelError(string.Empty, "Het wachtwoord moet minimaal 8 tekens bevatten.");
                    return View("Index", new UserViewModel());
                }

                if (viewModel.Wachtwoord != viewModel.BevestigWachtwoord)
                {
                    ModelState.AddModelError(string.Empty, "De wachtwoorden komen niet overeen.");
                    return View("Index", new UserViewModel());
                }

                _userService.RegistreerGebruiker(viewModel.Gebruikersnaam, viewModel.Wachtwoord);
                HttpContext.Session.SetString("Gebruiker", viewModel.Gebruikersnaam);
                return RedirectToAction("Index", "Home");
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

            if (user.Wachtwoord != wachtwoord)
            {
                TempData["Foutmelding"] = "Wachtwoord is onjuist.";
                return View();
            }

            HttpContext.Session.SetString("Gebruiker", gebruikersnaam);
            return RedirectToAction("Index", "Home");
        }
    }
}