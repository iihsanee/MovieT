using DAL.DTO;

namespace Interfaces.Interfaces
{
    public interface IWachtwoordResetRepository
    {
        void SlaResetTokenOp(WachtwoordResetDTO wachtwoordResetDTO);
        WachtwoordResetDTO? GetByToken(string token);
        WachtwoordResetDTO? GetByGebruikerId(int gebruikerId);
        void MarkeerAlsGebruikt(string token);
    }
}