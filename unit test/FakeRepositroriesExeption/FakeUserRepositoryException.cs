using DAL.DTO;
using Interfaces.Interfaces;

namespace unit_test.FakeRepositories
{
    public class FakeUserRepositoryException : IUserRepository
    {
        public UserDTO? GetById(int id) =>
            throw new Exception($"Databasefout bij ophalen van gebruiker met ID {id}.");

        public UserDTO? GetByNaam(string naam) =>
            throw new Exception("Databasefout bij ophalen van gebruiker op naam.");

        public bool UsernameExists(string naam) =>
            throw new Exception("Databasefout bij controleren van gebruikersnaam.");

        public void AddUser(string naam, string wachtwoord) =>
            throw new Exception("Databasefout bij aanmaken van gebruiker.");

        public bool VerifyPassword(string wachtwoord, string hashedWachtwoord) =>
            throw new Exception("Databasefout bij verifiëren van wachtwoord.");
    }
}