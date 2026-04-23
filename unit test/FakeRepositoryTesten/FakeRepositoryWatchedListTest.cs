using DAL.DTO;
using DAL.Repositories;
using System.Collections.Generic;

namespace unit_test.FakeRepositories
{
    public class FakeWatchedListRepository : IWatchedListRepository
    {
        private List<WatchedListDTO> _items = new List<WatchedListDTO>
        {
            new WatchedListDTO(1, 1, null, "Inception", "Film"),
            new WatchedListDTO(1, null, 1, "Breaking Bad", "Serie")
        };
        public List<WatchedListDTO> GetByUser(int userId) => _items.FindAll(i => i.UserId == userId);
        public void Add(int userId, int? filmId, int? serieId)
        {
            _items.Add(new WatchedListDTO(
                userId,
                filmId,
                serieId,
                "Test",
                filmId.HasValue ? "Film" : "Serie"
            ));
        }
    }
}