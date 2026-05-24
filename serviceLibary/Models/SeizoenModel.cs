namespace serviceLibary.Models
{
    public class SeizoenModel
    {
        public int Id { get; }
        public int SerieId { get; }
        public int Seizoennummer { get; }
        public int AantalAfleveringen { get; }
        public int Jaartal { get; }

        public SeizoenModel(int id, int serieId, int seizoennummer, int aantalAfleveringen, int jaartal)
        {
            Id = id;
            SerieId = serieId;
            Seizoennummer = seizoennummer;
            AantalAfleveringen = aantalAfleveringen;
            Jaartal = jaartal;
        }
    }
}