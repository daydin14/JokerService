namespace JokerService.Services
{
    public class MsTeamsService(ILogger<MsTeamsService> logger)
    {
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
                throw;
            }
        }
    }
}
