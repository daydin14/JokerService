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
    /// <param name="emailSettings">The Email settings.</param>
    /// <param name="logger">The logger.</param>
    public class EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
    {
        private readonly EmailSettings _emailSettings = emailSettings.Value;

        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="toAddress">The recipient email address.</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="body">The email body.</param>
        /// <returns>An asynchronus task operation for sending emails.</returns>
        public async Task SendEmailAsync(string toAddress, string subject, string body)
        {
            using var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(_emailSettings.FromAddress, _emailSettings.Password),
                EnableSsl = _emailSettings.EnableSsl
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.FromAddress),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            mailMessage.To.Add(toAddress);

            try
            {
                await client.SendMailAsync(mailMessage);
                logger.LogInformation("Email sent successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send email.\r\n{Error}", ex);
            }
        }
    }
}
