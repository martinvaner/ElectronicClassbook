using Base.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;

namespace Base
{
	public class EmailService : IEmailService
	{
        private readonly SmtpSettings smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
		{
            this.smtpSettings = smtpSettings.Value;
		}
        public void Send(string to, string subject, string message)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(smtpSettings.SenderEmail));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            var bodyBuilder = new BodyBuilder();

            email.Body = new TextPart(TextFormat.Html) { Text = message };

            // send email
            using (var smtp = new SmtpClient())
            {
                smtp.CheckCertificateRevocation = false;

                smtp.SslProtocols = SslProtocols.Ssl3 | SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13;
                smtp.Connect(smtpSettings.Server, smtpSettings.Port, SecureSocketOptions.Auto);
                smtp.AuthenticationMechanisms.Remove("XOAUTH2");
                smtp.Authenticate(smtpSettings.Username, smtpSettings.Password);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
	}
}
