using Interfaces.Interfaces;
using serviceLibary.Models;
using DAL.DTO;

namespace serviceLibary.Services
{
    public class WatchingListService
    {
        private readonly IWatchingListRepository _repository;

        public WatchingListService(IWatchingListRepository repository)
        {
            _repository = repository;
        }

        public List<WatchingListModel> GetByUser(int userId)
        {
            return _repository.GetByUser(userId)
                .Select(dto => MapWatchingList(dto))
                .ToList();
        }

        public void Add(int userId, int? filmId, int? serieId)
        {
            _repository.Add(userId, filmId, serieId);
        }

        private WatchingListModel MapWatchingList(WatchingListDTO dto)
        {
            return new WatchingListModel(
                userId: dto.UserId,
                filmId: dto.FilmId,
                serieId: dto.SerieId,
                title: dto.Title,
                type: dto.Type
            );
        }
    }
}