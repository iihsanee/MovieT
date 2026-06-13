using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Services;
using unit_test.FakeRepositories;

namespace unit_test.ServiceTesten
{
    [TestClass]
    public class AfleveringServiceTest
    {
        // Happy flow
        [TestMethod]
        public void GetBySeizoenId_ReturnsCorrectAfleveringen()
        {
            var service = new AfleveringService(new FakeAfleveringRepository());
            var result = service.GetBySeizoenId(1);
            Assert.HasCount(2, result);
            Assert.AreEqual("Pilot", result[0].Titel);
        }

        [TestMethod]
        public void GetBySeizoenId_ReturnsEmpty_WhenSeizoenNotFound()
        {
            var service = new AfleveringService(new FakeAfleveringRepository());
            var result = service.GetBySeizoenId(99);
            Assert.HasCount(0, result);
        }

        [TestMethod]
        public void GetById_ReturnsCorrectAflevering()
        {
            var service = new AfleveringService(new FakeAfleveringRepository());
            var result = service.GetById(1);
            Assert.IsNotNull(result);
            Assert.AreEqual("Pilot", result.Titel);
            Assert.AreEqual(1, result.SeizoenId);
            Assert.AreEqual(1, result.Afleveringsnummer);
            Assert.AreEqual(45, result.Duurtijd);
        }

        [TestMethod]
        public void GetById_ReturnsNull_WhenAfleveringNotFound()
        {
            var service = new AfleveringService(new FakeAfleveringRepository());
            var result = service.GetById(99);
            Assert.IsNull(result);
        }

        // Uitzonderingen
        [TestMethod]
        public void GetBySeizoenId_ReturnsEmptyList_WhenGeenAfleveringen()
        {
            var repo = new FakeAfleveringRepository();
            repo.SimuleerLegeDatabase = true;
            var service = new AfleveringService(repo);
            var result = service.GetBySeizoenId(1);
            Assert.HasCount(0, result);
        }

        [TestMethod]
        public void GetById_ReturnsNull_WhenGeenAflevering()
        {
            var repo = new FakeAfleveringRepository();
            repo.SimuleerLegeDatabase = true;
            var service = new AfleveringService(repo);
            var result = service.GetById(1);
            Assert.IsNull(result);
        }
    }
}