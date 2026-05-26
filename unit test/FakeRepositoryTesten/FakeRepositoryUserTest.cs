using DAL.DTO;
using Interfaces.Interfaces;

namespace unit_test.FakeRepositories
{
    public class FakeUserRepository : IUserRepository
    {
        private List<UserDTO> _users = new List<UserDTO>
        {
            new UserDTO(1, "TestGebruiker", BCrypt.Net.BCrypt.HashPassword("wachtwoord123")),
            new UserDTO(2, "AdminUser", BCrypt.Net.BCrypt.HashPassword("admin456"))
        };

        public UserDTO? GetById(int id) => _users.FirstOrDefault(u => u.Id == id);
        public UserDTO? GetByNaam(string naam) => _users.FirstOrDefault(u => u.Gebruikersnaam == naam);
        public bool UsernameExists(string naam) => _users.Any(u => u.Gebruikersnaam == naam);

        public void AddUser(string naam, string wachtwoord)
        {
            int newId = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
            string hashedWachtwoord = BCrypt.Net.BCrypt.HashPassword(wachtwoord);
            _users.Add(new UserDTO(newId, naam, hashedWachtwoord));
        }

        public bool VerifyPassword(string wachtwoord, string hashedWachtwoord)
        {
            return BCrypt.Net.BCrypt.Verify(wachtwoord, hashedWachtwoord);
        }
    }
}