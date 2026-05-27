using DAL.DTO;
using Interfaces.Interfaces;

namespace unit_test.FakeRepositories
{
    public class FakeWatchingListRepositoryException : IWatchingListRepository
    {
        public List<WatchingListDTO> GetByUser(int userId) =>
            throw new Exception($"Databasefout bij ophalen van watchinglist voor gebruiker {userId}.");

        public void Add(int userId, int? filmId, int? serieId) =>
            throw new Exception("Databasefout bij toevoegen aan watchinglist.");

        public void Remove(int userId, int? filmId, int? serieId) =>
            throw new Exception("Databasefout bij verwijderen uit watchinglist.");
    }
}