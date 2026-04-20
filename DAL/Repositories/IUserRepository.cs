using DAL.DTO;

namespace DAL.Repositories
{
    public interface IUserRepository
    {
        UserDTO GetById(int id);
    }
}