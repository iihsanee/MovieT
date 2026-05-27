using Interfaces.Interfaces;
using serviceLibary.Models;
using DAL.DTO;

namespace serviceLibary.Services
{
    public class SerieService
    {
        private readonly ISerieRepository _repository;

        public SerieService(ISerieRepository repository)
        {
            _repository = repository;
        }

        public SerieModel? GetById(int id)
        {
            var dto = _repository.GetById(id);
            if (dto == null) return null;
            return MapSerie(dto);
        }

        public List<SerieModel> GetAll()
        {
            return _repository.GetAll()
                .Select(dto => MapSerie(dto))
                .ToList();
        }

        public List<SerieModel> Search(string query)
        {
            return _repository.Search(query)
                .Select(dto => MapSerie(dto))
                .ToList();
        }

        public void AddToWatchingList(int userId, int serieId)
        {
            _repository.AddToWatchingList(userId, serieId);
        }

        public void AddToWatchedList(int userId, int serieId)
        {
            _repository.AddToWatchedList(userId, serieId);
        }

        public List<SerieModel> GetWatchingList(int userId)
        {
            return _repository.GetWatchingList(userId)
                .Select(dto => MapSerie(dto))
                .ToList();
        }

        public List<SerieModel> GetWatchedList(int userId)
        {
            return _repository.GetWatchedList(userId)
                .Select(dto => MapSerie(dto))
                .ToList();
        }

        public List<SerieModel> GetTop10Trending()
        {
            return _repository.GetTop10Trending()
                .Select(dto => MapSerie(dto))
                .ToList();
        }

        private SerieModel MapSerie(SerieDTO dto)
        {
            return new SerieModel(
                id: dto.Id,
                title: dto.Title,
                releaseDate: dto.ReleaseDate,
                duration: dto.Duration,
                description: dto.Description
            );
        }
    }
}