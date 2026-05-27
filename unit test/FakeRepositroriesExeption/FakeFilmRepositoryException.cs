using DAL.DTO;
using Interfaces.Interfaces;

namespace unit_test.FakeRepositories
{
    public class FakeFilmRepositoryException : IFilmModelRepository
    {
        public List<FilmModelDTO> GetAll() =>
            throw new Exception("Databasefout bij ophalen van alle films.");

        public FilmModelDTO? GetById(int id) =>
            throw new Exception($"Databasefout bij ophalen van film met ID {id}.");

        public List<FilmModelDTO> Search(string query) =>
            throw new Exception($"Databasefout bij zoeken naar films met query '{query}'.");

        public void AddToWatchingList(int userId, int filmModelId) =>
            throw new Exception("Databasefout bij toevoegen aan watchinglist.");

        public void AddToWatchedList(int userId, int filmModelId) =>
            throw new Exception("Databasefout bij toevoegen aan watchedlist.");

        public List<FilmModelDTO> GetWatchingList(int userId) =>
            throw new Exception($"Databasefout bij ophalen van watchinglist voor gebruiker {userId}.");

        public List<FilmModelDTO> GetWatchedList(int userId) =>
            throw new Exception($"Databasefout bij ophalen van watchedlist voor gebruiker {userId}.");

        public List<FilmModelDTO> GetTop10Trending() =>
            throw new Exception("Databasefout bij ophalen van top 10 trending films.");
    }
}