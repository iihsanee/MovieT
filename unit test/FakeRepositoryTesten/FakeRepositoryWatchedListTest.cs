using DAL.DTO;
using Interfaces.Interfaces;
namespace unit_test.FakeRepositories
{
    public class FakeWatchedListRepository : IWatchedListRepository
    {
        public bool SimuleerLegeDatabase = false;
        private List<WatchedListDTO> _items = new List<WatchedListDTO>
        {
            new WatchedListDTO(1, 1, null, "Inception", "Film"),
            new WatchedListDTO(1, null, 1, "Breaking Bad", "Serie")
        };

        public List<WatchedListDTO> GetByUser(int userId)
        {
            if (SimuleerLegeDatabase) return new List<WatchedListDTO>();
            return _items.FindAll(i => i.UserId == userId);
        }

        public void Add(int userId, int? filmId, int? serieId)
        {
            _items.Add(new WatchedListDTO(
                userId, filmId, serieId,
                "Test",
                filmId.HasValue ? "Film" : "Serie"
            ));
        }

        public void Remove(int userId, int? filmId, int? serieId)
        {
            if (filmId.HasValue)
                _items.RemoveAll(i => i.UserId == userId && i.FilmId == filmId);
            else
                _items.RemoveAll(i => i.UserId == userId && i.SerieId == serieId);
        }
    }
}