using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using serviceLibary.Services;
using MovieT.ViewModels;
using System.Collections.Generic;
using System.Text.Json;

namespace MovieT.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var user = _userService.GetById(1);
            var watchingListJson = HttpContext.Session.GetString("WatchingList");
            var watchedListJson = HttpContext.Session.GetString("WatchedList");

            var viewModel = new UserViewModel
            {
                Id = user?.Id ?? 1,
                Naam = user?.Naam ?? "Gebruiker",
                WatchingList = watchingListJson != null ? JsonSerializer.Deserialize<List<WatchingListViewModel>>(watchingListJson) : new List<WatchingListViewModel>(),
                WatchedList = watchedListJson != null ? JsonSerializer.Deserialize<List<WatchedListViewModel>>(watchedListJson) : new List<WatchedListViewModel>()
            };

            return View(viewModel);
        }
    }
}