using Microsoft.AspNetCore.Mvc;
using serviceLibary.Services;
using MovieT.ViewModels;
using System.Collections.Generic;

namespace MovieT.Controllers
{
    public class TrendingController : Controller
    {
        private readonly FilmModel _filmService;
        private readonly SerieService _serieService;

        public TrendingController(FilmModel filmService, SerieService serieService)
        {
            _filmService = filmService;
            _serieService = serieService;
        }

        public IActionResult Index()
        {
            var trendingFilms = _filmService.GetTop10Trending();
            var trendingSeries = _serieService.GetTop10Trending();

            var viewModels = new List<TrendingViewModel>();

            foreach (var film in trendingFilms)
            {
                viewModels.Add(new TrendingViewModel
                {
                    Id = film.Id,
                    Title = film.Title,
                    Type = "Film"
                });
            }

            foreach (var serie in trendingSeries)
            {
                viewModels.Add(new TrendingViewModel
                {
                    Id = serie.Id,
                    Title = serie.Title,
                    Type = "Serie"
                });
            }

            viewModels = viewModels.Take(10).ToList();
            return View(viewModels);
        }
    }
}