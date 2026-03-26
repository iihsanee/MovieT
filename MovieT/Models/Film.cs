namespace MovieT.Models
{
    public class Film
    {
        public int ID { get; set; }
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
