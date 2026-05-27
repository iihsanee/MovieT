using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using serviceLibary.Services;
using MovieT.ViewModels;
using System.Collections.Generic;
using System.Linq;
using DAL.Repositories;

namespace MovieT.Controllers
{
    public class TrendingController : Controller
    {
        private readonly FilmService _filmService;
        private readonly SerieService _serieService;

        public TrendingController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new System.Exception("ConnectionString 'DefaultConnection' not found");
            var filmRepo = new FilmModelRepository(connectionString);
            var serieRepo = new SerieRepository(connectionString);
            _filmService = new FilmService(filmRepo);
            _serieService = new SerieService(serieRepo);
        }

        public IActionResult Index()
        {
            try
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
            catch (Exception)
            {
                TempData["Foutmelding"] = "Er is een fout opgetreden bij het ophalen van de trending lijst.";
                return View("Error");
            }
        }
    }
}