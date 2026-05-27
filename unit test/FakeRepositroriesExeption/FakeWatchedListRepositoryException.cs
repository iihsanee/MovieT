using DAL.DTO;
using Interfaces.Interfaces;

namespace unit_test.FakeRepositories
{
    public class FakeWatchedListRepositoryException : IWatchedListRepository
    {
        public List<WatchedListDTO> GetByUser(int userId) =>
            throw new Exception($"Databasefout bij ophalen van watchedlist voor gebruiker {userId}.");

        public void Add(int userId, int? filmId, int? serieId) =>
            throw new Exception("Databasefout bij toevoegen aan watchedlist.");
    }
}