using System;
using System.Collections.Generic;

namespace serviceLibary.Models
{
    public class SerieModel
    {
        public int Id { get; }
        public string Title { get; }
        public DateTime ReleaseDate { get; }
        public TimeSpan Duration { get; }
        public string Description { get; }
       

        public SerieModel(int id, string title, DateTime releaseDate, TimeSpan duration, string description)
        {
            Id = id;
            Title = title;
            ReleaseDate = releaseDate;
            Duration = duration;
            Description = description;
           
        }
    }
}