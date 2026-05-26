using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Services;
using unit_test.FakeRepositories;

namespace unit_test.ServiceTesten
{
    [TestClass]
    public class SeizoenServiceTest
    {
        [TestMethod]
        public void GetBySerieId_ReturnsCorrectSeizoenen()
        {
            var repo = new FakeSeizoenRepository();
            var service = new SeizoenService(repo);

            var result = service.GetBySerieId(1);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].Seizoennummer);
        }

        [TestMethod]
        public void GetBySerieId_ReturnsEmpty_WhenSerieNotFound()
        {
            var repo = new FakeSeizoenRepository();
            var service = new SeizoenService(repo);

            var result = service.GetBySerieId(99);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetById_ReturnsCorrectSeizoen()
        {
            var repo = new FakeSeizoenRepository();
            var service = new SeizoenService(repo);

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
            var repo = new FakeSeizoenRepository();
            var service = new SeizoenService(repo);

            var result = service.GetById(99);

            Assert.IsNull(result);
        }
    }
}