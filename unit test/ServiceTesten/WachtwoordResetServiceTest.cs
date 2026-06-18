using Microsoft.VisualStudio.TestTools.UnitTesting;
using serviceLibary.Services;
using unit_test.FakeRepositories;
namespace unit_test.ServiceTesten
{
    [TestClass]
    public class WachtwoordResetServiceTest
    {
        private WachtwoordResetService MaakService(FakeWachtwoordResetRepository? resetRepo = null, FakeUserRepository? userRepo = null)
        {
            return new WachtwoordResetService(
                resetRepo ?? new FakeWachtwoordResetRepository(),
                userRepo ?? new FakeUserRepository()
            );
        }
        // Happy flow
        [TestMethod]
        public void VraagResetAan_BestaandEmail_GeeftGeenFoutmelding()
        {
            var service = MaakService();
            var result = service.VraagResetAan("test@student.fontys.nl");
            Assert.IsNull(result);
        }
        [TestMethod]
        public void VraagResetAan_BestaandEmail_SlaatTokenOp()
        {
            var resetRepo = new FakeWachtwoordResetRepository();
            var service = MaakService(resetRepo: resetRepo);
            service.VraagResetAan("test@student.fontys.nl");
            var token = resetRepo.GetLaatsteToken();
            Assert.IsFalse(string.IsNullOrEmpty(token));
        }
        [TestMethod]
        public void GetByToken_GeldigToken_GeeftModel()
        {
            var resetRepo = new FakeWachtwoordResetRepository();
            var service = MaakService(resetRepo: resetRepo);
            service.VraagResetAan("test@student.fontys.nl");
            var token = resetRepo.GetLaatsteToken();
            var result = service.GetByToken(token);
            Assert.IsNotNull(result);
            Assert.AreEqual(token, result.ResetToken);
        }
        [TestMethod]
        public void ValideerToken_GeldigToken_GeeftGeenFoutmelding()
        {
            var resetRepo = new FakeWachtwoordResetRepository();
            var service = MaakService(resetRepo: resetRepo);
            service.VraagResetAan("test@student.fontys.nl");
            var token = resetRepo.GetLaatsteToken();
            var result = service.ValideerToken(token);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void ResetWachtwoord_GeldigToken_MarkeertTokenAlsGebruikt()
        {
            var resetRepo = new FakeWachtwoordResetRepository();
            var service = MaakService(resetRepo: resetRepo);
            service.VraagResetAan("test@student.fontys.nl");
            var token = resetRepo.GetLaatsteToken();
            service.ResetWachtwoord(token, "NieuwWachtwoord123");
            var dto = resetRepo.GetByToken(token);
            Assert.IsTrue(dto.Gebruikt);
        }
        // Uitzonderingen
        [TestMethod]
        public void VraagResetAan_OnbekendEmail_GeeftFoutmelding()
        {
            var service = MaakService();
            var result = service.VraagResetAan("onbekend@student.fontys.nl");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetByEmail_OnbekendEmail_GeeftNull()
        {
            var service = MaakService();
            var result = service.GetByEmail("onbekend@student.fontys.nl");
            Assert.IsNull(result);
        }
        [TestMethod]
        public void GetByToken_OngeldigToken_GeeftNull()
        {
            var service = MaakService();
            var result = service.GetByToken("niet-bestaand-token");
            Assert.IsNull(result);
        }
        [TestMethod]
        public void ValideerToken_OngeldigToken_GeeftFoutmelding()
        {
            var service = MaakService();
            var result = service.ValideerToken("niet-bestaand-token");
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void ValideerToken_TokenAlGebruikt_GeeftFoutmelding()
        {
            var resetRepo = new FakeWachtwoordResetRepository();
            var service = MaakService(resetRepo: resetRepo);
            service.VraagResetAan("test@student.fontys.nl");
            var token = resetRepo.GetLaatsteToken();
            service.ResetWachtwoord(token, "NieuwWachtwoord123");
            var result = service.ValideerToken(token);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetByGebruikerId_GeenTokens_GeeftLegeLijst()
        {
            var resetRepo = new FakeWachtwoordResetRepository();
            resetRepo.SimuleerLegeDatabase = true;
            var service = MaakService(resetRepo: resetRepo);
            var result = service.GetByEmail("test@student.fontys.nl");
            Assert.IsNull(result);
        }
    }
}