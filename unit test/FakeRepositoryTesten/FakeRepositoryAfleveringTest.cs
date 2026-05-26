using DAL.DTO;
using Interfaces.Interfaces;

namespace unit_test.FakeRepositories
{
    public class FakeAfleveringRepository : IAfleveringRepository
    {
        private List<AfleveringDTO> _items = new List<AfleveringDTO>
        {
            new AfleveringDTO(1, 1, "Pilot", 1, 45),
            new AfleveringDTO(2, 1, "Aflevering 2", 2, 42),
            new AfleveringDTO(3, 2, "Seizoen 2 Pilot", 1, 50)
        };

        public List<AfleveringDTO> GetBySeizoenId(int seizoenId) =>
            _items.FindAll(a => a.SeizoenId == seizoenId);

        public AfleveringDTO? GetById(int id) =>
            _items.FirstOrDefault(a => a.Id == id);
    }
}