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
            {
                return new List<WatchedListViewModel>();
            }

            return JsonSerializer.Deserialize<List<WatchedListViewModel>>(json) ?? new List<WatchedListViewModel>();
        }

        private void SaveWatchedList(List<WatchedListViewModel> list)
        {
            HttpContext.Session.SetString("WatchedList", JsonSerializer.Serialize(list));
        }

        public IActionResult Index()
        {
            var viewModels = GetWatchedList();
            return View(viewModels);
        }

        public IActionResult Add(int? filmId, int? serieId, string title, string type)
        {
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
    }
}