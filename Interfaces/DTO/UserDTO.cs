namespace DAL.DTO
{
    public class UserDTO
    {
        public int Id { get; }
        public string Naam { get; } = string.Empty;
        public string Wachtwoord { get; } = string.Empty;

        public UserDTO(int id, string naam, string wachtwoord)
        {
            Id = id;
            Naam = naam;
            Wachtwoord = wachtwoord;
        }
    }
}