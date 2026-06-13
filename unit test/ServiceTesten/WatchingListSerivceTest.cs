using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Services;
using unit_test.FakeRepositories;

namespace unit_test.ServiceTesten
{
    [TestClass]
    public class WatchingListServiceTest
    {
        // Happy flow
        [TestMethod]
        public void GetByUser_ReturnsItemsForUser()
        {
            var service = new WatchingListService(new FakeWatchingListRepository());
            var result = service.GetByUser(1);
            Assert.IsNotNull(result);
            Assert.HasCount(2, result);
        }

        [TestMethod]
        public void GetByUser_ReturnsEmptyList_WhenUserHasNoItems()
        {
            var service = new WatchingListService(new FakeWatchingListRepository());
            var result = service.GetByUser(99);
            Assert.IsNotNull(result);
            Assert.HasCount(0, result);
        }

        [TestMethod]
        public void Add_FilmToWatchingList_DoesNotThrowException()
        {
            var service = new WatchingListService(new FakeWatchingListRepository());
            try { service.Add(1, 1, null); }
            catch { Assert.Fail("Add gooide een exception"); }
        }

        [TestMethod]
        public void Add_SerieToWatchingList_DoesNotThrowException()
        {
            var service = new WatchingListService(new FakeWatchingListRepository());
            try { service.Add(1, null, 1); }
            catch { Assert.Fail("Add gooide een exception"); }
        }

        // Uitzonderingen
        [TestMethod]
        public void GetByUser_ReturnsEmptyList_WhenGeenItems()
        {
            var repo = new FakeWatchingListRepository();
            repo.SimuleerLegeDatabase = true;
            var service = new WatchingListService(repo);
            var result = service.GetByUser(1);
            Assert.HasCount(0, result);
        }


        }
    }