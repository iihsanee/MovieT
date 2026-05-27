using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Services;
using unit_test.FakeRepositories;

namespace unit_test.ServiceExeptionTests
{
    [TestClass]
    public class SeizoenServiceExceptionTest
    {
        [TestMethod]
        public void GetBySerieId_ThrowsException_WhenDatabaseFout()
        {
            var repo = new FakeSeizoenRepositoryException();
            var service = new SeizoenService(repo);
            try { service.GetBySerieId(1); Assert.Fail("Geen exception gegooid"); }
            catch (Exception) { }
        }

        [TestMethod]
        public void GetById_ThrowsException_WhenDatabaseFout()
        {
            var repo = new FakeSeizoenRepositoryException();
            var service = new SeizoenService(repo);
            try { service.GetById(1); Assert.Fail("Geen exception gegooid"); }
            catch (Exception) { }
        }
    }
}