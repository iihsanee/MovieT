using DAL.DTO;
using Interfaces.Interfaces;

namespace unit_test.FakeRepositories
{
    public class FakeFilmRepository : IFilmModelRepository
    {
        public bool SimuleerLegeDatabase = false;
        public bool SimuleerGeenResultaten = false;

        private List<FilmModelDTO> _films = new List<FilmModelDTO>
        {
            new FilmModelDTO(1, "Inception", new DateTime(2010, 7, 16), TimeSpan.FromHours(2), "Test"),
            new FilmModelDTO(2, "The Matrix", new DateTime(1999, 3, 31), TimeSpan.FromHours(2), "Test")
        };

        public List<FilmModelDTO> GetAll()
        {
            if (SimuleerLegeDatabase) return new List<FilmModelDTO>();
            return _films;
        }

        public FilmModelDTO? GetById(int id)
        {
            if (SimuleerLegeDatabase) return null;
            return _films.Find(f => f.Id == id);
        }

        public List<FilmModelDTO> Search(string query)
        {
            if (SimuleerGeenResultaten) return new List<FilmModelDTO>();
            return _films.FindAll(f => f.Title.Contains(query));
        }

        public void AddToWatchingList(int userId, int filmId) { }
        public void AddToWatchedList(int userId, int filmId) { }

        public List<FilmModelDTO> GetWatchingList(int userId)
        {
            if (SimuleerLegeDatabase) return new List<FilmModelDTO>();
            return _films;
        }

        public List<FilmModelDTO> GetWatchedList(int userId)
        {
            if (SimuleerLegeDatabase) return new List<FilmModelDTO>();
            return _films;
        }

        public List<FilmModelDTO> GetTop10Trending()
        {
            if (SimuleerLegeDatabase) return new List<FilmModelDTO>();
            return _films;
        }
    }
}