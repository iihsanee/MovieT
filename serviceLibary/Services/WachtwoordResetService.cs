using Interfaces.Interfaces;
using serviceLibary.Models;
using DAL.DTO;

namespace serviceLibary.Services
{
    public class WachtwoordResetService
    {
        private readonly IWachtwoordResetRepository _repository;
        private readonly IUserRepository _userRepository;

        public WachtwoordResetService(IWachtwoordResetRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public string? VraagResetAan(string email)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null)
                return "Er is geen account gevonden met dit e-mailadres.";

            string token = Guid.NewGuid().ToString();
            SlaResetTokenOp(user.Id, token);
            return null;
        }

        public WachtwoordResetModel? GetByEmail(string email)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null) return null;

            var dto = _repository.GetByGebruikerId(user.Id);
            if (dto == null) return null;
            return MapWachtwoordReset(dto);
        }

        public void SlaResetTokenOp(int gebruikerId, string token)
        {
            WachtwoordResetDTO dto = new WachtwoordResetDTO(
                id: 0,
                gebruikerId: gebruikerId,
                resetToken: token,
                aangemaaktOp: DateTime.Now,
                gebruikt: false
            );
            _repository.SlaResetTokenOp(dto);
        }

        public WachtwoordResetModel? GetByToken(string token)
        {
            var dto = _repository.GetByToken(token);
            if (dto == null) return null;
            return MapWachtwoordReset(dto);
        }

        public string? ValideerToken(string token)
        {
            var dto = _repository.GetByToken(token);
            if (dto == null)
                return "Deze reset link bestaat niet.";
            if (dto.Gebruikt)
                return "Deze reset link is al gebruikt.";
            return null;
        }

        public void MarkeerAlsGebruikt(string token)
        {
            _repository.MarkeerAlsGebruikt(token);
        }

        public void ResetWachtwoord(string token, string nieuwWachtwoord)
        {
            var dto = _repository.GetByToken(token);
            if (dto == null) return;

            _userRepository.UpdateWachtwoord(dto.GebruikerId, nieuwWachtwoord);
            _repository.MarkeerAlsGebruikt(token);
        }

        private WachtwoordResetModel MapWachtwoordReset(WachtwoordResetDTO dto)
        {
            return new WachtwoordResetModel(
                id: dto.Id,
                gebruikerId: dto.GebruikerId,
                resetToken: dto.ResetToken,
                aangemaaktOp: dto.AangemaaktOp,
                gebruikt: dto.Gebruikt
            );
        }
    }
}