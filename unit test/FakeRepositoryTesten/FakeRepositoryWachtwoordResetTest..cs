using DAL.DTO;
using Interfaces.Interfaces;
namespace unit_test.FakeRepositories
{
    public class FakeWachtwoordResetRepository : IWachtwoordResetRepository
    {
        public bool SimuleerLegeDatabase = false;
        private List<WachtwoordResetDTO> _tokens = new List<WachtwoordResetDTO>();
        public void SlaResetTokenOp(WachtwoordResetDTO wachtwoordResetDTO)
        {
            int newId = _tokens.Count > 0 ? _tokens.Max(t => t.Id) + 1 : 1;
            _tokens.Add(new WachtwoordResetDTO(
                newId,
                wachtwoordResetDTO.GebruikerId,
                wachtwoordResetDTO.ResetToken,
                wachtwoordResetDTO.AangemaaktOp,
                wachtwoordResetDTO.Gebruikt
            ));
        }
        public WachtwoordResetDTO? GetByToken(string token)
        {
            if (SimuleerLegeDatabase) return null;
            return _tokens.FirstOrDefault(t => t.ResetToken == token);
        }
        public WachtwoordResetDTO? GetByGebruikerId(int gebruikerId)
        {
            if (SimuleerLegeDatabase) return null;
            return _tokens
                .Where(t => t.GebruikerId == gebruikerId)
                .OrderByDescending(t => t.AangemaaktOp)
                .FirstOrDefault();
        }
        public void MarkeerAlsGebruikt(string token)
        {
            var dto = _tokens.FirstOrDefault(t => t.ResetToken == token);
            if (dto == null) return;
            _tokens.Remove(dto);
            _tokens.Add(new WachtwoordResetDTO(dto.Id, dto.GebruikerId, dto.ResetToken, dto.AangemaaktOp, true));
        }
        public void VerwijderOudeTokens(int gebruikerId)
        {
            _tokens.RemoveAll(t => t.GebruikerId == gebruikerId);
        }
        public string GetLaatsteToken()
        {
            return _tokens.Last().ResetToken;
        }
    }
}

