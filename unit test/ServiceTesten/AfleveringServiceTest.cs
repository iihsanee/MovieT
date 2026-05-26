using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Services;
using unit_test.FakeRepositories;

namespace unit_test.ServiceTesten
{
    [TestClass]
    public class AfleveringServiceTest
    {
        [TestMethod]
        public void GetBySeizoenId_ReturnsCorrectAfleveringen()
        {
            var repo = new FakeAfleveringRepository();
            var service = new AfleveringService(repo);

            var result = service.GetBySeizoenId(1);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Pilot", result[0].Titel);
        }

        [TestMethod]
        public void GetBySeizoenId_ReturnsEmpty_WhenSeizoenNotFound()
        {
            var repo = new FakeAfleveringRepository();
            var service = new AfleveringService(repo);

            var result = service.GetBySeizoenId(99);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetById_ReturnsCorrectAflevering()
        {
            var repo = new FakeAfleveringRepository();
            var service = new AfleveringService(repo);

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
            var repo = new FakeAfleveringRepository();
            var service = new AfleveringService(repo);

            var result = service.GetById(99);

            Assert.IsNull(result);
        }
    }
}