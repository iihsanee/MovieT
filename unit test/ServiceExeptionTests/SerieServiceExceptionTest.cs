using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Services;
using unit_test.FakeRepositories;

namespace unit_test.ServiceExeptionTests
{
    [TestClass]
    public class SerieServiceExceptionTest
    {
        [TestMethod]
        public void GetAll_ThrowsException_WhenDatabaseFout()
        {
            var repo = new FakeSerieRepositoryException();
            var service = new SerieService(repo);
            try { service.GetAll(); Assert.Fail("Geen exception gegooid"); }
            catch (Exception) { }
        }

        [TestMethod]
        public void GetById_ThrowsException_WhenDatabaseFout()
        {
            var repo = new FakeSerieRepositoryException();
            var service = new SerieService(repo);
            try { service.GetById(1); Assert.Fail("Geen exception gegooid"); }
            catch (Exception) { }
        }

        [TestMethod]
        public void Search_ThrowsException_WhenDatabaseFout()
        {
            var repo = new FakeSerieRepositoryException();
            var service = new SerieService(repo);
            try { service.Search("test"); Assert.Fail("Geen exception gegooid"); }
            catch (Exception) { }
        }

        [TestMethod]
        public void GetWatchingList_ThrowsException_WhenDatabaseFout()
        {
            var repo = new FakeSerieRepositoryException();
            var service = new SerieService(repo);
            try { service.GetWatchingList(1); Assert.Fail("Geen exception gegooid"); }
            catch (Exception) { }
        }

        [TestMethod]
        public void GetWatchedList_ThrowsException_WhenDatabaseFout()
        {
            var repo = new FakeSerieRepositoryException();
            var service = new SerieService(repo);
            try { service.GetWatchedList(1); Assert.Fail("Geen exception gegooid"); }
            catch (Exception) { }
        }

        [TestMethod]
        public void GetTop10Trending_ThrowsException_WhenDatabaseFout()
        {
            var repo = new FakeSerieRepositoryException();
            var service = new SerieService(repo);
            try { service.GetTop10Trending(); Assert.Fail("Geen exception gegooid"); }
            catch (Exception) { }
        }

        [TestMethod]
        public void AddToWatchingList_ThrowsException_WhenDatabaseFout()
        {
            var repo = new FakeSerieRepositoryException();
            var service = new SerieService(repo);
            try { service.AddToWatchingList(1, 1); Assert.Fail("Geen exception gegooid"); }
            catch (Exception) { }
        }

        [TestMethod]
        public void AddToWatchedList_ThrowsException_WhenDatabaseFout()
        {
            var repo = new FakeSerieRepositoryException();
            var service = new SerieService(repo);
            try { service.AddToWatchedList(1, 1); Assert.Fail("Geen exception gegooid"); }
            catch (Exception) { }
        }
    }
}