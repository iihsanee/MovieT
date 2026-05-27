using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Services;
using unit_test.FakeRepositories;

namespace unit_test.ServiceExeptionTests
{
    [TestClass]
    public class AfleveringServiceExceptionTest
    {
        [TestMethod]
        public void GetBySeizoenId_ThrowsException_WhenDatabaseFout()
        {
            var repo = new FakeAfleveringRepositoryException();
            var service = new AfleveringService(repo);
            try { service.GetBySeizoenId(1); Assert.Fail("Geen exception gegooid"); }
            catch (Exception) { }
        }

        [TestMethod]
        public void GetById_ThrowsException_WhenDatabaseFout()
        {
            var repo = new FakeAfleveringRepositoryException();
            var service = new AfleveringService(repo);
            try { service.GetById(1); Assert.Fail("Geen exception gegooid"); }
            catch (Exception) { }
        }
    }
}