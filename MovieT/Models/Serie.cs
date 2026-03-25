namespace MovieT.Models
{
    public class Serie
    {
        public int Id { get; set; }
        public string Titel { get; set; } = string.Empty;
        public string Beschrijving { get; set; } = string.Empty;

        public string Gettitel()
        {
            return Titel;
        }

        public string GetBeschrijving()
        {
            return Beschrijving;
        }
    }
}
