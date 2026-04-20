using System;

namespace DAL.DTO
{
    public class FilmModelDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; }
    }
}