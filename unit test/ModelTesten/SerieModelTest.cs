using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Models;
using System;

namespace unit_test.ModelTesten
{
    [TestClass]
    public class SerieModelTest
    {
        [TestMethod]
        public void SerieModel_Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var id = 1;
            var title = "Breaking Bad";
            var releaseDate = new DateTime(2008, 1, 20);
            var duration = TimeSpan.FromMinutes(47);
            var description = "A chemistry teacher turns criminal";

            // Act
            var serie = new SerieModel(id, title, releaseDate, duration, description);

            // Assert
            Assert.AreEqual(id, serie.Id);
            Assert.AreEqual(title, serie.Title);
            Assert.AreEqual(releaseDate, serie.ReleaseDate);
            Assert.AreEqual(duration, serie.Duration);
            Assert.AreEqual(description, serie.Description);

        }
    }
}