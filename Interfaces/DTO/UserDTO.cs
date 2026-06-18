namespace DAL.DTO
{
    public class UserDTO
    {
        public int Id { get; }
       
        public string Wachtwoord { get; } = string.Empty;
        public string Email { get; } = string.Empty;

        public UserDTO(int id, string wachtwoord, string email)
        {
            Id = id;
            Wachtwoord = wachtwoord;
            Email = email;
        }
    }
}