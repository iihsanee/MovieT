using DAL.DTO;
using Interfaces.Interfaces;

namespace unit_test.FakeRepositories
{
    public class FakeGenreRepository : IGenreRepository
    {
        public bool SimuleerLegeDatabase = false;
        public bool SimuleerGeenResultaten = false;

        public List<GenreDTO> GetAll()
        {
            if (SimuleerLegeDatabase) return new List<GenreDTO>();
            return new List<GenreDTO>
            {
                new GenreDTO(1, "Thriller"),
                new GenreDTO(2, "Drama")
            };
        }

        public GenreDTO? GetById(int id)
        {
            if (SimuleerLegeDatabase) return null;
            return new List<GenreDTO>
            {
                new GenreDTO(1, "Thriller"),
                new GenreDTO(2, "Drama")
            }.Find(g => g.Id == id);
        }

        public List<GenreDTO> GetByFilmId(int filmId)
        {
            if (SimuleerGeenResultaten) return new List<GenreDTO>();
            return new List<GenreDTO> { new GenreDTO(1, "Thriller") };
        }

        public List<GenreDTO> GetBySerieId(int serieId)
        {
            if (SimuleerGeenResultaten) return new List<GenreDTO>();
            return new List<GenreDTO> { new GenreDTO(2, "Drama") };
        }
    }
}