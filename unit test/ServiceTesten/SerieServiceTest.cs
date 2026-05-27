using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Services;
using unit_test.FakeRepositories;

namespace unit_test.ServiceTesten
{
    [TestClass]
    public class SerieServiceTest
    {
        [TestMethod]
        public void GetAll_ReturnsAllSeries()
        {
            var repo = new FakeSerieRepository();
            var service = new SerieService(repo);
            var result = service.GetAll();
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetById_ReturnsCorrectSerie()
        {
            var repo = new FakeSerieRepository();
            var service = new SerieService(repo);
            var result = service.GetById(1);
            Assert.IsNotNull(result);
            Assert.AreEqual("Breaking Bad", result.Title);
        }

        [TestMethod]
        public void GetById_ReturnsNull_WhenSerieNotFound()
        {
            var repo = new FakeSerieRepository();
            var service = new SerieService(repo);
            var result = service.GetById(99);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Search_ReturnsMatchingSeries()
        {
            var repo = new FakeSerieRepository();
            var service = new SerieService(repo);
            var result = service.Search("Break");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Breaking Bad", result[0].Title);
        }

        [TestMethod]
        public void GetWatchingList_ReturnsSeriesInWatchingList()
        {
            var repo = new FakeSerieRepository();
            var service = new SerieService(repo);
            var result = service.GetWatchingList(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetWatchedList_ReturnsSeriesInWatchedList()
        {
            var repo = new FakeSerieRepository();
            var service = new SerieService(repo);
            var result = service.GetWatchedList(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTop10Trending_ReturnsTrendingSeries()
        {
            var repo = new FakeSerieRepository();
            var service = new SerieService(repo);
            var result = service.GetTop10Trending();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AddToWatchingList_DoesNotThrowException()
        {
            var repo = new FakeSerieRepository();
            var service = new SerieService(repo);
            try { service.AddToWatchingList(1, 1); }
            catch { Assert.Fail("AddToWatchingList gooide een exception"); }
        }

        [TestMethod]
        public void AddToWatchedList_DoesNotThrowException()
        {
            var repo = new FakeSerieRepository();
            var service = new SerieService(repo);
            try { service.AddToWatchedList(1, 1); }
            catch { Assert.Fail("AddToWatchedList gooide een exception"); }
        }
    }
}