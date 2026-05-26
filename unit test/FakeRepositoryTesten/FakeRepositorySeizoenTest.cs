using DAL.DTO;
using Interfaces.Interfaces;

namespace unit_test.FakeRepositories
{
    public class FakeSeizoenRepository : ISeizoenRepository
    {
        private List<SeizoenDTO> _items = new List<SeizoenDTO>
        {
            new SeizoenDTO(1, 1, 1, 10, 2020),
            new SeizoenDTO(2, 1, 2, 8, 2021),
            new SeizoenDTO(3, 2, 1, 6, 2022)
        };

        public List<SeizoenDTO> GetBySerieId(int serieId) =>
            _items.FindAll(s => s.SerieId == serieId);

        public SeizoenDTO? GetById(int id) =>
            _items.FirstOrDefault(s => s.Id == id);
    }
}