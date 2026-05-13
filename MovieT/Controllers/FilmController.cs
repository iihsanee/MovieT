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
        private readonly FilmModel _FilmModelService;
        private readonly GenreService _genreService;

        public FilmController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new System.Exception("ConnectionString 'DefaultConnection' not found");

            var filmRepo = new FilmModelRepository(connectionString);
            var genreRepo = new GenreRepository(connectionString);

            _FilmModelService = new FilmModel(filmRepo);
            _genreService = new GenreService(genreRepo);
        }

        public IActionResult Index(int? genreId)
        {
            try
            {
                var FilmModels = _FilmModelService.GetAll();
                var allGenres = _genreService.GetAll();
                var viewModels = new List<FilmModelViewModel>();

                foreach (var FilmModel in FilmModels)
                {
                    var genres = _genreService.GetByFilmId(FilmModel.Id);
                    var genreName = genreId == null ? null : _genreService.GetById(genreId.Value)?.Naam;

                    if (genreId == null || (genreName != null && genres.Contains(genreName)))
                    {
                        if (!viewModels.Any(v => v.Id == FilmModel.Id))
                        {
                            viewModels.Add(new FilmModelViewModel
                            {
                                Id = FilmModel.Id,
                                Title = FilmModel.Title,
                                ReleaseDate = FilmModel.ReleaseDate,
                                Duration = FilmModel.Duration,
                                Description = FilmModel.Description,
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
                TempData["Foutmelding"] = "Er is een fout opgetreden bij het ophalen van de films. Probeer het later opnieuw.";
                return View("Error");
            }
        }

        public IActionResult Details(int id)
        {
            try
            {
                var FilmModel = _FilmModelService.GetById(id);

                if (FilmModel == null)
                    return NotFound();

                var viewModel = new FilmModelViewModel
                {
                    Id = FilmModel.Id,
                    Title = FilmModel.Title,
                    ReleaseDate = FilmModel.ReleaseDate,
                    Duration = FilmModel.Duration,
                    Description = FilmModel.Description
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
                var FilmModels = _FilmModelService.Search(query);
                var allGenres = _genreService.GetAll();
                var viewModels = new List<FilmModelViewModel>();

                foreach (var FilmModel in FilmModels)
                {
                    var genres = _genreService.GetByFilmId(FilmModel.Id);

                    if (!viewModels.Any(v => v.Id == FilmModel.Id))
                    {
                        viewModels.Add(new FilmModelViewModel
                        {
                            Id = FilmModel.Id,
                            Title = FilmModel.Title,
                            ReleaseDate = FilmModel.ReleaseDate,
                            Duration = FilmModel.Duration,
                            Description = FilmModel.Description,
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

        public IActionResult AddToWatchingList(int userId, int FilmModelId)
        {
            try
            {
                _FilmModelService.AddToWatchingList(userId, FilmModelId);
                return RedirectToAction("Index", "Film");
            }
            catch (Exception)
            {
                TempData["Foutmelding"] = "Er is een fout opgetreden bij het toevoegen aan je watchinglist.";
                return View("Error");
            }
        }

        public IActionResult AddToWatchedList(int userId, int FilmModelId)
        {
            try
            {
                _FilmModelService.AddToWatchedList(userId, FilmModelId);
                return RedirectToAction("Index", "Film");
            }
            catch (Exception)
            {
                TempData["Foutmelding"] = "Er is een fout opgetreden bij het toevoegen aan je watchedlist.";
                return View("Error");
            }
        }
    }
}