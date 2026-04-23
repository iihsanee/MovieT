namespace serviceLibary.Models
{
    public class WatchingListModel
    {
        public int UserId { get; }
        public int? FilmId { get; }
        public int? SerieId { get; }
        public string Title { get; }
        public string Type { get; }

        public WatchingListModel(int userId, int? filmId, int? serieId, string title, string type)
        {
            UserId = userId;
            FilmId = filmId;
            SerieId = serieId;
            Title = title;
            Type = type;
        }
    }
}