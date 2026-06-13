namespace DAL.DTO
{
    public class UserDTO
    {
        public int Id { get; }
        public string Gebruikersnaam { get; } = string.Empty;
        public string Wachtwoord { get; } = string.Empty;
        public string Email { get; } = string.Empty;

        public UserDTO(int id, string gebruikersnaam, string wachtwoord, string email)
        {
            Id = id;
            Gebruikersnaam = gebruikersnaam;
            Wachtwoord = wachtwoord;
            Email = email;
        }
    }
}