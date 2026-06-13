using DAL.DTO;
using Interfaces.Interfaces;

namespace unit_test.FakeRepositories
{
    public class FakeSerieRepository : ISerieRepository
    {
        public bool SimuleerLegeDatabase = false;
        public bool SimuleerGeenResultaten = false;

        private List<SerieDTO> _series = new List<SerieDTO>
        {
            new SerieDTO(1, "Breaking Bad", new DateTime(2008, 1, 20), TimeSpan.FromMinutes(47), "Test"),
            new SerieDTO(2, "Stranger Things", new DateTime(2016, 7, 15), TimeSpan.FromMinutes(51), "Test")
        };

        public List<SerieDTO> GetAll()
        {
            if (SimuleerLegeDatabase) return new List<SerieDTO>();
            return _series;
        }

        public SerieDTO? GetById(int id)
        {
            if (SimuleerLegeDatabase) return null;
            return _series.Find(s => s.Id == id);
        }

        public List<SerieDTO> Search(string query)
        {
            if (SimuleerGeenResultaten) return new List<SerieDTO>();
            return _series.FindAll(s => s.Title.Contains(query));
        }

        public void AddToWatchingList(int userId, int serieId) { }
        public void AddToWatchedList(int userId, int serieId) { }

        public List<SerieDTO> GetWatchingList(int userId)
        {
            if (SimuleerLegeDatabase) return new List<SerieDTO>();
            return _series;
        }

        public List<SerieDTO> GetWatchedList(int userId)
        {
            if (SimuleerLegeDatabase) return new List<SerieDTO>();
            return _series;
        }

        public List<SerieDTO> GetTop10Trending()
        {
            if (SimuleerLegeDatabase) return new List<SerieDTO>();
            return _series;
        }
    }
}