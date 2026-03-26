using Microsoft.AspNetCore.Mvc;
using MovieT.Repositories;

namespace MovieT.Controllers
{
    public class HomeController : Controller
    {
        private readonly FilmRepository _filmRepo;
        private readonly SerieRepository _serieRepo;
        private readonly GenreRepository _genreRepo;

        public HomeController(IConfiguration configuration)
        {
            string con = configuration.GetConnectionString("DefaultConnection")!;
            _filmRepo = new FilmRepository(con);
            _serieRepo = new SerieRepository(con);
            _genreRepo = new GenreRepository(con);
        }

        public IActionResult Index(string filter = "alles", string genre = "", string zoekterm = "")
        {
            var films = string.IsNullOrEmpty(genre)
                ? _filmRepo.GetAll()
                : _filmRepo.GetByGenre(genre);

            var series = string.IsNullOrEmpty(genre)
                ? _serieRepo.GetAll()
                : _serieRepo.GetByGenre(genre);

            // Zoekfunctie
            if (!string.IsNullOrEmpty(zoekterm))
            {
                films = films.Where(f => f.Titel.StartsWith(zoekterm, StringComparison.OrdinalIgnoreCase)
                    || f.Titel.Contains(zoekterm, StringComparison.OrdinalIgnoreCase)).ToList();
                series = series.Where(s => s.Titel.StartsWith(zoekterm, StringComparison.OrdinalIgnoreCase)
                    || s.Titel.Contains(zoekterm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            ViewBag.Genres = _genreRepo.GetAll();
            ViewBag.Filter = filter;
            ViewBag.Genre = genre;
            ViewBag.Zoekterm = zoekterm;
            ViewBag.Films = filter == "alles" || filter == "films" || filter == "genre" ? films : new List<MovieT.Models.Film>();
            ViewBag.Series = filter == "alles" || filter == "series" || filter == "genre" ? series : new List<MovieT.Models.Serie>();

            return View();
        }
    }
}