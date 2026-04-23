using DAL.DTO;
using DAL.Repositories;
using System;
using System.Collections.Generic;

namespace unit_test.FakeRepositories
{
    public class FakeSerieRepository : ISerieRepository
    {
        private List<SerieDTO> _series = new List<SerieDTO>
        {
            new SerieDTO(1, "Breaking Bad", new DateTime(2008, 1, 20), TimeSpan.FromMinutes(47), "Test"),
            new SerieDTO(2, "Stranger Things", new DateTime(2016, 7, 15), TimeSpan.FromMinutes(51), "Test")
        };
        public List<SerieDTO> GetAll() => _series;
        public SerieDTO? GetById(int id) => _series.Find(s => s.Id == id);
        public List<SerieDTO> Search(string query) => _series.FindAll(s => s.Title.Contains(query));
        public void AddToWatchingList(int userId, int serieId) { }
        public void AddToWatchedList(int userId, int serieId) { }
        public List<SerieDTO> GetWatchingList(int userId) => new List<SerieDTO>();
        public List<SerieDTO> GetWatchedList(int userId) => new List<SerieDTO>();
        public List<SerieDTO> GetTop10Trending() => new List<SerieDTO>();
    }
}