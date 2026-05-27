using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Services;
using unit_test.FakeRepositories;

namespace unit_test.ServiceTesten
{
    [TestClass]
    public class GenreServiceTest
    {
        [TestMethod]
        public void GetAll_ReturnsAllGenres()
        {
            var repo = new FakeGenreRepository();
            var service = new GenreService(repo);
            var result = service.GetAll();
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetById_ReturnsCorrectGenre()
        {
            var repo = new FakeGenreRepository();
            var service = new GenreService(repo);
            var result = service.GetById(1);
            Assert.IsNotNull(result);
            Assert.AreEqual("Thriller", result.Naam);
        }

        [TestMethod]
        public void GetById_ReturnsNull_WhenGenreNotFound()
        {
            var repo = new FakeGenreRepository();
            var service = new GenreService(repo);
            var result = service.GetById(99);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetByFilmId_ReturnsGenresForFilm()
        {
            var repo = new FakeGenreRepository();
            var service = new GenreService(repo);
            var result = service.GetByFilmId(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Thriller", result[0]);
        }

        [TestMethod]
        public void GetBySerieId_ReturnsGenresForSerie()
        {
            var repo = new FakeGenreRepository();
            var service = new GenreService(repo);
            var result = service.GetBySerieId(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Drama", result[0]);
        }
    }
}