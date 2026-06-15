using Interfaces.Interfaces;
using serviceLibary.Models;
using DAL.DTO;
namespace serviceLibary.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public UserModel? GetById(int id)
        {
            var dto = _repository.GetById(id);
            if (dto == null) return null;
            return MapUser(dto);
        }
        public UserModel? GetByEmail(string email)
        {
            var dto = _repository.GetByEmail(email);
            if (dto == null) return null;
            return MapUser(dto);
        }
        public bool EmailExists(string email)
        {
            return _repository.EmailExists(email);
        }
        public string? RegistreerGebruiker(string naam, string email, string wachtwoord, string bevestigWachtwoord)
        {
            if (wachtwoord != bevestigWachtwoord)
                return "De wachtwoorden komen niet overeen.";
            if (wachtwoord.Length < 8)
                return "Het wachtwoord moet minimaal 8 tekens bevatten.";
            if (_repository.EmailExists(email))
                return "Dit e-mailadres is al in gebruik.";
            _repository.AddUser(naam, email, wachtwoord);
            return null;
        }
        public bool Login(string email, string wachtwoord)
        {
            var dto = _repository.GetByEmail(email);
            if (dto == null) return false;
            return _repository.VerifyPassword(wachtwoord, dto.Wachtwoord);
        }
        private UserModel MapUser(UserDTO dto)
        {
            return new UserModel(
                id: dto.Id,
                gebruikersnaam: dto.Gebruikersnaam,
                wachtwoord: dto.Wachtwoord,
                email: dto.Email
            );
        }
    }
}