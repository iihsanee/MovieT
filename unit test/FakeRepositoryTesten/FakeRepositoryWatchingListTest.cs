using DAL.DTO;
using Interfaces.Interfaces;
using System.Collections.Generic;

namespace unit_test.FakeRepositories
{
    public class FakeWatchingListRepository : IWatchingListRepository
    {
        private List<WatchingListDTO> _items = new List<WatchingListDTO>
        {
            new WatchingListDTO(1, 1, null, "Inception", "Film"),
            new WatchingListDTO(1, null, 1, "Breaking Bad", "Serie")
        };
        public List<WatchingListDTO> GetByUser(int userId) => _items.FindAll(i => i.UserId == userId);
        public void Add(int userId, int? filmId, int? serieId)
        {
            _items.Add(new WatchingListDTO(
                userId,
                filmId,
                serieId,
                "Test",
                filmId.HasValue ? "Film" : "Serie"
            ));
        }
    }
}