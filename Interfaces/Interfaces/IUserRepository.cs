using DAL.DTO;

namespace Interfaces.Interfaces
{
    public interface IUserRepository
    {
        UserDTO? GetById(int id);
        UserDTO? GetByNaam(string naam);
        bool UsernameExists(string naam);
        void AddUser(string naam, string wachtwoord);
        bool VerifyPassword(string wachtwoord, string hashedWachtwoord);
    }
}