using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL.DTO;
using DAL.Repositories;
using serviceLibary.Services;
using System;
using System.Collections.Generic;

namespace unit_test
{
    // Nep repository zonder echte database
    public class FakeFilmRepository : IFilmModelRepository
    {
        private List<FilmModelDTO> _films = new List<FilmModelDTO>
        {
            new FilmModelDTO { Id = 1, Title = "Inception", Description = "Test", ReleaseDate = new DateTime(2010, 7, 16), Duration = TimeSpan.FromHours(2) },
            new FilmModelDTO { Id = 2, Title = "The Matrix", Description = "Test", ReleaseDate = new DateTime(1999, 3, 31), Duration = TimeSpan.FromHours(2) }
        };

        public List<FilmModelDTO> GetAll() => _films;

        public FilmModelDTO GetById(int id) => _films.Find(f => f.Id == id);

        public List<FilmModelDTO> Search(string query) => _films.FindAll(f => f.Title.Contains(query));

        public void AddToWatchingList(int userId, int filmId) { }
        public void AddToWatchedList(int userId, int filmId) { }
        public List<FilmModelDTO> GetWatchingList(int userId) => new List<FilmModelDTO>();
        public List<FilmModelDTO> GetWatchedList(int userId) => new List<FilmModelDTO>();
        public List<FilmModelDTO> GetTop10Trending() => new List<FilmModelDTO>();
    }

    [TestClass]
    public class FilmServiceTest
    {
        [TestMethod]
        public void GetAll_ReturnsAllFilms()
        {
            // Arrange
            var repo = new FakeFilmRepository();
            var service = new FilmModel(repo);

            // Act
            var result = service.GetAll();

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetById_ReturnsCorrectFilm()
        {
            // Arrange
            var repo = new FakeFilmRepository();
            var service = new FilmModel(repo);

            // Act
            var result = service.GetById(1);

            // Assert
            Assert.AreEqual("Inception", result.Title);
        }

        [TestMethod]
        public void GetById_ReturnsNull_WhenFilmNotFound()
        {
            // Arrange
            var repo = new FakeFilmRepository();
            var service = new FilmModel(repo);

            // Act
            var result = service.GetById(99);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Search_ReturnsMatchingFilms()
        {
            // Arrange
            var repo = new FakeFilmRepository();
            var service = new FilmModel(repo);

            // Act
            var result = service.Search("Inc");

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Inception", result[0].Title);
        }
    }
}
