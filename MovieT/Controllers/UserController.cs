using Microsoft.AspNetCore.Mvc;
using serviceLibary.Services;
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
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string wachtwoord)
        {
            bool succes = _userService.Login(email, wachtwoord);
            if (!succes)
            {
                TempData["Fout"] = "Ongeldig e-mailadres of wachtwoord.";
                return View();
            }

            var user = _userService.GetByEmail(email);
            HttpContext.Session.SetInt32("UserId", user!.Id);
            HttpContext.Session.SetString("Gebruiker", user.Email);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string email, string wachtwoord, string bevestigWachtwoord)
        {
            string? fout = _userService.RegistreerGebruiker(email, wachtwoord, bevestigWachtwoord);
            if (fout != null)
            {
                TempData["Fout"] = fout;
                return View();
            }

            TempData["Succes"] = "Account aangemaakt! Je kunt nu inloggen.";
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
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