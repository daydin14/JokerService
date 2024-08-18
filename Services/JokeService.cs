using JokerService.Processes;

namespace JokerService.Services;

/// <summary>
/// Represents a service for retrieving jokes.
/// </summary>
public class JokeService
{
    private readonly ILogger<JokeService> _logger;
    private JokeBuilderProcess _jokeBuilderProcess;

    public JokeService(ILogger<JokeService> logger, JokeBuilderProcess jokeBuilderProcess) => (_logger, _jokeBuilderProcess) = (logger, jokeBuilderProcess);

    public async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Execute the joke builder process...
                await _jokeBuilderProcess.ExecuteAsync(stoppingToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex);
        }
    }
}
