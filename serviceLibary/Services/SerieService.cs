using System.Collections.Generic;
using DAL.DTO;
using DAL.Repositories;
using serviceLibary.Models;

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
            SerieDTO? dto = _repository.GetById(id);
            if (dto == null)
                return null;
            return new SerieModel(
                dto.Id,
                dto.Title,
                dto.ReleaseDate,
                dto.Duration,
                dto.Description
            );
        }

        public List<SerieModel> GetAll()
        {
            var dtos = _repository.GetAll();
            var models = new List<SerieModel>();
            foreach (var dto in dtos)
            {
                models.Add(new SerieModel(
                    dto.Id,
                    dto.Title,
                    dto.ReleaseDate,
                    dto.Duration,
                    dto.Description
                ));
            }
            return models;
        }

        public List<SerieModel> Search(string query)
        {
            var dtos = _repository.Search(query);
            var models = new List<SerieModel>();
            foreach (var dto in dtos)
            {
                models.Add(new SerieModel(
                    dto.Id,
                    dto.Title,
                    dto.ReleaseDate,
                    dto.Duration,
                    dto.Description
                ));
            }
            return models;
        }

        public void AddToWatchingList(int userId, int SerieId)
        {
            _repository.AddToWatchingList(userId, SerieId);
        }

        public void AddToWatchedList(int userId, int SerieId)
        {
            _repository.AddToWatchedList(userId, SerieId);
        }

        public List<SerieModel> GetWatchingList(int userId)
        {
            var dtos = _repository.GetWatchingList(userId);
            var models = new List<SerieModel>();
            foreach (var dto in dtos)
            {
                models.Add(new SerieModel(
                    dto.Id,
                    dto.Title,
                    dto.ReleaseDate,
                    dto.Duration,
                    dto.Description
                ));
            }
            return models;
        }

        public List<SerieModel> GetWatchedList(int userId)
        {
            var dtos = _repository.GetWatchedList(userId);
            var models = new List<SerieModel>();
            foreach (var dto in dtos)
            {
                models.Add(new SerieModel(
                    dto.Id,
                    dto.Title,
                    dto.ReleaseDate,
                    dto.Duration,
                    dto.Description
                ));
            }
            return models;
        }

        public List<SerieModel> GetTop10Trending()
        {
            var dtos = _repository.GetTop10Trending();
            var models = new List<SerieModel>();
            foreach (var dto in dtos)
            {
                models.Add(new SerieModel(
                    dto.Id,
                    dto.Title,
                    dto.ReleaseDate,
                    dto.Duration,
                    dto.Description
                ));
            }
            return models;
        }
    }
}