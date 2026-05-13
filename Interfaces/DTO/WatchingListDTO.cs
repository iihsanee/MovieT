namespace DAL.DTO
{
    public class WatchingListDTO
    {
        public int UserId { get; }
        public int? FilmId { get; }
        public int? SerieId { get; }
        public string Title { get; } = string.Empty;
        public string Type { get; } = string.Empty;
        public WatchingListDTO(int userId, int? filmId, int? serieId, string title, string type)
        {
            UserId = userId;
            FilmId = filmId;
            SerieId = serieId;
            Title = title;
            Type = type;
        }
    }
}