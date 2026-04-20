namespace serviceLibary.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Naam { get; set; }

        public UserModel(int id, string naam)
        {
            Id = id;
            Naam = naam;
        }
    }
}