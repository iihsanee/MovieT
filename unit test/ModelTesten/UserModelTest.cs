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

            // Act
            var user = new UserModel(id, naam);

            // Assert
            Assert.AreEqual(id, user.Id);
            Assert.AreEqual(naam, user.Naam);
        }
    }
}