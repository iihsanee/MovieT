using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;

using serviceLibary.Services;

using MovieT.ViewModels;

using System.Collections.Generic;

using System.Linq;

using DAL.Repositories;

namespace MovieT.Controllers

{

    public class SerieController : Controller

    {

        private readonly SerieService _serieService;

        private readonly GenreService _genreService;

        private readonly SeizoenService _seizoenService;

        private readonly AfleveringService _afleveringService;

        public SerieController(IConfiguration configuration)

        {

            var connectionString = configuration.GetConnectionString("DefaultConnection")

                ?? throw new System.Exception("ConnectionString 'DefaultConnection' not found");

            _serieService = new SerieService(new SerieRepository(connectionString));

            _genreService = new GenreService(new GenreRepository(connectionString));

            _seizoenService = new SeizoenService(new SeizoenRepository(connectionString));

            _afleveringService = new AfleveringService(new AfleveringRepository(connectionString));

        }

        public IActionResult Index(int? genreId)

        {

            try

            {

                var series = _serieService.GetAll();

                var allGenres = _genreService.GetAll();

                var viewModels = new List<SerieViewModel>();

                foreach (var serie in series)

                {

                    var genres = _genreService.GetBySerieId(serie.Id);

                    if (genreId == null || genres.Contains(_genreService.GetById(genreId.Value)?.Naam ?? string.Empty))

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

            catch (Exception)

            {

                TempData["Foutmelding"] = "Er is een fout opgetreden bij het ophalen van de series.";

                return View("Error");

            }

        }

        public IActionResult Details(int id)

        {

            try

            {

                var serie = _serieService.GetById(id);

                if (serie == null)

                    return NotFound();

                var seizoenen = _seizoenService.GetBySerieId(id);

                var seizoenViewModels = seizoenen.Select(s =>

                {

                    var afleveringen = _afleveringService.GetBySeizoenId(s.Id);

                    return new SeizoenViewModel

                    {

                        Id = s.Id,

                        SerieId = s.SerieId,

                        Seizoennummer = s.Seizoennummer,

                        AantalAfleveringen = s.AantalAfleveringen,

                        Jaartal = s.Jaartal,

                        Afleveringen = afleveringen.Select(a => new AfleveringViewModel

                        {

                            Id = a.Id,

                            SeizoenId = a.SeizoenId,

                            Titel = a.Titel,

                            Afleveringsnummer = a.Afleveringsnummer,

                            Duurtijd = a.Duurtijd

                        }).ToList()

                    };

                }).ToList();

                var viewModel = new SerieViewModel

                {

                    Id = serie.Id,

                    Title = serie.Title,

                    ReleaseDate = serie.ReleaseDate,

                    Duration = serie.Duration,

                    Description = serie.Description,

                    Seizoenen = seizoenViewModels

                };

                return View(viewModel);

            }

            catch (Exception)

            {

                TempData["Foutmelding"] = "Er is een fout opgetreden bij het ophalen van de seriedetails.";

                return View("Error");

            }

        }

        public IActionResult Search(string query)

        {

            try

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

            catch (Exception)

            {

                TempData["Foutmelding"] = "Er is een fout opgetreden bij het zoeken naar series.";

                return View("Error");

            }

        }

        public IActionResult AddToWatchingList(int userId, int SerieId)

        {

            try

            {

                _serieService.AddToWatchingList(userId, SerieId);

                return RedirectToAction("Index");

            }

            catch (Exception)

            {

                TempData["Foutmelding"] = "Er is een fout opgetreden bij het toevoegen aan je watchinglist.";

                return View("Error");

            }

        }

        public IActionResult AddToWatchedList(int userId, int SerieId)

        {

            try

            {

                _serieService.AddToWatchedList(userId, SerieId);

                return RedirectToAction("Index");

            }

            catch (Exception)

            {

                TempData["Foutmelding"] = "Er is een fout opgetreden bij het toevoegen aan je watchedlist.";

                return View("Error");

            }

        }

    }

}
