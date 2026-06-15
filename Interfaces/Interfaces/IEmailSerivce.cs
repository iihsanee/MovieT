namespace Interfaces.Interfaces
{
    public interface IEmailService
    {
        void StuurResetEmail(string naarEmail, string resetToken, string resetUrl);
    }
}