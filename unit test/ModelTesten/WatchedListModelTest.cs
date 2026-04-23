using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Models;

namespace unit_test.ModelTesten
{
    [TestClass]
    public class WatchedListModelTest
    {
        [TestMethod]
        public void WatchedListModel_Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var userId = 1;
            var filmId = 2;
            var title = "Inception";
            var type = "Film";

            // Act
            var item = new WatchedListModel(userId, filmId, null, title, type);

            // Assert
            Assert.AreEqual(userId, item.UserId);
            Assert.AreEqual(filmId, item.FilmId);
            Assert.IsNull(item.SerieId);
            Assert.AreEqual(title, item.Title);
            Assert.AreEqual(type, item.Type);
        }

        [TestMethod]
        public void WatchedListModel_WithSerie_SetsPropertiesCorrectly()
        {
            // Arrange
            var userId = 1;
            var serieId = 3;
            var title = "Breaking Bad";
            var type = "Serie";

            // Act
            var item = new WatchedListModel(userId, null, serieId, title, type);

            // Assert
            Assert.AreEqual(userId, item.UserId);
            Assert.IsNull(item.FilmId);
            Assert.AreEqual(serieId, item.SerieId);
            Assert.AreEqual(title, item.Title);
            Assert.AreEqual(type, item.Type);
        }
    }
}