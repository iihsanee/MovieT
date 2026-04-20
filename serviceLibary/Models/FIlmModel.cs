using System;
using System.Collections.Generic;
using System.Text;

namespace serviceLibary.Models
{
    public class FilmModelModel
    {
        public int Id { get; }
        public string Title { get; }
        public DateTime ReleaseDate { get; }
        public TimeSpan Duration { get; }
        public string Description { get; }

        public FilmModelModel(int id, string title, DateTime releaseDate, TimeSpan duration, string description)
        {
            Id = id;
            Title = title;
            ReleaseDate = releaseDate;
            Duration = duration;
            Description = description;
        }
    }
}