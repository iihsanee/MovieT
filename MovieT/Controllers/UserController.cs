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
                var user = _userService.GetById(1);

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
                    Id = user?.Id ?? 1,
                    Gebruikersnaam = user?.Naam ?? "Gebruiker",
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
            return View("User", new UserViewModel());
        }

        [HttpPost]
        public IActionResult AccountAanmaken(UserViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("User", viewModel);

            try
            {
                var fout = _userService.RegistreerGebruiker(
                    viewModel.Gebruikersnaam,
                    viewModel.Wachtwoord,
                    viewModel.BevestigWachtwoord
                );

                if (fout != null)
                {
                    ModelState.AddModelError(string.Empty, fout);
                    return View("User", viewModel);
                }


                HttpContext.Session.SetString("Gebruiker", viewModel.Gebruikersnaam);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Foutmelding"] = "Er is een fout opgetreden bij het aanmaken van je account.";
                return View("Error");
            }
        }
    }
}