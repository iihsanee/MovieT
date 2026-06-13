using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Services;
using unit_test.FakeRepositories;

namespace unit_test.ServiceTesten
{
    [TestClass]
    public class FilmServiceTest
    {
        // Happy flow
        [TestMethod]
        public void GetAll_ReturnsAllFilms()
        {
            var service = new FilmService(new FakeFilmRepository());
            var result = service.GetAll();
            Assert.HasCount(2, result);
        }

        [TestMethod]
        public void GetById_ReturnsCorrectFilm()
        {
            var service = new FilmService(new FakeFilmRepository());
            var result = service.GetById(1);
            Assert.IsNotNull(result);
            Assert.AreEqual("Inception", result.Title);
        }

        [TestMethod]
        public void GetById_ReturnsNull_WhenFilmNotFound()
        {
            var service = new FilmService(new FakeFilmRepository());
            var result = service.GetById(99);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Search_ReturnsMatchingFilms()
        {
            var service = new FilmService(new FakeFilmRepository());
            var result = service.Search("Inc");
            Assert.HasCount(1, result);
            Assert.AreEqual("Inception", result[0].Title);
        }

        [TestMethod]
        public void GetWatchingList_ReturnsFilms()
        {
            var service = new FilmService(new FakeFilmRepository());
            var result = service.GetWatchingList(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetWatchedList_ReturnsFilms()
        {
            var service = new FilmService(new FakeFilmRepository());
            var result = service.GetWatchedList(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTop10Trending_ReturnsFilms()
        {
            var service = new FilmService(new FakeFilmRepository());
            var result = service.GetTop10Trending();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AddToWatchingList_DoesNotThrowException()
        {
            var service = new FilmService(new FakeFilmRepository());
            try { service.AddToWatchingList(1, 1); }
            catch { Assert.Fail("AddToWatchingList gooide een exception"); }
        }

        [TestMethod]
        public void AddToWatchedList_DoesNotThrowException()
        {
            var service = new FilmService(new FakeFilmRepository());
            try { service.AddToWatchedList(1, 1); }
            catch { Assert.Fail("AddToWatchedList gooide een exception"); }
        }

        // Uitzonderingen
        [TestMethod]
        public void GetAll_ReturnsEmptyList_WhenGeenFilms()
        {
            var repo = new FakeFilmRepository();
            repo.SimuleerLegeDatabase = true;
            var service = new FilmService(repo);
            var result = service.GetAll();
            Assert.HasCount(0, result);
        }

        [TestMethod]
        public void GetById_ReturnsNull_WhenGeenFilm()
        {
            var repo = new FakeFilmRepository();
            repo.SimuleerLegeDatabase = true;
            var service = new FilmService(repo);
            var result = service.GetById(1);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Search_ReturnsEmptyList_WhenGeenResultaten()
        {
            var repo = new FakeFilmRepository();
            repo.SimuleerGeenResultaten = true;
            var service = new FilmService(repo);
            var result = service.Search("test");
            Assert.HasCount(0, result);
        }

        [TestMethod]
        public void GetTop10Trending_ReturnsEmptyList_WhenGeenFilms()
        {
            var repo = new FakeFilmRepository();
            repo.SimuleerLegeDatabase = true;
            var service = new FilmService(repo);
            var result = service.GetTop10Trending();
            Assert.HasCount(0, result);
        }
    }
}