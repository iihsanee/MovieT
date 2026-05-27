using Interfaces.Interfaces;
using serviceLibary.Models;
using DAL.DTO;

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
                .Select(dto => MapAflevering(dto))
                .ToList();
        }

        public AfleveringModel? GetById(int id)
        {
            var dto = _repository.GetById(id);
            if (dto == null) return null;
            return MapAflevering(dto);
        }

        private AfleveringModel MapAflevering(AfleveringDTO dto)
        {
            return new AfleveringModel(
                id: dto.Id,
                seizoenId: dto.SeizoenId,
                titel: dto.Titel,
                afleveringsnummer: dto.Afleveringsnummer,
                duurtijd: dto.Duurtijd
            );
        }
    }
}