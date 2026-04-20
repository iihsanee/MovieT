namespace MovieT.ViewModels
{
    public class WatchedListViewModel
    {
        public int UserId { get; set; }
        public int? FilmId { get; set; }
        public int? SerieId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
    }
}