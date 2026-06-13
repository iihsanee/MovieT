using DAL.DTO;
using Interfaces.Interfaces;

namespace unit_test.FakeRepositories
{
    public class FakeSeizoenRepository : ISeizoenRepository
    {
        public bool SimuleerLegeDatabase = false;

        public List<SeizoenDTO> GetBySerieId(int serieId)
        {
            if (SimuleerLegeDatabase) return new List<SeizoenDTO>();
            return new List<SeizoenDTO>
            {
                new SeizoenDTO(1, 1, 1, 10, 2020),
                new SeizoenDTO(2, 1, 2, 8, 2021),
                new SeizoenDTO(3, 2, 1, 6, 2022)
            }.FindAll(s => s.SerieId == serieId);
        }

        public SeizoenDTO? GetById(int id)
        {
            if (SimuleerLegeDatabase) return null;
            return new List<SeizoenDTO>
            {
                new SeizoenDTO(1, 1, 1, 10, 2020),
                new SeizoenDTO(2, 1, 2, 8, 2021),
                new SeizoenDTO(3, 2, 1, 6, 2022)
            }.FirstOrDefault(s => s.Id == id);
        }
    }
}