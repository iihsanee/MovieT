using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace servicelibrary.Services
{
    public class EmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _vanEmail;

        public EmailService(IConfiguration configuration)
        {
            _smtpHost = configuration["Email:SmtpHost"]
                ?? throw new Exception("Email:SmtpHost niet gevonden in appsettings");
            _smtpPort = int.Parse(configuration["Email:SmtpPort"]
                ?? throw new Exception("Email:SmtpPort niet gevonden in appsettings"));
            _vanEmail = configuration["Email:VanEmail"]
                ?? throw new Exception("Email:VanEmail niet gevonden in appsettings");
        }

        public void StuurResetEmail(string naarEmail, string resetToken, string resetUrl)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(_vanEmail);
                mail.To.Add(naarEmail);
                mail.Subject = "Wachtwoord resetten";
                mail.Body = $"Klik op de volgende link om je wachtwoord te resetten: {resetUrl}";
                mail.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient(_smtpHost, _smtpPort);
                smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception("Fout bij het versturen van de reset email.", ex);
            }
        }
    }
}