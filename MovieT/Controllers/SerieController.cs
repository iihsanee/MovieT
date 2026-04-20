using Microsoft.AspNetCore.Mvc;
using serviceLibary.Services;
using MovieT.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MovieT.Controllers
{
    public class SerieController : Controller
    {
        private readonly SerieService _serieService;
        private readonly GenreService _genreService;

        public SerieController(SerieService serieService, GenreService genreService)
        {
            _serieService = serieService;
            _genreService = genreService;
        }

        public IActionResult Index(int? genreId)
        {
            var series = _serieService.GetAll();
            var allGenres = _genreService.GetAll();
            var viewModels = new List<SerieViewModel>();
            foreach (var serie in series)
            {
                var genres = _genreService.GetBySerieId(serie.Id);
                if (genreId == null || genres.Contains(_genreService.GetById(genreId.Value)?.Naam))
                {
                    if (!viewModels.Any(v => v.Id == serie.Id))
                    {
                        viewModels.Add(new SerieViewModel
                        {
                            Id = serie.Id,
                            Title = serie.Title,
                            ReleaseDate = serie.ReleaseDate,
                            Duration = serie.Duration,
                            Description = serie.Description,
                            Genres = genres
                        });
                    }
                }
            }
            ViewBag.Genres = allGenres;
            ViewBag.SelectedGenre = genreId;
            return View(viewModels);
        }

        public IActionResult Details(int id)
        {
            var serie = _serieService.GetById(id);
            if (serie == null)
                return NotFound();
            var viewModel = new SerieViewModel
            {
                Id = serie.Id,
                Title = serie.Title,
                ReleaseDate = serie.ReleaseDate,
                Duration = serie.Duration,
                Description = serie.Description
            };
            return View(viewModel);
        }

        public IActionResult Search(string query)
        {
            var series = _serieService.Search(query);
            var allGenres = _genreService.GetAll();
            var viewModels = new List<SerieViewModel>();
            foreach (var serie in series)
            {
                var genres = _genreService.GetBySerieId(serie.Id);
                if (!viewModels.Any(v => v.Id == serie.Id))
                {
                    viewModels.Add(new SerieViewModel
                    {
                        Id = serie.Id,
                        Title = serie.Title,
                        ReleaseDate = serie.ReleaseDate,
                        Duration = serie.Duration,
                        Description = serie.Description,
                        Genres = genres
                    });
                }
            }
            ViewBag.Genres = allGenres;
            ViewBag.SelectedGenre = null;
            return View("Index", viewModels);
        }

        public IActionResult AddToWatchingList(int userId, int SerieId)
        {
            _serieService.AddToWatchingList(userId, SerieId);
            return RedirectToAction("Index");
        }

        public IActionResult AddToWatchedList(int userId, int SerieId)
        {
            _serieService.AddToWatchedList(userId, SerieId);
            return RedirectToAction("Index");
        }
    }
}