using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Services;
using unit_test.FakeRepositories;

namespace unit_test.ServiceTesten
{
    [TestClass]
    public class SeizoenServiceTest
    {
        // Happy flow
        [TestMethod]
        public void GetBySerieId_ReturnsCorrectSeizoenen()
        {
            var service = new SeizoenService(new FakeSeizoenRepository());
            var result = service.GetBySerieId(1);
            Assert.HasCount(2, result);
            Assert.AreEqual(1, result[0].Seizoennummer);
        }

        [TestMethod]
        public void GetBySerieId_ReturnsEmpty_WhenSerieNotFound()
        {
            var service = new SeizoenService(new FakeSeizoenRepository());
            var result = service.GetBySerieId(99);
            Assert.HasCount(0, result);
        }

        [TestMethod]
        public void GetById_ReturnsCorrectSeizoen()
        {
            var service = new SeizoenService(new FakeSeizoenRepository());
            var result = service.GetById(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.SerieId);
            Assert.AreEqual(1, result.Seizoennummer);
            Assert.AreEqual(10, result.AantalAfleveringen);
            Assert.AreEqual(2020, result.Jaartal);
        }

        [TestMethod]
        public void GetById_ReturnsNull_WhenSeizoenNotFound()
        {
            var service = new SeizoenService(new FakeSeizoenRepository());
            var result = service.GetById(99);
            Assert.IsNull(result);
        }

        // Uitzonderingen
        [TestMethod]
        public void GetBySerieId_ReturnsEmptyList_WhenGeenSeizoenen()
        {
            var repo = new FakeSeizoenRepository();
            repo.SimuleerLegeDatabase = true;
            var service = new SeizoenService(repo);
            var result = service.GetBySerieId(1);
            Assert.HasCount(0, result);
        }

        [TestMethod]
        public void GetById_ReturnsNull_WhenGeenSeizoen()
        {
            var repo = new FakeSeizoenRepository();
            repo.SimuleerLegeDatabase = true;
            var service = new SeizoenService(repo);
            var result = service.GetById(1);
            Assert.IsNull(result);
        }
    }
}