using DAL.DTO;
using Interfaces.Interfaces;

namespace unit_test.FakeRepositories
{
    public class FakeSeizoenRepositoryException : ISeizoenRepository
    {
        public List<SeizoenDTO> GetBySerieId(int serieId) =>
            throw new Exception($"Databasefout bij ophalen van seizoenen voor serie {serieId}.");

        public SeizoenDTO? GetById(int id) =>
            throw new Exception($"Databasefout bij ophalen van seizoen {id}.");
    }
}