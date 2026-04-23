using System;

namespace DAL.DTO
{
    public class FilmModelDTO
    {
        public int Id { get; }
        public string Title { get; } = string.Empty;
        public DateTime ReleaseDate { get; }
        public TimeSpan Duration { get; }
        public string Description { get; } = string.Empty;

        public FilmModelDTO(int id, string title, DateTime releaseDate, TimeSpan duration, string description)
        {
            Id = id;
            Title = title;
            ReleaseDate = releaseDate;
            Duration = duration;
            Description = description;
        }
    }
}