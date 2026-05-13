using DAL.DTO;

namespace Interfaces.Interfaces
{
    public interface IUserRepository
    {
        UserDTO? GetById(int id);
    }
}