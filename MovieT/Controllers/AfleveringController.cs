using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using serviceLibary.Services;
using MovieT.ViewModels;
using DAL.Repositories;

namespace MovieT.Controllers
{
    public class AfleveringController : Controller
    {
        private readonly AfleveringService _afleveringService;

        public AfleveringController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("ConnectionString 'DefaultConnection' not found");
            _afleveringService = new AfleveringService(new AfleveringRepository(connectionString));
        }

        public IActionResult Index(int seizoenId)
        {
            try
            {
                var afleveringen = _afleveringService.GetBySeizoenId(seizoenId);
                var viewModels = afleveringen.Select(a => new AfleveringViewModel
                {
                    Id = a.Id,
                    SeizoenId = a.SeizoenId,
                    Titel = a.Titel,
                    Afleveringsnummer = a.Afleveringsnummer,
                    Duurtijd = a.Duurtijd
                }).ToList();

                ViewBag.SeizoenId = seizoenId;
                return View(viewModels);
            }
            catch (Exception)
            {
                TempData["Foutmelding"] = "Er is een fout opgetreden bij het ophalen van de afleveringen.";
                return View("Error");
            }
        }

        public IActionResult Detail(int id)
        {
            try
            {
                var aflevering = _afleveringService.GetById(id);
                if (aflevering == null) return NotFound();

                var viewModel = new AfleveringViewModel
                {
                    Id = aflevering.Id,
                    SeizoenId = aflevering.SeizoenId,
                    Titel = aflevering.Titel,
                    Afleveringsnummer = aflevering.Afleveringsnummer,
                    Duurtijd = aflevering.Duurtijd
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                TempData["Foutmelding"] = "Er is een fout opgetreden bij het ophalen van de aflevering.";
                return View("Error");
            }
        }
    }
}