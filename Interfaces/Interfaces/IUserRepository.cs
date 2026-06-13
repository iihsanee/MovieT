using DAL.DTO;

namespace Interfaces.Interfaces
{
    public interface IUserRepository
    {
        UserDTO? GetById(int id);
        UserDTO? GetByNaam(string naam);
        UserDTO? GetByEmail(string email);
        bool UsernameExists(string naam);
        void AddUser(string naam, string wachtwoord);
        bool VerifyPassword(string wachtwoord, string hashedWachtwoord);
        void UpdateWachtwoord(int gebruikerId, string nieuwWachtwoord);
    }
}