using DAL.DTO;
using Interfaces.Interfaces;

namespace unit_test.FakeRepositories
{
    public class FakeAfleveringRepositoryException : IAfleveringRepository
    {
        public List<AfleveringDTO> GetBySeizoenId(int seizoenId) =>
            throw new Exception($"Databasefout bij ophalen van afleveringen voor seizoen {seizoenId}.");

        public AfleveringDTO? GetById(int id) =>
            throw new Exception($"Databasefout bij ophalen van aflevering {id}.");
    }
}