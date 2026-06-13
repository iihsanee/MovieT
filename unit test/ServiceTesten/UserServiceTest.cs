using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Services;
using unit_test.FakeRepositories;

namespace unit_test.ServiceTesten
{
    [TestClass]
    public class UserServiceTest
    {
        // Happy flow
        [TestMethod]
        public void GetById_ReturnsCorrectUser()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.GetById(1);
            Assert.IsNotNull(result);
            Assert.AreEqual("TestGebruiker", result.Gebruikersnaam);
        }

        [TestMethod]
        public void GetById_ReturnsNull_WhenUserNotFound()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.GetById(99);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetByNaam_ReturnsCorrectUser()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.GetByNaam("TestGebruiker");
            Assert.IsNotNull(result);
            Assert.AreEqual("TestGebruiker", result.Gebruikersnaam);
        }

        [TestMethod]
        public void GetByNaam_ReturnsNull_WhenUserNotFound()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.GetByNaam("BestaatNiet");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void UsernameExists_ReturnsTrue_WhenUserExists()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.UsernameExists("TestGebruiker");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UsernameExists_ReturnsFalse_WhenUserNotFound()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.UsernameExists("BestaatNiet");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RegistreerGebruiker_ReturnsNull_WhenSuccessful()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.RegistreerGebruiker("NieuweGebruiker", "Wachtwoord123", "Wachtwoord123");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Login_ReturnsTrue_WhenCorrectCredentials()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.Login("TestGebruiker", "wachtwoord123");
            Assert.IsTrue(result);
        }

        // Uitzonderingen
        [TestMethod]
        public void RegistreerGebruiker_ReturnsError_WhenUsernameExists()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.RegistreerGebruiker("TestGebruiker", "Wachtwoord123", "Wachtwoord123");
            Assert.AreEqual("Deze gebruikersnaam is al in gebruik.", result);
        }

        [TestMethod]
        public void RegistreerGebruiker_ReturnsError_WhenPasswordTooShort()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.RegistreerGebruiker("NieuweGebruiker", "kort", "kort");
            Assert.AreEqual("Het wachtwoord moet minimaal 8 tekens bevatten.", result);
        }

        [TestMethod]
        public void RegistreerGebruiker_ReturnsError_WhenPasswordsDoNotMatch()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.RegistreerGebruiker("NieuweGebruiker", "Wachtwoord123", "AndersWachtwoord");
            Assert.AreEqual("De wachtwoorden komen niet overeen.", result);
        }

        [TestMethod]
        public void Login_ReturnsFalse_WhenUserNotFound()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.Login("BestaatNiet", "Wachtwoord123");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Login_ReturnsFalse_WhenWachtwoordOnjuist()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.Login("TestGebruiker", "VerkeerWachtwoord");
            Assert.IsFalse(result);
        }
    }
}