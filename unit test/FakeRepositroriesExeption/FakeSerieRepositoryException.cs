using DAL.DTO;
using Interfaces.Interfaces;

namespace unit_test.FakeRepositories
{
    public class FakeSerieRepositoryException : ISerieRepository
    {
        public List<SerieDTO> GetAll() =>
            throw new Exception("Databasefout bij ophalen van alle series.");

        public SerieDTO? GetById(int id) =>
            throw new Exception($"Databasefout bij ophalen van serie met ID {id}.");

        public List<SerieDTO> Search(string query) =>
            throw new Exception($"Databasefout bij zoeken naar series met query '{query}'.");

        public void AddToWatchingList(int userId, int serieId) =>
            throw new Exception("Databasefout bij toevoegen van serie aan watchinglist.");

        public void AddToWatchedList(int userId, int serieId) =>
            throw new Exception("Databasefout bij toevoegen van serie aan watchedlist.");

        public List<SerieDTO> GetWatchingList(int userId) =>
            throw new Exception($"Databasefout bij ophalen van watchinglist voor gebruiker {userId}.");

        public List<SerieDTO> GetWatchedList(int userId) =>
            throw new Exception($"Databasefout bij ophalen van watchedlist voor gebruiker {userId}.");

        public List<SerieDTO> GetTop10Trending() =>
            throw new Exception("Databasefout bij ophalen van top 10 trending series.");
    }
}