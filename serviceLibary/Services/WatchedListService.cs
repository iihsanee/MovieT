using Interfaces.Interfaces;
using serviceLibary.Models;
using DAL.DTO;

namespace serviceLibary.Services
{
    public class WatchedListService
    {
        private readonly IWatchedListRepository _repository;

        public WatchedListService(IWatchedListRepository repository)
        {
            _repository = repository;
        }

        public List<WatchedListModel> GetByUser(int userId)
        {
            return _repository.GetByUser(userId)
                .Select(dto => MapWatchedList(dto))
                .ToList();
        }

        public void Add(int userId, int? filmId, int? serieId)
        {
            _repository.Add(userId, filmId, serieId);
        }

        public void Remove(int userId, int? filmId, int? serieId)
        {
            _repository.Remove(userId, filmId, serieId);
        }

        private WatchedListModel MapWatchedList(WatchedListDTO dto)
        {
            return new WatchedListModel(
                userId: dto.UserId,
                filmId: dto.FilmId,
                serieId: dto.SerieId,
                title: dto.Title,
                type: dto.Type
            );
        }
    }
}