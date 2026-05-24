using DAL.DTO;

namespace Interfaces.Interfaces
{
    public interface ISeizoenRepository
    {
        List<SeizoenDTO> GetBySerieId(int serieId);
        SeizoenDTO? GetById(int id);
    }
}