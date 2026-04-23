using System.Collections.Generic;
using DAL.DTO;

namespace DAL.Repositories
{
    public interface IFilmModelRepository
    {
        List<FilmModelDTO> GetAll();
        FilmModelDTO? GetById(int id);
        List<FilmModelDTO> Search(string query);
        void AddToWatchingList(int userId, int FilmModelId);
        void AddToWatchedList(int userId, int FilmModelId);
        List<FilmModelDTO> GetWatchingList(int userId);
        List<FilmModelDTO> GetWatchedList(int userId);
        List<FilmModelDTO> GetTop10Trending();
    }
}