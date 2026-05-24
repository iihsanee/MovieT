using System;
using System.Collections.Generic;

namespace MovieT.ViewModels
{
    public class SerieViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<string> Genres { get; set; } = new List<string>();
        public List<SeizoenViewModel> Seizoenen { get; set; } = new List<SeizoenViewModel>();
    }
}