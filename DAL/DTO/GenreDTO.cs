namespace DAL.DTO
{
    public class GenreDTO
    {
        public int Id { get; }
        public string Naam { get; } = string.Empty;
        public GenreDTO(int id, string naam)
        {
            Id = id;
            Naam = naam;
        }
    }
}