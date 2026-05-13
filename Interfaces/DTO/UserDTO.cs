namespace DAL.DTO
{
    public class UserDTO
    {
        public int Id { get; }
        public string Naam { get; } = string.Empty;
        public UserDTO(int id, string naam)
        {
            Id = id;
            Naam = naam;
        }
    }
}