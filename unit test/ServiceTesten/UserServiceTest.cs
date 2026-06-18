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
            var service = new UserService(new FakeUserRepository());
            var result = service.GetById(1);
            Assert.IsNotNull(result);
            Assert.AreEqual("test@student.fontys.nl", result.Email);
        }

        [TestMethod]
        public void GetById_ReturnsNull_WhenUserNotFound()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.GetById(99);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetByEmail_ReturnsCorrectUser()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.GetByEmail("test@student.fontys.nl");
            Assert.IsNotNull(result);
            Assert.AreEqual("test@student.fontys.nl", result.Email);
        }

        [TestMethod]
        public void GetByEmail_ReturnsNull_WhenUserNotFound()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.GetByEmail("bestaatnie@test.nl");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void EmailExists_ReturnsTrue_WhenUserExists()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.EmailExists("test@student.fontys.nl");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EmailExists_ReturnsFalse_WhenUserNotFound()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.EmailExists("bestaatnie@test.nl");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RegistreerGebruiker_ReturnsNull_WhenSuccessful()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.RegistreerGebruiker("nieuw@test.nl", "Wachtwoord123", "Wachtwoord123");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Login_ReturnsTrue_WhenCorrectCredentials()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.Login("test@student.fontys.nl", "wachtwoord123");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RegistreerGebruiker_ReturnsError_WhenEmailExists()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.RegistreerGebruiker("test@student.fontys.nl", "Wachtwoord123", "Wachtwoord123");
            Assert.AreEqual("Dit e-mailadres is al in gebruik.", result);
        }

        [TestMethod]
        public void RegistreerGebruiker_ReturnsError_WhenPasswordTooShort()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.RegistreerGebruiker("nieuw@test.nl", "kort", "kort");
            Assert.AreEqual("Het wachtwoord moet minimaal 8 tekens bevatten.", result);
        }

        [TestMethod]
        public void RegistreerGebruiker_ReturnsError_WhenPasswordsDoNotMatch()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.RegistreerGebruiker("nieuw@test.nl", "Wachtwoord123", "AndersWachtwoord");
            Assert.AreEqual("De wachtwoorden komen niet overeen.", result);
        }

        [TestMethod]
        public void Login_ReturnsFalse_WhenUserNotFound()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.Login("bestaatnie@test.nl", "Wachtwoord123");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Login_ReturnsFalse_WhenWachtwoordOnjuist()
        {
            var service = new UserService(new FakeUserRepository());
            var result = service.Login("test@student.fontys.nl", "VerkeerWachtwoord");
            Assert.IsFalse(result);
        }
    }
}