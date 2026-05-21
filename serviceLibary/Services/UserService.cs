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

        public UserModel? GetByNaam(string naam)
        {
            var dto = _repository.GetByNaam(naam);
            if (dto == null) return null;
            return new UserModel(dto.Id, dto.Naam, dto.Wachtwoord);
        }

        public bool UsernameExists(string naam)
        {
            return _repository.UsernameExists(naam);
        }

        public void RegistreerGebruiker(string naam, string wachtwoord)
        {
            _repository.AddUser(naam, wachtwoord);
        }
    }
}