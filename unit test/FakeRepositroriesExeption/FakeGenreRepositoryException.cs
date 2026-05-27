using DAL.DTO;
using Interfaces.Interfaces;

namespace unit_test.FakeRepositories
{
    public class FakeGenreRepositoryException : IGenreRepository
    {
        public List<GenreDTO> GetAll() =>
            throw new Exception("Databasefout bij ophalen van alle genres.");

        public GenreDTO? GetById(int id) =>
            throw new Exception($"Databasefout bij ophalen van genre met ID {id}.");

        public List<GenreDTO> GetByFilmId(int filmId) =>
            throw new Exception($"Databasefout bij ophalen van genres voor film met ID {filmId}.");

        public List<GenreDTO> GetBySerieId(int serieId) =>
            throw new Exception($"Databasefout bij ophalen van genres voor serie met ID {serieId}.");
    }
}