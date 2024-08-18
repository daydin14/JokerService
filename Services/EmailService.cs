using System.Net;
using System.Net.Mail;
using JokerService.Settings;

namespace JokerService.Services
{
    /// <summary>
    /// Represents a service for sending emails.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="EmailService"/> class.
    /// </remarks>
    /// <param name="logger"></param>
    /// <param name="emailSettings"></param>
    public class EmailService(ILogger<EmailService> logger, EmailSettings emailSettings)
    {

        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="toAddress">The recipient email address.</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="body">The email body.</param>
        /// <returns>An asynchronus task operation for sending emails.</returns>
        public async Task SendEmailAsync(string toAddress, string subject, string body)
        {
            using var client = new SmtpClient(emailSettings.Host, emailSettings.Port)
            {
                Credentials = new NetworkCredential(emailSettings.FromAddress, emailSettings.Password),
                EnableSsl = emailSettings.EnableSsl
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailSettings.FromAddress),
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
