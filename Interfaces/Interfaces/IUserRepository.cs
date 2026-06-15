using DAL.DTO;
namespace Interfaces.Interfaces
{
    public interface IUserRepository
    {
        UserDTO? GetById(int id);
        UserDTO? GetByEmail(string email);
        bool EmailExists(string email);
        void AddUser(string naam, string email, string wachtwoord);
        bool VerifyPassword(string wachtwoord, string hashedWachtwoord);
        void UpdateWachtwoord(int gebruikerId, string nieuwWachtwoord);
        UserDTO? Login(string email, string wachtwoord);
        void Register(string email, string wachtwoord);
    }
}