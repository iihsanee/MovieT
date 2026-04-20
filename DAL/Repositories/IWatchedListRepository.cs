using System.Collections.Generic;
using DAL.DTO;

namespace DAL.Repositories
{
    public interface IWatchedListRepository
    {
        List<WatchedListDTO> GetByUser(int userId);
        void Add(int userId, int? filmId, int? serieId);
    }
}
