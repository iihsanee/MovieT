using Microsoft.AspNetCore.Mvc;
using serviceLibary.Services;
using servicelibrary.Services;
using MovieT.ViewModels;
using System.Text.Json;
using DAL.Repositories;

namespace MovieT.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly WachtwoordResetService _wachtwoordResetService;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
            string connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("ConnectionString niet gevonden");

            var userRepository = new UserRepository(connectionString);
            var wachtwoordResetRepository = new WachtwoordResetRepository(connectionString);

            _userService = new UserService(userRepository);
            _wachtwoordResetService = new WachtwoordResetService(wachtwoordResetRepository, userRepository);
            _emailService = new EmailService(configuration);
        }

        
        [HttpGet]
        public IActionResult WachtwoordVergeten()
        {
            return View();
        }

        [HttpPost]
        public IActionResult WachtwoordVergeten(string email)
        {
            string? fout = _wachtwoordResetService.VraagResetAan(email);
            if (fout != null)
            {
                TempData["Fout"] = fout;
                return View();
            }

            // Token ophalen om de URL mee te bouwen
            var resetModel = _wachtwoordResetService.GetByEmail(email);
            if (resetModel != null)
            {
                string resetUrl = Url.Action("WachtwoordResetten", "User",
                    new { token = resetModel.ResetToken }, Request.Scheme)!;

                _emailService.StuurResetEmail(email, resetModel.ResetToken, resetUrl);
            }

            TempData["Succes"] = "Als dit e-mailadres bekend is, ontvang je een reset link.";
            return RedirectToAction("Login");
        }

        // STAP 2 — Gebruiker klikt op link in email
        [HttpGet]
        public IActionResult WachtwoordResetten(string token)
        {
            string? fout = _wachtwoordResetService.ValideerToken(token);
            if (fout != null)
            {
                TempData["Fout"] = fout;
                return RedirectToAction("Login");
            }

            ViewBag.Token = token;
            return View();
        }

        [HttpPost]
        public IActionResult WachtwoordResetten(string token, string nieuwWachtwoord, string bevestigWachtwoord)
        {
            string? fout = _wachtwoordResetService.ValideerToken(token);
            if (fout != null)
            {
                TempData["Fout"] = fout;
                return RedirectToAction("Login");
            }

            if (nieuwWachtwoord != bevestigWachtwoord)
            {
                TempData["Fout"] = "De wachtwoorden komen niet overeen.";
                ViewBag.Token = token;
                return View();
            }

            if (nieuwWachtwoord.Length < 8)
            {
                TempData["Fout"] = "Het wachtwoord moet minimaal 8 tekens bevatten.";
                ViewBag.Token = token;
                return View();
            }

            _wachtwoordResetService.ResetWachtwoord(token, nieuwWachtwoord);
            TempData["Succes"] = "Je wachtwoord is succesvol gewijzigd. Je kunt nu inloggen.";
            return RedirectToAction("Login");
        }
    }
}