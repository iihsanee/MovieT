using DAL.DTO;
using Interfaces.Interfaces;
using System.Collections.Generic;

namespace unit_test.FakeRepositories
{
    public class FakeUserRepository : IUserRepository
    {
        private List<UserDTO> _users = new List<UserDTO>
        {
            new UserDTO(1, "TestGebruiker", "Wachtwoord123")
        };

        public UserDTO? GetById(int id) => _users.Find(u => u.Id == id);

        public bool UsernameExists(string naam) => _users.Exists(u => u.Naam == naam);

        public void AddUser(string naam, string wachtwoord)
        {
            _users.Add(new UserDTO(_users.Count + 1, naam, wachtwoord));
        }
    }
}