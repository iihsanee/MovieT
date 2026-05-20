using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MovieT.ViewModels;
using System.Collections.Generic;
using System.Text.Json;

namespace MovieT.Controllers
{
    public class WatchedListController : Controller
    {
        private List<WatchedListViewModel> GetWatchedList()
        {
            var json = HttpContext.Session.GetString("WatchedList");

            if (json == null)
                return new List<WatchedListViewModel>();

            return JsonSerializer.Deserialize<List<WatchedListViewModel>>(json)
                   ?? new List<WatchedListViewModel>();
        }

        private void SaveWatchedList(List<WatchedListViewModel> list)
        {
            HttpContext.Session.SetString("WatchedList", JsonSerializer.Serialize(list));
        }

        public IActionResult Index()
        {
            try
            {
                var viewModels = GetWatchedList();
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
                    TempData["Foutmelding"] = "Je moet eerst een account aanmaken.";
                    return RedirectToAction("Index", "User");
                }

                var list = GetWatchedList();

                if (!list.Exists(x => x.FilmId == filmId && x.SerieId == serieId))
                {
                    list.Add(new WatchedListViewModel
                    {
                        FilmId = filmId,
                        SerieId = serieId,
                        Title = title,
                        Type = type
                    });

                    SaveWatchedList(list);
                }

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