using Interfaces.Interfaces;
using serviceLibary.Models;

namespace serviceLibary.Services
{
    public class AfleveringService
    {
        private readonly IAfleveringRepository _repository;

        public AfleveringService(IAfleveringRepository repository)
        {
            _repository = repository;
        }

        public List<AfleveringModel> GetBySeizoenId(int seizoenId)
        {
            return _repository.GetBySeizoenId(seizoenId)
                .Select(dto => new AfleveringModel(dto.Id, dto.SeizoenId, dto.Titel, dto.Afleveringsnummer, dto.Duurtijd))
                .ToList();
        }

        public AfleveringModel? GetById(int id)
        {
            var dto = _repository.GetById(id);
            if (dto == null) return null;
            return new AfleveringModel(dto.Id, dto.SeizoenId, dto.Titel, dto.Afleveringsnummer, dto.Duurtijd);
        }
    }
}