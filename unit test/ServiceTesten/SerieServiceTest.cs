using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Services;
using unit_test.FakeRepositories;

namespace unit_test.ServiceTesten
{
    [TestClass]
    public class SerieServiceTest
    {
        // Happy flow
        [TestMethod]
        public void GetAll_ReturnsAllSeries()
        {
            var service = new SerieService(new FakeSerieRepository());
            var result = service.GetAll();
            Assert.HasCount(2, result);
        }

        [TestMethod]
        public void GetById_ReturnsCorrectSerie()
        {
            var service = new SerieService(new FakeSerieRepository());
            var result = service.GetById(1);
            Assert.IsNotNull(result);
            Assert.AreEqual("Breaking Bad", result.Title);
        }

        [TestMethod]
        public void GetById_ReturnsNull_WhenSerieNotFound()
        {
            var service = new SerieService(new FakeSerieRepository());
            var result = service.GetById(99);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Search_ReturnsMatchingSeries()
        {
            var service = new SerieService(new FakeSerieRepository());
            var result = service.Search("Break");
            Assert.HasCount(1, result);
            Assert.AreEqual("Breaking Bad", result[0].Title);
        }

        [TestMethod]
        public void GetWatchingList_ReturnsSeries()
        {
            var service = new SerieService(new FakeSerieRepository());
            var result = service.GetWatchingList(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetWatchedList_ReturnsSeries()
        {
            var service = new SerieService(new FakeSerieRepository());
            var result = service.GetWatchedList(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTop10Trending_ReturnsSeries()
        {
            var service = new SerieService(new FakeSerieRepository());
            var result = service.GetTop10Trending();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AddToWatchingList_DoesNotThrowException()
        {
            var service = new SerieService(new FakeSerieRepository());
            try { service.AddToWatchingList(1, 1); }
            catch { Assert.Fail("AddToWatchingList gooide een exception"); }
        }

        [TestMethod]
        public void AddToWatchedList_DoesNotThrowException()
        {
            var service = new SerieService(new FakeSerieRepository());
            try { service.AddToWatchedList(1, 1); }
            catch { Assert.Fail("AddToWatchedList gooide een exception"); }
        }

        // Uitzonderingen
        [TestMethod]
        public void GetAll_ReturnsEmptyList_WhenGeenSeries()
        {
            var repo = new FakeSerieRepository();
            repo.SimuleerLegeDatabase = true;
            var service = new SerieService(repo);
            var result = service.GetAll();
            Assert.HasCount(0, result);
        }

        [TestMethod]
        public void GetById_ReturnsNull_WhenGeenSerie()
        {
            var repo = new FakeSerieRepository();
            repo.SimuleerLegeDatabase = true;
            var service = new SerieService(repo);
            var result = service.GetById(1);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Search_ReturnsEmptyList_WhenGeenResultaten()
        {
            var repo = new FakeSerieRepository();
            repo.SimuleerGeenResultaten = true;
            var service = new SerieService(repo);
            var result = service.Search("test");
            Assert.HasCount(0, result);
        }

        [TestMethod]
        public void GetTop10Trending_ReturnsEmptyList_WhenGeenSeries()
        {
            var repo = new FakeSerieRepository();
            repo.SimuleerLegeDatabase = true;
            var service = new SerieService(repo);
            var result = service.GetTop10Trending();
            Assert.HasCount(0, result);
        }
    }
}