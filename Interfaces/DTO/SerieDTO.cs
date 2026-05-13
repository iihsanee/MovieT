using System;
namespace DAL.DTO
{
    public class SerieDTO
    {
        public int Id { get; }
        public string Title { get; } = string.Empty;
        public DateTime ReleaseDate { get; }
        public TimeSpan Duration { get; }
        public string Description { get; } = string.Empty;
        public SerieDTO(int id, string title, DateTime releaseDate, TimeSpan duration, string description)
        {
            Id = id;
            Title = title;
            ReleaseDate = releaseDate;
            Duration = duration;
            Description = description;
        }
    }
}