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
            // Arrange
            var repo = new FakeUserRepository();
            var service = new UserService(repo);

            // Act
            var result = service.GetById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("TestGebruiker", result.Naam);
        }

        [TestMethod]
        public void GetById_ReturnsNull_WhenUserNotFound()
        {
            // Arrange
            var repo = new FakeUserRepository();
            var service = new UserService(repo);

            // Act
            var result = service.GetById(99);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void RegistreerGebruiker_ReturnsNull_WhenSuccessful()
        {
            // Arrange
            var repo = new FakeUserRepository();
            var service = new UserService(repo);

            // Act
            var result = service.RegistreerGebruiker("NieuweGebruiker", "Wachtwoord123", "Wachtwoord123");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void RegistreerGebruiker_ReturnsError_WhenUsernameExists()
        {
            // Arrange
            var repo = new FakeUserRepository();
            var service = new UserService(repo);

            // Act
            var result = service.RegistreerGebruiker("TestGebruiker", "Wachtwoord123", "Wachtwoord123");

            // Assert
            Assert.AreEqual("Deze gebruikersnaam is al in gebruik.", result);
        }

        [TestMethod]
        public void RegistreerGebruiker_ReturnsError_WhenPasswordTooShort()
        {
            // Arrange
            var repo = new FakeUserRepository();
            var service = new UserService(repo);

            // Act
            var result = service.RegistreerGebruiker("NieuweGebruiker", "kort", "kort");

            // Assert
            Assert.AreEqual("Het wachtwoord moet minimaal 8 tekens bevatten.", result);
        }

        [TestMethod]
        public void RegistreerGebruiker_ReturnsError_WhenPasswordsDoNotMatch()
        {
            // Arrange
            var repo = new FakeUserRepository();
            var service = new UserService(repo);

            // Act
            var result = service.RegistreerGebruiker("NieuweGebruiker", "Wachtwoord123", "AndersWachtwoord");

            // Assert
            Assert.AreEqual("De wachtwoorden komen niet overeen.", result);
        }
    }
}