using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Services;
using unit_test.FakeRepositories;

namespace unit_test.ServiceTesten
{
    [TestClass]
    public class GenreServiceTest
    {
        // Happy flow
        [TestMethod]
        public void GetAll_ReturnsAllGenres()
        {
            var service = new GenreService(new FakeGenreRepository());
            var result = service.GetAll();
            Assert.HasCount(2, result);
        }

        [TestMethod]
        public void GetById_ReturnsCorrectGenre()
        {
            var service = new GenreService(new FakeGenreRepository());
            var result = service.GetById(1);
            Assert.IsNotNull(result);
            Assert.AreEqual("Thriller", result.Naam);
        }

        [TestMethod]
        public void GetById_ReturnsNull_WhenGenreNotFound()
        {
            var service = new GenreService(new FakeGenreRepository());
            var result = service.GetById(99);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetByFilmId_ReturnsGenresForFilm()
        {
            var service = new GenreService(new FakeGenreRepository());
            var result = service.GetByFilmId(1);
            Assert.IsNotNull(result);
            Assert.HasCount(1, result);
            Assert.AreEqual("Thriller", result[0]);
        }

        [TestMethod]
        public void GetBySerieId_ReturnsGenresForSerie()
        {
            var service = new GenreService(new FakeGenreRepository());
            var result = service.GetBySerieId(1);
            Assert.IsNotNull(result);
            Assert.HasCount(1, result);
            Assert.AreEqual("Drama", result[0]);
        }

        // Uitzonderingen
        [TestMethod]
        public void GetAll_ReturnsEmptyList_WhenGeenGenres()
        {
            var repo = new FakeGenreRepository();
            repo.SimuleerLegeDatabase = true;
            var service = new GenreService(repo);
            var result = service.GetAll();
            Assert.HasCount(0, result);
        }

        [TestMethod]
        public void GetById_ReturnsNull_WhenGeenGenre()
        {
            var repo = new FakeGenreRepository();
            repo.SimuleerLegeDatabase = true;
            var service = new GenreService(repo);
            var result = service.GetById(1);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetByFilmId_ReturnsEmptyList_WhenGeenGenres()
        {
            var repo = new FakeGenreRepository();
            repo.SimuleerGeenResultaten = true;
            var service = new GenreService(repo);
            var result = service.GetByFilmId(1);
            Assert.HasCount(0, result);
        }

        [TestMethod]
        public void GetBySerieId_ReturnsEmptyList_WhenGeenGenres()
        {
            var repo = new FakeGenreRepository();
            repo.SimuleerGeenResultaten = true;
            var service = new GenreService(repo);
            var result = service.GetBySerieId(1);
            Assert.HasCount(0, result);
        }
    }
}