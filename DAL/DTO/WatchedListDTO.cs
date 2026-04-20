namespace DAL.DTO
{
    public class WatchedListDTO
    {
        public int UserId { get; set; }
        public int? FilmId { get; set; }
        public int? SerieId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
    }
}