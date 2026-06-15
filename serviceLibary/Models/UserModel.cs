using System.ComponentModel.DataAnnotations;
namespace serviceLibary.Models
{
    public class UserModel
    {
        public int Id { get; }
        public string Gebruikersnaam { get; }
        public string Wachtwoord { get; }
        public string Email { get; }
        public UserModel(int id, string gebruikersnaam, string wachtwoord, string email)
        {
            Id = id;
            Gebruikersnaam = gebruikersnaam;
            Email = email;
            Wachtwoord = wachtwoord;
        }
    }
}