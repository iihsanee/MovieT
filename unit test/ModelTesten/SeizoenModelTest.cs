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
            var model = new SeizoenModel(1, 1, 1, 10, 2020);

            Assert.AreEqual(1, model.Id);
            Assert.AreEqual(1, model.SerieId);
            Assert.AreEqual(1, model.Seizoennummer);
            Assert.AreEqual(10, model.AantalAfleveringen);
            Assert.AreEqual(2020, model.Jaartal);
        }
    }
}