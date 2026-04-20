using Microsoft.AspNetCore.Mvc;
using serviceLibary.Services;
using MovieT.ViewModels;
using System.Collections.Generic;

namespace MovieT.Controllers
{
    public class GenreController : Controller
    {
        private readonly GenreService _genreService;

        public GenreController(GenreService genreService)
        {
            _genreService = genreService;
        }

        public IActionResult Index()
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
    }
}
