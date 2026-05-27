using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Services;
using unit_test.FakeRepositories;

namespace unit_test.ServiceExeptionTests
{
    [TestClass]
    public class WatchingListServiceExceptionTest
    {
        [TestMethod]
        public void GetByUser_ThrowsException_WhenDatabaseFout()
        {
            var repo = new FakeWatchingListRepositoryException();
            var service = new WatchingListService(repo);
            try { service.GetByUser(1); Assert.Fail("Geen exception gegooid"); }
            catch (Exception) { }
        }

        [TestMethod]
        public void Add_ThrowsException_WhenDatabaseFout()
        {
            var repo = new FakeWatchingListRepositoryException();
            var service = new WatchingListService(repo);
            try { service.Add(1, 1, null); Assert.Fail("Geen exception gegooid"); }
            catch (Exception) { }
        }
    }
}