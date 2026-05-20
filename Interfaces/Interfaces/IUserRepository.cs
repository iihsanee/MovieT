using DAL.DTO;

namespace Interfaces.Interfaces
{
    public interface IUserRepository
    {
        UserDTO? GetById(int id);
        bool UsernameExists(string naam);
        void AddUser(string naam, string wachtwoord);
    }
}