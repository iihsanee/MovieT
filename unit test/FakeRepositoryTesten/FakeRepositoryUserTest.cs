using DAL.DTO;
using DAL.Repositories;

namespace unit_test.FakeRepositories
{
    public class FakeUserRepository : IUserRepository
    {
        private UserDTO _user = new UserDTO(1, "TestGebruiker");
        public UserDTO? GetById(int id)
        {
            if (id == _user.Id)
                return _user;
            return null;
        }
    }
}