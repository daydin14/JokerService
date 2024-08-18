using JokerService.Services;

namespace JokerService;

/// <summary>
/// Represents a worker that performs background tasks.
/// </summary>
public class Worker : BackgroundService
{
    private readonly JokeService _jokeService;
    private readonly ILogger<Worker> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="Worker"/> class.
    /// </summary>
    /// <param name="jokeService">The joke service.</param>
    /// <param name="logger">The logger.</param>
    public Worker(JokeService jokeService, ILogger<Worker> logger) => (_jokeService, _logger) = (jokeService, logger);

    /// <summary>
    /// Executes the background task asynchronously.
    /// </summary>
    /// <param name="stoppingToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Execute the joke builder process...
                await _jokeService.ExecuteAsync(stoppingToken);
            }
        }
        catch (OperationCanceledException ex)
        {
            // When the stopping token is canceled, for example, a call made from services.msc,
            // we shouldn't exit with a non-zero exit code. In other words, this is expected...
            _logger.LogWarning(ex, "{Message}", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex);
            /*
                Terminates this process and returns an exit code to the operating system.
                This is required to avoid the 'BackgroundServiceExceptionBehavior', which performs one of two scenarios:

                1. When set to "Ignore": will do nothing at all, errors cause zombie services.
                2. When set to "StopHost": will cleanly stop the host, and log errors.

                In order for the Windows Service Management system to leverage configured
                recovery options, we need to terminate the process with a non-zero exit code.
            */
            Environment.Exit(1);
        }
    }

    /// <summary>
    /// Starts the background service asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("The Joker is here!...");
        await base.StartAsync(cancellationToken);
    }

    /// <summary>
    /// Stops the background service asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("The Joker has escaped!");
        await base.StopAsync(cancellationToken);
    }
}
