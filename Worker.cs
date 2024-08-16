using JokerService.Services;
using JokerService.Settings;

namespace JokerService;

/// <summary>
/// Represents a worker that performs background tasks.
/// </summary>
public class Worker : BackgroundService
{
    private readonly JokeService _jokeService;
    private readonly EmailService _emailService;
    private readonly MsTeamsService _msTeamsService;
    private readonly TimersSettings _timers;
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<Worker> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="Worker"/> class.
    /// </summary>
    /// <param name="jokeService">The joke service.</param>
    /// <param name="emailService">The email service.</param>
    /// <param name="msTeamsService">The Microsoft Teams service.</param>
    /// <param name="timers">The timers settings.</param>
    /// <param name="emailSettings">The email settings.</param>
    /// <param name="logger">The logger.</param>
    public Worker(JokeService jokeService, EmailService emailService, MsTeamsService msTeamsService, TimersSettings timers, EmailSettings emailSettings, ILogger<Worker> logger) =>
        (_jokeService, _emailService, _msTeamsService, _timers, _emailSettings, _logger) = (jokeService, emailService, msTeamsService, timers, emailSettings, logger);

    /// <summary>
    /// Executes the background task asynchronously.
    /// </summary>
    /// <param name="stoppingToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await Task.Delay(_timers.StartUpDelay * 1000, stoppingToken); // 5 Second Delay on Project StartUp.

            while (!stoppingToken.IsCancellationRequested)
            {
                // Log the current time to the console...
                var currentTime = DateTime.Now.ToString("T");
                _logger.LogInformation("Getting a random joke... {time}", currentTime);
                await Task.Delay(_timers.GetRandomJokeDelay * 1000, stoppingToken); // 3 Second Delay before getting a random joke.

                /*
                    Get a random joke from the "HashSet Record Struct" collection.
                    If the joke is null, throw an exception...
                    Log the random joke to the console.
                */
                string joke = _jokeService.GetRandomJoke() ?? throw new InvalidOperationException("The joke is null!...");
                _logger.LogInformation("{Joke}", joke);

                // Send joke via email
                _logger.LogInformation("Sending joke via email...");
                var emailSubject = _emailSettings.Subject + "\tProgramming Joke Incomming!";
                await Task.Delay(_timers.EmailServiceDelay * 1000, stoppingToken); // 5 Second Delay before sending the email.
                await _emailService.SendEmailAsync(_emailSettings.ToAddress, emailSubject, joke);

                // Send joke via Microsoft Teams
                //_logger.LogInformation("Sending joke via Microsoft Teams...");
                //await Task.Delay((int)(_timers.MsTeamsServiceDelay! * 1000), stoppingToken); // 5 Second Delay before sending the message.
                //await _msTeamsService.SendTeamsPostAsync("Async MS Teams Post Message");

                // Delay for a period of time before getting another random joke...
#if DEBUG
                _logger.LogWarning("Waiting 10 seconds before getting another random joke...");
                await Task.Delay((int)(_timers.DebugConstantDelay! * 1000), stoppingToken); // 10 Seconds (DEBUG)
#else
                    _logger.LogWarning("Waiting 1 hour before getting another random joke...");
                    await Task.Delay(TimeSpan.FromHours(1), stoppingToken); // 1 Hour (RELEASE)
#endif
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
        await base.StartAsync(cancellationToken);
        _logger.LogInformation("The Joker is here!...");
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
