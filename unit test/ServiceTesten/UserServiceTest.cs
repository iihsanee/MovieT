using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Services;
using unit_test.FakeRepositories;

namespace unit_test.ServiceTesten
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void GetById_ReturnsCorrectUser()
        {
            var repo = new FakeUserRepository();
            var service = new UserService(repo);
            var result = service.GetById(1);
            Assert.IsNotNull(result);
            Assert.AreEqual("TestGebruiker", result.Naam);
        }

        [TestMethod]
        public void GetById_ReturnsNull_WhenUserNotFound()
        {
            var repo = new FakeUserRepository();
            var service = new UserService(repo);
            var result = service.GetById(99);
            Assert.IsNull(result);
        }
    }
}