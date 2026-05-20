namespace serviceLibary.Models
{
    public class UserModel
    {
        public int Id { get; }
        public string Naam { get; }
        public string Wachtwoord { get; }

        public UserModel(int id, string naam, string wachtwoord)
        {
            Id = id;
            Naam = naam;
            Wachtwoord = wachtwoord;
        }
    }
}