using DAL.DTO;
namespace Interfaces.Interfaces
{
    public interface IWatchedListRepository
    {
        List<WatchedListDTO> GetByUser(int userId);
        void Add(int userId, int? filmId, int? serieId);
        void Remove(int userId, int? filmId, int? serieId);
    }
}