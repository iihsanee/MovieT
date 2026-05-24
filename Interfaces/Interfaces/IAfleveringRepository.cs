using DAL.DTO;

namespace Interfaces.Interfaces
{
    public interface IAfleveringRepository
    {
        List<AfleveringDTO> GetBySeizoenId(int seizoenId);
        AfleveringDTO? GetById(int id);
    }
}