
using DAL.DTO;

namespace Interfaces.Interfaces
{
    public interface IGenreRepository
    {
        List<GenreDTO> GetAll();
        GenreDTO? GetById(int id);
        List<GenreDTO> GetByFilmId(int filmId);
        List<GenreDTO> GetBySerieId(int serieId);
    }
}