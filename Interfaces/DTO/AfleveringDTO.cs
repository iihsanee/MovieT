namespace DAL.DTO
{
    public class AfleveringDTO
    {
        public int Id { get; }
        public int SeizoenId { get; }
        public string Titel { get; }
        public int Afleveringsnummer { get; }
        public int Duurtijd { get; }

        public AfleveringDTO(int id, int seizoenId, string titel, int afleveringsnummer, int duurtijd)
        {
            Id = id;
            SeizoenId = seizoenId;
            Titel = titel;
            Afleveringsnummer = afleveringsnummer;
            Duurtijd = duurtijd;
        }
    }
}