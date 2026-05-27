using Interfaces.Interfaces;
using serviceLibary.Models;
using DAL.DTO;

namespace serviceLibary.Services
{
    public class GenreService
    {
        private readonly IGenreRepository _repository;

        public GenreService(IGenreRepository repository)
        {
            _repository = repository;
        }

        public List<GenreModel> GetAll()
        {
            return _repository.GetAll()
                .Select(dto => MapGenre(dto))
                .ToList();
        }

        public GenreModel? GetById(int id)
        {
            var dto = _repository.GetById(id);
            if (dto == null) return null;
            return MapGenre(dto);
        }

        public List<string> GetByFilmId(int filmId)
        {
            return _repository.GetByFilmId(filmId)
                .Select(dto => dto.Naam)
                .ToList();
        }

        public List<string> GetBySerieId(int serieId)
        {
            return _repository.GetBySerieId(serieId)
                .Select(dto => dto.Naam)
                .ToList();
        }

        private GenreModel MapGenre(GenreDTO dto)
        {
            return new GenreModel(
                id: dto.Id,
                naam: dto.Naam
            );
        }
    }
}