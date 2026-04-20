using System;
using System.Collections.Generic;

namespace MovieT.ViewModels
{
    public class FilmModelViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public TimeSpan Duration { get; set; }
        public string? Description { get; set; }
        public List<string> Genres { get; set; } = new List<string>();
    }
}