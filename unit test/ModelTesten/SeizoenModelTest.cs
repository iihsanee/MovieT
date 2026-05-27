using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Models;

namespace unit_test.ModelTesten
{
    [TestClass]
    public class SeizoenModelTest
    {
        [TestMethod]
        public void SeizoenModel_Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var id = 1;
            var serieId = 1;
            var seizoennummer = 1;
            var aantalAfleveringen = 10;
            var jaartal = 2020;

            // Act
            var model = new SeizoenModel(id, serieId, seizoennummer, aantalAfleveringen, jaartal);

            // Assert
            Assert.AreEqual(id, model.Id);
            Assert.AreEqual(serieId, model.SerieId);
            Assert.AreEqual(seizoennummer, model.Seizoennummer);
            Assert.AreEqual(aantalAfleveringen, model.AantalAfleveringen);
            Assert.AreEqual(jaartal, model.Jaartal);
        }
    }
}