using DAL.DTO;
using Interfaces.Interfaces;
namespace unit_test.FakeRepositories
{
    public class FakeUserRepository : IUserRepository
    {
        public bool SimuleerGebruikersnaamBestaat = false;
        private List<UserDTO> _users = new List<UserDTO>
        {
            new UserDTO(1, "TestGebruiker", BCrypt.Net.BCrypt.HashPassword("wachtwoord123"), "test@student.fontys.nl"),
            new UserDTO(2, "AdminUser", BCrypt.Net.BCrypt.HashPassword("admin456"), "admin@student.fontys.nl")
        };
        public UserDTO? GetById(int id) => _users.FirstOrDefault(u => u.Id == id);
        public UserDTO? GetByEmail(string email) => _users.FirstOrDefault(u => u.Email == email);
        public bool EmailExists(string email)
        {
            if (SimuleerGebruikersnaamBestaat) return true;
            return _users.Any(u => u.Email == email);
        }
        public void AddUser(string naam, string email, string wachtwoord)
        {
            int newId = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
            string hashedWachtwoord = BCrypt.Net.BCrypt.HashPassword(wachtwoord);
            _users.Add(new UserDTO(newId, naam, hashedWachtwoord, email));
        }
        public void UpdateWachtwoord(int gebruikerId, string nieuwWachtwoord)
        {
            var user = _users.FirstOrDefault(u => u.Id == gebruikerId);
            if (user == null) return;
            string hashedWachtwoord = BCrypt.Net.BCrypt.HashPassword(nieuwWachtwoord);
            _users.Remove(user);
            _users.Add(new UserDTO(user.Id, user.Gebruikersnaam, hashedWachtwoord, user.Email));
        }
        public bool VerifyPassword(string wachtwoord, string hashedWachtwoord) =>
            BCrypt.Net.BCrypt.Verify(wachtwoord, hashedWachtwoord);
        public UserDTO? Login(string email, string wachtwoord)
        {
            var user = _users.FirstOrDefault(u => u.Email == email);
            if (user == null) return null;
            if (!BCrypt.Net.BCrypt.Verify(wachtwoord, user.Wachtwoord)) return null;
            return user;
        }
        public void Register(string email, string wachtwoord)
        {
            int newId = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
            string hashedWachtwoord = BCrypt.Net.BCrypt.HashPassword(wachtwoord);
            _users.Add(new UserDTO(newId, string.Empty, hashedWachtwoord, email));
        }
    }
}