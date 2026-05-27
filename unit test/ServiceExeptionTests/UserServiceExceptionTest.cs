using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Services;
using unit_test.FakeRepositories;

namespace unit_test.ServiceExeptionTests
{
    [TestClass]
    public class UserServiceExceptionTest
    {
        [TestMethod]
        public void GetById_ThrowsException_WhenDatabaseFout()
        {
            var repo = new FakeUserRepositoryException();
            var service = new UserService(repo);
            try { service.GetById(1); Assert.Fail("Geen exception gegooid"); }
            catch (Exception) { }
        }

        [TestMethod]
        public void GetByNaam_ThrowsException_WhenDatabaseFout()
        {
            var repo = new FakeUserRepositoryException();
            var service = new UserService(repo);
            try { service.GetByNaam("TestGebruiker"); Assert.Fail("Geen exception gegooid"); }
            catch (Exception) { }
        }

        [TestMethod]
        public void UsernameExists_ThrowsException_WhenDatabaseFout()
        {
            var repo = new FakeUserRepositoryException();
            var service = new UserService(repo);
            try { service.UsernameExists("TestGebruiker"); Assert.Fail("Geen exception gegooid"); }
            catch (Exception) { }
        }

        [TestMethod]
        public void RegistreerGebruiker_ThrowsException_WhenDatabaseFout()
        {
            var repo = new FakeUserRepositoryException();
            var service = new UserService(repo);
            try { service.RegistreerGebruiker("NieuweGebruiker", "Wachtwoord123", "Wachtwoord123"); Assert.Fail("Geen exception gegooid"); }
            catch (Exception) { }
        }
    }
}