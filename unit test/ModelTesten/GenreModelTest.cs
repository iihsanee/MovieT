using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Models;

namespace unit_test.ModelTesten
{
    [TestClass]
    public class GenreModelTest
    {
        [TestMethod]
        public void GenreModel_Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var id = 1;
            var naam = "Thriller";

            // Act
            var genre = new GenreModel(id, naam);

            // Assert
            Assert.AreEqual(id, genre.Id);
            Assert.AreEqual(naam, genre.Naam);
        }
    }
}