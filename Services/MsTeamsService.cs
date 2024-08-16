namespace JokerService.Services
{
    /// <summary>
    /// Service class for sending messages to Microsoft Teams.
    /// </summary>
    public class MsTeamsService(ILogger<MsTeamsService> logger)
    {
        /// <summary>
        /// Sends a message to a Microsoft Teams channel.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SendTeamsPostAsync(string message)
        {
            // TODO: Implement the logic to send a message to a Microsoft Teams channel.

            try
            {
                using (logger.BeginScope(new Dictionary<string, object> { ["filterOnPropertyValue"] = "TheJoker" }))
                {
                    logger.LogInformation("Hello");
                }

                await Task.Delay(10000); // 10 Second Delay before sending the message.
                logger.LogInformation("MS Teams Post sent successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send MS Teams Post.\r\n{Error}", ex);
            }
        }
    }
}
