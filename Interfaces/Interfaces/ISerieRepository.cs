using System.Collections.Generic;
using DAL.DTO;

namespace Interfaces.Interfaces
{
    public interface ISerieRepository
    {
        List<SerieDTO> GetAll();
        SerieDTO? GetById(int id);
        List<SerieDTO> Search(string query);
        void AddToWatchingList(int userId, int SerieId);
        void AddToWatchedList(int userId, int SerieId);
        List<SerieDTO> GetWatchingList(int userId);
        List<SerieDTO> GetWatchedList(int userId);
        List<SerieDTO> GetTop10Trending();
    }
}