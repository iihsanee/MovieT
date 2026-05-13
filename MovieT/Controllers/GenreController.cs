using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using serviceLibary.Services;
using MovieT.ViewModels;
using System.Collections.Generic;
using DAL.Repositories;

namespace MovieT.Controllers
{
    public class GenreController : Controller
    {
        private readonly GenreService _genreService;

        public GenreController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new System.Exception("ConnectionString 'DefaultConnection' not found");
            var genreRepo = new GenreRepository(connectionString);
            _genreService = new GenreService(genreRepo);
        }

        public IActionResult Index()
        {
            try
            {
                var genres = _genreService.GetAll();
                var viewModels = new List<GenreViewModel>();

                foreach (var genre in genres)
                {
                    viewModels.Add(new GenreViewModel
                    {
                        Id = genre.Id,
                        Naam = genre.Naam
                    });
                }

                return View(viewModels);
            }
            catch (Exception)
            {
                TempData["Foutmelding"] = "Er is een fout opgetreden bij het ophalen van de genres.";
                return View("Error");
            }
        }
    }
}