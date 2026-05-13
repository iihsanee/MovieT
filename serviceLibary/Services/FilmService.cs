using System.Collections.Generic;
using DAL.DTO;
using Interfaces.Interfaces;    
using serviceLibary.Models;

namespace serviceLibary.Services
{
    public class FilmModel
    {
        private readonly IFilmModelRepository _repository;

        public FilmModel(IFilmModelRepository repository)
        {
            _repository = repository;
        }

        public FilmModelModel? GetById(int id)
        {
            FilmModelDTO? dto = _repository.GetById(id);
            if (dto == null)
                return null;
            return new FilmModelModel(
                dto.Id,
                dto.Title,
                dto.ReleaseDate,
                dto.Duration,
                dto.Description
            );
        }

        public List<FilmModelModel> GetAll()
        {
            var dtos = _repository.GetAll();
            var models = new List<FilmModelModel>();
            foreach (var dto in dtos)
            {
                models.Add(new FilmModelModel(
                    dto.Id,
                    dto.Title,
                    dto.ReleaseDate,
                    dto.Duration,
                    dto.Description
                ));
            }
            return models;
        }

        public List<FilmModelModel> Search(string query)
        {
            var dtos = _repository.Search(query);
            var models = new List<FilmModelModel>();
            foreach (var dto in dtos)
            {
                models.Add(new FilmModelModel(
                    dto.Id,
                    dto.Title,
                    dto.ReleaseDate,
                    dto.Duration,
                    dto.Description
                ));
            }
            return models;
        }

        public void AddToWatchingList(int userId, int FilmModelId)
        {
            _repository.AddToWatchingList(userId, FilmModelId);
        }

        public void AddToWatchedList(int userId, int FilmModelId)
        {
            _repository.AddToWatchedList(userId, FilmModelId);
        }

        public List<FilmModelModel> GetWatchingList(int userId)
        {
            var dtos = _repository.GetWatchingList(userId);
            var models = new List<FilmModelModel>();
            foreach (var dto in dtos)
            {
                models.Add(new FilmModelModel(
                    dto.Id,
                    dto.Title,
                    dto.ReleaseDate,
                    dto.Duration,
                    dto.Description
                ));
            }
            return models;
        }

        public List<FilmModelModel> GetWatchedList(int userId)
        {
            var dtos = _repository.GetWatchedList(userId);
            var models = new List<FilmModelModel>();
            foreach (var dto in dtos)
            {
                models.Add(new FilmModelModel(
                    dto.Id,
                    dto.Title,
                    dto.ReleaseDate,
                    dto.Duration,
                    dto.Description
                ));
            }
            return models;
        }

        public List<FilmModelModel> GetTop10Trending()
        {
            var dtos = _repository.GetTop10Trending();
            var models = new List<FilmModelModel>();
            foreach (var dto in dtos)
            {
                models.Add(new FilmModelModel(
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
