using DAL.DTO;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Interfaces.Interfaces
{
    public interface IWatchingListRepository
    {
        List<WatchingListDTO> GetByUser(int userId);
        void Add(int userId, int? filmId, int? serieId);
    }
}