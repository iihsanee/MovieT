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

        public UserModel? GetByNaam(string naam)
        {
            var dto = _repository.GetByNaam(naam);
            if (dto == null) return null;
            return MapUser(dto);
        }

        public bool UsernameExists(string naam)
        {
            return _repository.UsernameExists(naam);
        }

        public string? RegistreerGebruiker(string naam, string wachtwoord, string bevestigWachtwoord)
        {
            if (wachtwoord != bevestigWachtwoord)
                return "De wachtwoorden komen niet overeen.";
            if (wachtwoord.Length < 8)
                return "Het wachtwoord moet minimaal 8 tekens bevatten.";
            if (_repository.UsernameExists(naam))
                return "Deze gebruikersnaam is al in gebruik.";
            _repository.AddUser(naam, wachtwoord);
            return null;
        }

        public bool Login(string naam, string wachtwoord)
        {
            var dto = _repository.GetByNaam(naam);
            if (dto == null) return false;
            return _repository.VerifyPassword(wachtwoord, dto.Wachtwoord);
        }

        private UserModel MapUser(UserDTO dto)
        {
            return new UserModel(
                id: dto.Id,
                gebruikersnaam: dto.Gebruikersnaam,
                wachtwoord: dto.Wachtwoord
            );
        }
    }
}