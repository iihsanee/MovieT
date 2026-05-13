using DAL.DTO;
using Interfaces.Interfaces;
using System.Collections.Generic;

namespace unit_test.FakeRepositories
{
    public class FakeGenreRepository : IGenreRepository
    {
        private List<GenreDTO> _genres = new List<GenreDTO>
        {
            new GenreDTO(1, "Thriller"),
            new GenreDTO(2, "Drama")
        };
        public List<GenreDTO> GetAll() => _genres;
        public GenreDTO? GetById(int id) => _genres.Find(g => g.Id == id);
        public List<GenreDTO> GetByFilmId(int filmId) => new List<GenreDTO> { new GenreDTO(1, "Thriller") };
        public List<GenreDTO> GetBySerieId(int serieId) => new List<GenreDTO> { new GenreDTO(2, "Drama") };
    }
}