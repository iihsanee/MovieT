using DAL.DTO;
namespace Interfaces.Interfaces
{
    public interface IWachtwoordResetRepository
    {
        void SlaResetTokenOp(WachtwoordResetDTO wachtwoordResetDTO);
        WachtwoordResetDTO? GetByToken(string token);
        void MarkeerAlsGebruikt(string token);
        WachtwoordResetDTO? GetByGebruikerId(int gebruikerId);
        void VerwijderOudeTokens(int gebruikerId);
    }
}