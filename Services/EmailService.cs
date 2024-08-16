using System.Net;
using System.Net.Mail;
using JokerService.Settings;
using Microsoft.Extensions.Options;

namespace JokerService.Services
{
    /// <summary>
    /// Represents a service for sending emails.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="EmailService"/> class.
    /// </remarks>
    /// <param name="smtpSettings">The SMTP settings.</param>
    /// <param name="logger">The logger.</param>
    public class EmailService(IOptions<SmtpSettings> smtpSettings, ILogger<EmailService> logger)
    {
        private readonly SmtpSettings _smtpSettings = smtpSettings.Value;

        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="to">The recipient email address.</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="body">The email body.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
            {
                Credentials = new NetworkCredential(_smtpSettings.UserName, _smtpSettings.Password),
                EnableSsl = _smtpSettings.EnableSsl
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpSettings.UserName),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            mailMessage.To.Add(to);

            try
            {
                await client.SendMailAsync(mailMessage);
                logger.LogInformation("Email sent successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send email.\r\n{Error}", ex);
                throw;
            }
        }
    }
}
