using Interfaces.Interfaces;
using serviceLibary.Models;

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
                .Select(dto => new SeizoenModel(dto.Id, dto.SerieId, dto.Seizoennummer, dto.AantalAfleveringen, dto.Jaartal))
                .ToList();
        }

        public SeizoenModel? GetById(int id)
        {
            var dto = _repository.GetById(id);
            if (dto == null) return null;
            return new SeizoenModel(dto.Id, dto.SerieId, dto.Seizoennummer, dto.AantalAfleveringen, dto.Jaartal);
        }
    }
}