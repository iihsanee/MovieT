using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using serviceLibary.Services;
using MovieT.ViewModels;
using System.Collections.Generic;
using System.Linq;
using DAL.Repositories;

namespace MovieT.Controllers
{
    public class FilmController : Controller
    {
        private readonly FilmModel _filmService;
        private readonly GenreService _genreService;

        public FilmController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new System.Exception("ConnectionString 'DefaultConnection' not found");
            _filmService = new FilmModel(new FilmModelRepository(connectionString));
            _genreService = new GenreService(new GenreRepository(connectionString));
        }

        public IActionResult Index(int? genreId)
        {
            try
            {
                var films = _filmService.GetAll();
                var allGenres = _genreService.GetAll();
                var viewModels = new List<FilmModelViewModel>();

                foreach (var film in films)
                {
                    var genres = _genreService.GetByFilmId(film.Id);

                    if (genreId == null || genres.Contains(_genreService.GetById(genreId.Value)?.Naam ?? string.Empty))
                    {
                        if (!viewModels.Any(v => v.Id == film.Id))
                        {
                            viewModels.Add(new FilmModelViewModel
                            {
                                Id = film.Id,
                                Title = film.Title,
                                ReleaseDate = film.ReleaseDate,
                                Duration = film.Duration,
                                Description = film.Description,
                                Genres = genres
                            });
                        }
                    }
                }

                ViewBag.Genres = allGenres;
                ViewBag.SelectedGenre = genreId;

                return View(viewModels);
            }
            catch (Exception)
            {
                TempData["Foutmelding"] = "Er is een fout opgetreden bij het ophalen van de films.";
                return View("Error");
            }
        }

        public IActionResult Details(int id)
        {
            try
            {
                var film = _filmService.GetById(id);
                if (film == null)
                    return NotFound();

                var viewModel = new FilmModelViewModel
                {
                    Id = film.Id,
                    Title = film.Title,
                    ReleaseDate = film.ReleaseDate,
                    Duration = film.Duration,
                    Description = film.Description
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                TempData["Foutmelding"] = "Er is een fout opgetreden bij het ophalen van de filmdetails.";
                return View("Error");
            }
        }

        public IActionResult Search(string query)
        {
            try
            {
                var films = _filmService.Search(query);
                var allGenres = _genreService.GetAll();
                var viewModels = new List<FilmModelViewModel>();

                foreach (var film in films)
                {
                    var genres = _genreService.GetByFilmId(film.Id);

                    if (!viewModels.Any(v => v.Id == film.Id))
                    {
                        viewModels.Add(new FilmModelViewModel
                        {
                            Id = film.Id,
                            Title = film.Title,
                            ReleaseDate = film.ReleaseDate,
                            Duration = film.Duration,
                            Description = film.Description,
                            Genres = genres
                        });
                    }
                }

                ViewBag.Genres = allGenres;
                ViewBag.SelectedGenre = null;

                return View("Index", viewModels);
            }
            catch (Exception)
            {
                TempData["Foutmelding"] = "Er is een fout opgetreden bij het zoeken naar films.";
                return View("Error");
            }
        }
    }
}