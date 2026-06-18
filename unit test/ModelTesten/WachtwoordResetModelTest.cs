using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Models;
namespace unit_test.ModelTesten
{
    [TestClass]
    public class WachtwoordResetModelTest
    {
        [TestMethod]
        public void WachtwoordResetModel_Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var id = 1;
            var gebruikerId = 42;
            var resetToken = "abc123-reset-token";
            var aangemaaktOp = new DateTime(2026, 6, 14, 10, 0, 0);
            var gebruikt = false;
            // Act
            var model = new WachtwoordResetModel(id, gebruikerId, resetToken, aangemaaktOp, gebruikt);
            // Assert
            Assert.AreEqual(id, model.Id);
            Assert.AreEqual(gebruikerId, model.GebruikerId);
            Assert.AreEqual(resetToken, model.ResetToken);
            Assert.AreEqual(aangemaaktOp, model.AangemaaktOp);
            Assert.AreEqual(gebruikt, model.Gebruikt);
        }
        [TestMethod]
        public void WachtwoordResetModel_GebruiktTrue_SetsPropertyCorrectly()
        {
            // Arrange
            var id = 2;
            var gebruikerId = 7;
            var resetToken = "al-gebruikt-token";
            var aangemaaktOp = DateTime.Now;
            var gebruikt = true;
            // Act
            var model = new WachtwoordResetModel(id, gebruikerId, resetToken, aangemaaktOp, gebruikt);
            // Assert
            Assert.IsTrue(model.Gebruikt);
        }
    }
}