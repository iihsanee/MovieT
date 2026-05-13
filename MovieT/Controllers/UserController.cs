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
                ?? throw new System.Exception("ConnectionString 'DefaultConnection' not found");
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
                    ? JsonSerializer.Deserialize<List<WatchingListViewModel>>(watchingListJson) ?? new List<WatchingListViewModel>()
                    : new List<WatchingListViewModel>();

                var watchedList = watchedListJson != null
                    ? JsonSerializer.Deserialize<List<WatchedListViewModel>>(watchedListJson) ?? new List<WatchedListViewModel>()
                    : new List<WatchedListViewModel>();

                var viewModel = new UserViewModel
                {
                    Id = user?.Id ?? 1,
                    Naam = user?.Naam ?? "Gebruiker",
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
    }
}