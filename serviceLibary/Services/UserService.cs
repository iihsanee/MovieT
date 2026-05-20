using Interfaces.Interfaces;
using serviceLibary.Models;

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
            return new UserModel(dto.Id, dto.Naam, dto.Wachtwoord);
        }

        public string? RegistreerGebruiker(string naam, string wachtwoord, string bevestigWachtwoord)
        {
            
            if (_repository.UsernameExists(naam))
                return "Deze gebruikersnaam is al in gebruik.";

            
            if (wachtwoord.Length < 8)
                return "Het wachtwoord moet minimaal 8 tekens bevatten.";

            
            if (wachtwoord != bevestigWachtwoord)
                return "De wachtwoorden komen niet overeen.";

            _repository.AddUser(naam, wachtwoord);
            return null;
        }
    }
}