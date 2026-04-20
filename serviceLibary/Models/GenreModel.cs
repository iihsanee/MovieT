namespace serviceLibary.Models
{
    public class GenreModel
    {
        public int Id { get; }
        public string Naam { get; }

        public GenreModel(int id, string naam)
        {
            Id = id;
            Naam = naam;
        }
    }
}