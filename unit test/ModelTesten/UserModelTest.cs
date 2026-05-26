using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Models;

namespace unit_test.ModelTesten
{
    [TestClass]
    public class UserModelTest
    {
        [TestMethod]
        public void UserModel_Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var id = 1;
            var naam = "TestGebruiker";
            var wachtwoord = "Wachtwoord123";

            // Act
            var user = new UserModel(id, naam, wachtwoord);

            // Assert
            Assert.AreEqual(id, user.Id);
            Assert.AreEqual(naam, user.Gebruikersnaam);
            Assert.AreEqual(wachtwoord, user.Wachtwoord);
        }
    }
}