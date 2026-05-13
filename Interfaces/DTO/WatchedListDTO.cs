namespace DAL.DTO
{
    public class WatchedListDTO
    {
        public int UserId { get; }
        public int? FilmId { get; }
        public int? SerieId { get; }
        public string Title { get; } = string.Empty;
        public string Type { get; } = string.Empty;
        public WatchedListDTO(int userId, int? filmId, int? serieId, string title, string type)
        {
            UserId = userId;
            FilmId = filmId;
            SerieId = serieId;
            Title = title;
            Type = type;
        }
    }
}