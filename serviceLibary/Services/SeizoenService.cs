using Interfaces.Interfaces;
using serviceLibary.Models;
using DAL.DTO;

namespace serviceLibary.Services
{
    public class SeizoenService
    {
        private readonly ISeizoenRepository _repository;

        public SeizoenService(ISeizoenRepository repository)
        {
            _repository = repository;
        }

        public List<SeizoenModel> GetBySerieId(int serieId)
        {
            return _repository.GetBySerieId(serieId)
                .Select(dto => MapSeizoen(dto))
                .ToList();
        }

        public SeizoenModel? GetById(int id)
        {
            var dto = _repository.GetById(id);
            if (dto == null) return null;
            return MapSeizoen(dto);
        }

        private SeizoenModel MapSeizoen(SeizoenDTO dto)
        {
            return new SeizoenModel(
                id: dto.Id,
                serieId: dto.SerieId,
                seizoennummer: dto.Seizoennummer,
                aantalAfleveringen: dto.AantalAfleveringen,
                jaartal: dto.Jaartal
            );
        }
    }
}