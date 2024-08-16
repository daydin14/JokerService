using Serilog;

namespace JokerService.Settings
{
    /// <summary>
    /// Represents the settings for configuring Serilog.
    /// </summary>
    public class SerilogSettings
    {
        /// <summary>
        /// Configures Serilog using the settings from the appsettings.json file.
        /// </summary>
        public void ConfigureSerilog()
        {
            try
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while configuring Serilog in appSettings.json : {ex}");
                throw;
            }
        }
    }
}
