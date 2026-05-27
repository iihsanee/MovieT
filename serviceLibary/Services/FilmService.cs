using Interfaces.Interfaces;
using serviceLibary.Models;
using DAL.DTO;

namespace serviceLibary.Services
{
    public class FilmService
    {
        private readonly IFilmModelRepository _repository;

        public FilmService(IFilmModelRepository repository)
        {
            _repository = repository;
        }

        public FilmModelModel? GetById(int id)
        {
            var dto = _repository.GetById(id);
            if (dto == null) return null;
            return MapFilm(dto);
        }

        public List<FilmModelModel> GetAll()
        {
            return _repository.GetAll()
                .Select(dto => MapFilm(dto))
                .ToList();
        }

        public List<FilmModelModel> Search(string query)
        {
            return _repository.Search(query)
                .Select(dto => MapFilm(dto))
                .ToList();
        }

        public void AddToWatchingList(int userId, int filmId)
        {
            _repository.AddToWatchingList(userId, filmId);
        }

        public void AddToWatchedList(int userId, int filmId)
        {
            _repository.AddToWatchedList(userId, filmId);
        }

        public List<FilmModelModel> GetWatchingList(int userId)
        {
            return _repository.GetWatchingList(userId)
                .Select(dto => MapFilm(dto))
                .ToList();
        }

        public List<FilmModelModel> GetWatchedList(int userId)
        {
            return _repository.GetWatchedList(userId)
                .Select(dto => MapFilm(dto))
                .ToList();
        }

        public List<FilmModelModel> GetTop10Trending()
        {
            return _repository.GetTop10Trending()
                .Select(dto => MapFilm(dto))
                .ToList();
        }

        private FilmModelModel MapFilm(FilmModelDTO dto)
        {
            return new FilmModelModel(
                id: dto.Id,
                title: dto.Title,
                releaseDate: dto.ReleaseDate,
                duration: dto.Duration,
                description: dto.Description
            );
        }
    }
}