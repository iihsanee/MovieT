namespace MovieT.ViewModels
{
    public class AfleveringViewModel
    {
        public int Id { get; set; }
        public int SeizoenId { get; set; }
        public string Titel { get; set; } = string.Empty;
        public int Afleveringsnummer { get; set; }
        public int Duurtijd { get; set; }
    }
}