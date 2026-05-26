namespace serviceLibary.Models
{
    public class UserModel
    {
        public int Id { get; }
        public string Gebruikersnaam { get; }
        public string Wachtwoord { get; }

        public UserModel(int id, string gebruikersnaam, string wachtwoord)
        {
            Id = id;
            Gebruikersnaam = gebruikersnaam;
            Wachtwoord = wachtwoord;
        }
    }
}