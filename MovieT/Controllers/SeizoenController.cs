using Microsoft.AspNetCore.Mvc;
using serviceLibary.Services;
using MovieT.ViewModels;
using DAL.Repositories;

namespace MovieT.Controllers
{
    public class SeizoenController : Controller
    {
        private readonly SeizoenService _seizoenService;

        public SeizoenController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("ConnectionString 'DefaultConnection' not found");
            _seizoenService = new SeizoenService(new SeizoenRepository(connectionString));
        }

        public IActionResult Index(int serieId)
        {
            try
            {
                var seizoenen = _seizoenService.GetBySerieId(serieId);
                var viewModels = seizoenen.Select(s => new SeizoenViewModel
                {
                    Id = s.Id,
                    SerieId = s.SerieId,
                    Seizoennummer = s.Seizoennummer,
                    AantalAfleveringen = s.AantalAfleveringen,
                    Jaartal = s.Jaartal
                }).ToList();

                ViewBag.SerieId = serieId;
                return View(viewModels);
            }
            catch (Exception)
            {
                TempData["Foutmelding"] = "Er is een fout opgetreden bij het ophalen van de seizoenen.";
                return View("Error");
            }
        }

        public IActionResult Detail(int id)
        {
            try
            {
                var seizoen = _seizoenService.GetById(id);
                if (seizoen == null) return NotFound();

                var viewModel = new SeizoenViewModel
                {
                    Id = seizoen.Id,
                    SerieId = seizoen.SerieId,
                    Seizoennummer = seizoen.Seizoennummer,
                    AantalAfleveringen = seizoen.AantalAfleveringen,
                    Jaartal = seizoen.Jaartal
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                TempData["Foutmelding"] = "Er is een fout opgetreden bij het ophalen van het seizoen.";
                return View("Error");
            }
        }
    }
}