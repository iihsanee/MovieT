using System.ComponentModel.DataAnnotations;
namespace serviceLibary.Models
{
    public class UserModel
    {
        public int Id { get; }
        public string Wachtwoord { get; }
        public string Email { get; }
        public UserModel(int id, string wachtwoord, string email)
        {
            Id = id;
            Email = email;
            Wachtwoord = wachtwoord;
        }
    }
}