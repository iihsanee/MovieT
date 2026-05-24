using System.Collections.Generic;

namespace MovieT.ViewModels
{
    public class SeizoenViewModel
    {
        public int Id { get; set; }
        public int SerieId { get; set; }
        public int Seizoennummer { get; set; }
        public int AantalAfleveringen { get; set; }
        public int Jaartal { get; set; }
        public List<AfleveringViewModel> Afleveringen { get; set; } = new List<AfleveringViewModel>();
    }
}