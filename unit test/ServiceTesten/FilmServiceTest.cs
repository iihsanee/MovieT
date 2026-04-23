using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Services;
using unit_test.FakeRepositories;

namespace unit_test.ServiceTesten
{
    [TestClass]
    public class FilmServiceTest
    {
        [TestMethod]
        public void GetAll_ReturnsAllFilms()
        {
            var repo = new FakeFilmRepository();
            var service = new FilmModel(repo);
            var result = service.GetAll();
            Assert.HasCount(2, result);
        }

        [TestMethod]
        public void GetById_ReturnsCorrectFilm()
        {
            var repo = new FakeFilmRepository();
            var service = new FilmModel(repo);
            var result = service.GetById(1);
            Assert.IsNotNull(result);
            Assert.AreEqual("Inception", result.Title);
        }

        [TestMethod]
        public void GetById_ReturnsNull_WhenFilmNotFound()
        {
            var repo = new FakeFilmRepository();
            var service = new FilmModel(repo);
            var result = service.GetById(99);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Search_ReturnsMatchingFilms()
        {
            var repo = new FakeFilmRepository();
            var service = new FilmModel(repo);
            var result = service.Search("Inc");
            Assert.HasCount(1, result);
            Assert.AreEqual("Inception", result[0].Title);
        }

        [TestMethod]
        public void GetWatchingList_ReturnsFilmsInWatchingList()
        {
            var repo = new FakeFilmRepository();
            var service = new FilmModel(repo);
            var result = service.GetWatchingList(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetWatchedList_ReturnsFilmsInWatchedList()
        {
            var repo = new FakeFilmRepository();
            var service = new FilmModel(repo);
            var result = service.GetWatchedList(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTop10Trending_ReturnsTrendingFilms()
        {
            var repo = new FakeFilmRepository();
            var service = new FilmModel(repo);
            var result = service.GetTop10Trending();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AddToWatchingList_DoesNotThrowException()
        {
            var repo = new FakeFilmRepository();
            var service = new FilmModel(repo);
            try { service.AddToWatchingList(1, 1); }
            catch { Assert.Fail("AddToWatchingList gooide een exception"); }
        }

        [TestMethod]
        public void AddToWatchedList_DoesNotThrowException()
        {
            var repo = new FakeFilmRepository();
            var service = new FilmModel(repo);
            try { service.AddToWatchedList(1, 1); }
            catch { Assert.Fail("AddToWatchedList gooide een exception"); }
        }
    }
}