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
            var wachtwoord = "Wachtwoord123";
            var email = "test@student.fontys.nl";
            // Act
            var user = new UserModel(id, wachtwoord, email);
            // Assert
            Assert.AreEqual(id, user.Id);
            Assert.AreEqual(wachtwoord, user.Wachtwoord);
            Assert.AreEqual(email, user.Email);
        }
    }
}