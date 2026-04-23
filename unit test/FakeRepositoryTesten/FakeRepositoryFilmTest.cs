using DAL.DTO;
using DAL.Repositories;
using System;
using System.Collections.Generic;

namespace unit_test.FakeRepositories
{
    public class FakeFilmRepository : IFilmModelRepository
    {
        private List<FilmModelDTO> _films = new List<FilmModelDTO>
        {
            new FilmModelDTO(1, "Inception", new DateTime(2010, 7, 16), TimeSpan.FromHours(2), "Test"),
            new FilmModelDTO(2, "The Matrix", new DateTime(1999, 3, 31), TimeSpan.FromHours(2), "Test")
        };

        public List<FilmModelDTO> GetAll() => _films;

        public FilmModelDTO? GetById(int id) => _films.Find(f => f.Id == id);

        public List<FilmModelDTO> Search(string query) => _films.FindAll(f => f.Title.Contains(query));

        public void AddToWatchingList(int userId, int filmId) { }

        public void AddToWatchedList(int userId, int filmId) { }

        public List<FilmModelDTO> GetWatchingList(int userId) => _films;

        public List<FilmModelDTO> GetWatchedList(int userId) => _films;

        public List<FilmModelDTO> GetTop10Trending() => _films;
    }
}