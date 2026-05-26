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
            var model = new AfleveringModel(1, 1, "Pilot", 1, 45);

            Assert.AreEqual(1, model.Id);
            Assert.AreEqual(1, model.SeizoenId);
            Assert.AreEqual("Pilot", model.Titel);
            Assert.AreEqual(1, model.Afleveringsnummer);
            Assert.AreEqual(45, model.Duurtijd);
        }
    }
}