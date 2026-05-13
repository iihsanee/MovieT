using System.Collections.Generic;
using Interfaces.Interfaces;
using serviceLibary.Models;

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
            var dtos = _repository.GetByUser(userId);
            var models = new List<WatchingListModel>();
            foreach (var dto in dtos)
            {
                models.Add(new WatchingListModel(
                    dto.UserId,
                    dto.FilmId,
                    dto.SerieId,
                    dto.Title,
                    dto.Type
                ));
            }
            return models;
        }

        public void Add(int userId, int? filmId, int? serieId)
        {
            _repository.Add(userId, filmId, serieId);
        }
    }
}