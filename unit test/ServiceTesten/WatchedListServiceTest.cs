using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Services;
using unit_test.FakeRepositories;

namespace unit_test.ServiceTesten
{
    [TestClass]
    public class WatchedListServiceTest
    {
        [TestMethod]
        public void GetByUser_ReturnsItemsForUser()
        {
            var repo = new FakeWatchedListRepository();
            var service = new WatchedListService(repo);
            var result = service.GetByUser(1);
            Assert.IsNotNull(result);
            Assert.HasCount(2, result);
        }

        [TestMethod]
        public void GetByUser_ReturnsEmptyList_WhenUserHasNoItems()
        {
            var repo = new FakeWatchedListRepository();
            var service = new WatchedListService(repo);
            var result = service.GetByUser(99);
            Assert.IsNotNull(result);
            Assert.HasCount(0, result);
        }

        [TestMethod]
        public void Add_FilmToWatchedList_DoesNotThrowException()
        {
            var repo = new FakeWatchedListRepository();
            var service = new WatchedListService(repo);
            try { service.Add(1, 1, null); }
            catch { Assert.Fail("Add gooide een exception"); }
        }

        [TestMethod]
        public void Add_SerieToWatchedList_DoesNotThrowException()
        {
            var repo = new FakeWatchedListRepository();
            var service = new WatchedListService(repo);
            try { service.Add(1, null, 1); }
            catch { Assert.Fail("Add gooide een exception"); }
        }
    }
}