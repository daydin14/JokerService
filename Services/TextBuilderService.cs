using JokerService.Settings;
using System.Text;

namespace JokerService.Services
{
    /// <summary>
    /// Service for building and writing text.
    /// </summary>
    public class TextBuilderService(ILogger<TextBuilderService> logger, FileSystemSettings fileSystemSettings)
    {
        private readonly ILogger<TextBuilderService> _logger = logger;
        private readonly FileSystemSettings _fileSystemSettings = fileSystemSettings;

        /// <summary>
        /// Builds and writes the specified text.
        /// </summary>
        /// <param name="text">The text to be written.</param>
        public async Task BuildTextAsync(string text)
        {
            try
            {
                _logger.LogInformation("Building text...");
                _logger.LogInformation("Joke and Punch Line to text: \r\n{Text}", text);
                _logger.LogInformation($"Project Root Directory: {_fileSystemSettings.RootDirectory}");

                _logger.LogInformation("Writing text to the jokes directory...");
                // Create the jokes directory if it does not exist.
                if (_fileSystemSettings.CreateDirectoryIfNotExists(_fileSystemSettings.JokesDirectory))
                {
                    _logger.LogInformation($"Created new directory: {_fileSystemSettings.JokesDirectory}");
                }
                // Write the text to a file in the jokes directory
                string jokesFilePath = Path.Combine(_fileSystemSettings.JokesDirectory, "joke.txt");
                await File.WriteAllTextAsync(jokesFilePath, text, Encoding.UTF8);

                if (_fileSystemSettings.ProcessRemoteJokes)
                {
                    _logger.LogInformation("Writing text to the remote jokes directory...");
                    // Create the remote jokes directory if it does not exist.
                    if (_fileSystemSettings.CreateDirectoryIfNotExists(_fileSystemSettings.RemoteJokesDirectory!))
                    {
                        _logger.LogInformation($"Created new directory: {_fileSystemSettings.RemoteJokesDirectory}");
                    } 
                    // Write the text to a file in the remote jokes directory
                    string remoteJokesFilePath = Path.Combine(_fileSystemSettings.RemoteJokesDirectory!, "joke.txt");
                    await File.WriteAllTextAsync(remoteJokesFilePath, text, Encoding.UTF8);
                }

                _logger.LogInformation("Text written successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while writing to the file.");
            }
        }
    }
}
