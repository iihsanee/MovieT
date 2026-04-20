using System.Collections.Generic;
using DAL.DTO;

namespace DAL.Repositories
{
    public interface IWatchingListRepository
    {
        List<WatchingListDTO> GetByUser(int userId);
        void Add(int userId, int? filmId, int? serieId);
    }
}