using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Services;
using unit_test.FakeRepositories;

namespace unit_test.ServiceTesten
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void GetById_ReturnsCorrectUser()
        {
            var repo = new FakeUserRepository();
            var service = new UserService(repo);
            var result = service.GetById(1);
            Assert.IsNotNull(result);
            Assert.AreEqual("TestGebruiker", result.Gebruikersnaam);
        }

        [TestMethod]
        public void GetById_ReturnsNull_WhenUserNotFound()
        {
            var repo = new FakeUserRepository();
            var service = new UserService(repo);
            var result = service.GetById(99);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetByNaam_ReturnsCorrectUser()
        {
            var repo = new FakeUserRepository();
            var service = new UserService(repo);
            var result = service.GetByNaam("TestGebruiker");
            Assert.IsNotNull(result);
            Assert.AreEqual("TestGebruiker", result.Gebruikersnaam);
        }

        [TestMethod]
        public void GetByNaam_ReturnsNull_WhenUserNotFound()
        {
            var repo = new FakeUserRepository();
            var service = new UserService(repo);
            var result = service.GetByNaam("BestaatNiet");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void UsernameExists_ReturnsTrue_WhenUserExists()
        {
            var repo = new FakeUserRepository();
            var service = new UserService(repo);
            var result = service.UsernameExists("TestGebruiker");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UsernameExists_ReturnsFalse_WhenUserNotFound()
        {
            var repo = new FakeUserRepository();
            var service = new UserService(repo);
            var result = service.UsernameExists("BestaatNiet");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RegistreerGebruiker_ReturnsNull_WhenSuccessful()
        {
            var repo = new FakeUserRepository();
            var service = new UserService(repo);
            var result = service.RegistreerGebruiker("NieuweGebruiker", "Wachtwoord123", "Wachtwoord123");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void RegistreerGebruiker_ReturnsError_WhenUsernameExists()
        {
            var repo = new FakeUserRepository();
            var service = new UserService(repo);
            var result = service.RegistreerGebruiker("TestGebruiker", "Wachtwoord123", "Wachtwoord123");
            Assert.AreEqual("Deze gebruikersnaam is al in gebruik.", result);
        }

        [TestMethod]
        public void RegistreerGebruiker_ReturnsError_WhenPasswordTooShort()
        {
            var repo = new FakeUserRepository();
            var service = new UserService(repo);
            var result = service.RegistreerGebruiker("NieuweGebruiker", "kort", "kort");
            Assert.AreEqual("Het wachtwoord moet minimaal 8 tekens bevatten.", result);
        }

        [TestMethod]
        public void RegistreerGebruiker_ReturnsError_WhenPasswordsDoNotMatch()
        {
            var repo = new FakeUserRepository();
            var service = new UserService(repo);
            var result = service.RegistreerGebruiker("NieuweGebruiker", "Wachtwoord123", "AndersWachtwoord");
            Assert.AreEqual("De wachtwoorden komen niet overeen.", result);
        }
    }
}