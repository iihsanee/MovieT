using System.Collections.Generic;
using DAL.DTO;

namespace DAL.Repositories
{
    public interface IGenreRepository
    {
        List<GenreDTO> GetAll();
        GenreDTO GetById(int id);
        List<GenreDTO> GetByFilmId(int filmId);
        List<GenreDTO> GetBySerieId(int serieId);
    }
}