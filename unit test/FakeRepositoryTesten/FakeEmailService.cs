using Interfaces.Interfaces;
namespace unit_test.FakeRepositories
{
    public class FakeEmailService : IEmailService
    {
        public bool EmailVerstuurd = false;
        public bool GooidFout = false;

        public void StuurResetEmail(string naarEmail, string resetToken, string resetUrl)
        {
            if (GooidFout)
                throw new Exception("Mail failed: Could not connect to SMTP server.");
            EmailVerstuurd = true;
        }
    }
}