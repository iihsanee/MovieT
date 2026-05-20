using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MovieT.ViewModels;
using System.Collections.Generic;
using System.Text.Json;

namespace MovieT.Controllers
{
    public class WatchingListController : Controller
    {
        private List<WatchingListViewModel> GetWatchingList()
        {
            var json = HttpContext.Session.GetString("WatchingList");

            if (json == null)
                return new List<WatchingListViewModel>();

            return JsonSerializer.Deserialize<List<WatchingListViewModel>>(json)
                   ?? new List<WatchingListViewModel>();
        }

        private void SaveWatchingList(List<WatchingListViewModel> list)
        {
            HttpContext.Session.SetString("WatchingList", JsonSerializer.Serialize(list));
        }

        public IActionResult Index()
        {
            try
            {
                var viewModels = GetWatchingList();
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
                    TempData["Foutmelding"] = "Je moet eerst een account aanmaken.";
                    return RedirectToAction("Index", "User");
                }

                var list = GetWatchingList();

                if (!list.Exists(x => x.FilmId == filmId))
                {
                    list.Add(new WatchingListViewModel
                    {
                        UserId = userId,
                        FilmId = filmId,
                        Title = title,
                        Type = "Film"
                    });

                    SaveWatchingList(list);
                }

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
                    TempData["Foutmelding"] = "Je moet eerst een account aanmaken.";
                    return RedirectToAction("Index", "User");
                }

                var list = GetWatchingList();

                if (!list.Exists(x => x.SerieId == serieId))
                {
                    list.Add(new WatchingListViewModel
                    {
                        UserId = userId,
                        SerieId = serieId,
                        Title = title,
                        Type = "Serie"
                    });

                    SaveWatchingList(list);
                }

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
                var list = GetWatchingList();

                if (filmId.HasValue)
                    list.RemoveAll(x => x.FilmId == filmId);

                else if (serieId.HasValue)
                    list.RemoveAll(x => x.SerieId == serieId);

                SaveWatchingList(list);

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