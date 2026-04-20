using System.Collections.Generic;
using DAL.DTO;
using DAL.Repositories;
using serviceLibary.Models;

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
            var dtos = _repository.GetAll();
            var models = new List<GenreModel>();
            foreach (var dto in dtos)
            {
                models.Add(new GenreModel(dto.Id, dto.Naam));
            }
            return models;
        }

        public GenreModel GetById(int id)
        {
            GenreDTO dto = _repository.GetById(id);
            if (dto == null)
                return null;
            return new GenreModel(dto.Id, dto.Naam);
        }

        public List<string> GetByFilmId(int filmId)
        {
            var dtos = _repository.GetByFilmId(filmId);
            var namen = new List<string>();
            foreach (var dto in dtos)
            {
                namen.Add(dto.Naam);
            }
            return namen;
        }

        public List<string> GetBySerieId(int serieId)
        {
            var dtos = _repository.GetBySerieId(serieId);
            var namen = new List<string>();
            foreach (var dto in dtos)
            {
                namen.Add(dto.Naam);
            }
            return namen;
        }
    }
}