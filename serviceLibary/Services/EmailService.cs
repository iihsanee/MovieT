using Interfaces.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
namespace serviceLibary.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public void StuurResetEmail(string naarEmail, string resetToken, string resetUrl)
        {
            try
            {
                string resetLink = $"https://i576606.luna.fhict.nl/User/WachtwoordResetten?token={resetToken}";
                string host = _config["MailServer:Host"] ?? throw new Exception("MailServer:Host niet gevonden in appsettings");
                int port = int.Parse(_config["MailServer:Port"] ?? "587");
                string vanAdres = _config["MailboxAddress:Address"] ?? throw new Exception("MailboxAddress:Address niet gevonden in appsettings");
                string vanNaam = _config["MailboxAddress:Name"] ?? throw new Exception("MailboxAddress:Name niet gevonden in appsettings");
                string wachtwoord = _config["MailServer:Password"] ?? throw new Exception("MailServer:Password niet gevonden in appsettings");
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(vanNaam, vanAdres));
                email.To.Add(new MailboxAddress("", naarEmail));
                email.Subject = "Wachtwoord resetten";
                email.Body = new TextPart("plain")
                {
                    Text = $"Klik op de volgende link om je wachtwoord te resetten: {resetLink}"
                };
                using var client = new SmtpClient();
                client.Connect(host, port, SecureSocketOptions.StartTls);
                client.Authenticate(vanAdres, wachtwoord);
                client.Send(email);
                client.Disconnect(true);
            }
            catch (Exception ex)
            {
                throw new Exception("Fout bij het versturen van de reset email.", ex);
            }
        }
    }
}