using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Services;
using unit_test.FakeRepositories;

namespace unit_test.ServiceExeptionTests
{
    [TestClass]
    public class GenreServiceExceptionTest
    {
        [TestMethod]
        public void GetAll_ThrowsException_WhenDatabaseFout()
        {
            var repo = new FakeGenreRepositoryException();
            var service = new GenreService(repo);
            try { service.GetAll(); Assert.Fail("Geen exception gegooid"); }
            catch (Exception) { }
        }

        [TestMethod]
        public void GetById_ThrowsException_WhenDatabaseFout()
        {
            var repo = new FakeGenreRepositoryException();
            var service = new GenreService(repo);
            try { service.GetById(1); Assert.Fail("Geen exception gegooid"); }
            catch (Exception) { }
        }

        [TestMethod]
        public void GetByFilmId_ThrowsException_WhenDatabaseFout()
        {
            var repo = new FakeGenreRepositoryException();
            var service = new GenreService(repo);
            try { service.GetByFilmId(1); Assert.Fail("Geen exception gegooid"); }
            catch (Exception) { }
        }

        [TestMethod]
        public void GetBySerieId_ThrowsException_WhenDatabaseFout()
        {
            var repo = new FakeGenreRepositoryException();
            var service = new GenreService(repo);
            try { service.GetBySerieId(1); Assert.Fail("Geen exception gegooid"); }
            catch (Exception) { }
        }
    }
}