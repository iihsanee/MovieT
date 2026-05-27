using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Models;

namespace unit_test.ModelTesten
{
    [TestClass]
    public class AfleveringModelTest
    {
        [TestMethod]
        public void AfleveringModel_Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var id = 1;
            var seizoenId = 1;
            var titel = "Pilot";
            var afleveringsnummer = 1;
            var duurtijd = 45;

            // Act
            var model = new AfleveringModel(id, seizoenId, titel, afleveringsnummer, duurtijd);

            // Assert
            Assert.AreEqual(id, model.Id);
            Assert.AreEqual(seizoenId, model.SeizoenId);
            Assert.AreEqual(titel, model.Titel);
            Assert.AreEqual(afleveringsnummer, model.Afleveringsnummer);
            Assert.AreEqual(duurtijd, model.Duurtijd);
        }
    }
}