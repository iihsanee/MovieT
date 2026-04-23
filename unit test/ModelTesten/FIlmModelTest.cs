using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Models;
using System;

namespace unit_test.ModelTesten
{
    [TestClass]
    public class FilmModelTest
    {
        [TestMethod]
        public void FilmModel_Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var id = 1;
            var title = "Inception";
            var releaseDate = new DateTime(2010, 7, 16);
            var duration = TimeSpan.FromHours(2);
            var description = "A mind-bending thriller";

            // Act
            var film = new FilmModelModel(id, title, releaseDate, duration, description);

            // Assert
            Assert.AreEqual(id, film.Id);
            Assert.AreEqual(title, film.Title);
            Assert.AreEqual(releaseDate, film.ReleaseDate);
            Assert.AreEqual(duration, film.Duration);
            Assert.AreEqual(description, film.Description);
            Assert.IsNotNull(film.WatchingLists);
            Assert.IsNotNull(film.WatchedLists);
            Assert.IsEmpty(film.WatchingLists);
            Assert.IsEmpty(film.WatchedLists);
        }
    }
}