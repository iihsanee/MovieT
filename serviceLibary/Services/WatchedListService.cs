using System.Collections.Generic;
using DAL.Repositories;
using serviceLibary.Models;

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
            var dtos = _repository.GetByUser(userId);
            var models = new List<WatchedListModel>();
            foreach (var dto in dtos)
            {
                models.Add(new WatchedListModel(
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
