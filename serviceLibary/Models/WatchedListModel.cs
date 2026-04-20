namespace serviceLibary.Models
{
    public class WatchedListModel
    {
        public int UserId { get; set; }
        public int? FilmId { get; set; }
        public int? SerieId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }

        public WatchedListModel(int userId, int? filmId, int? serieId, string title, string type)
        {
            UserId = userId;
            FilmId = filmId;
            SerieId = serieId;
            Title = title;
            Type = type;
        }
    }
}
