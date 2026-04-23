namespace serviceLibary.Models
{
    public class UserModel
    {
        public int Id { get; }
        public string Naam { get; }

        public UserModel(int id, string naam)
        {
            Id = id;
            Naam = naam;
        }
    }
}