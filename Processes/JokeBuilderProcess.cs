using JokerService.Services;
using JokerService.Settings;

namespace JokerService.Processes
{
    public class JokeBuilderProcess(ILogger<JokeBuilderProcess> logger, TimersSettings timers, EmailSettings emailSettings, EmailService emailService, MsTeamsService msTeamsService, TextBuilderService textBuilderService)
    {
        private readonly ILogger<JokeBuilderProcess> _logger = logger;
        private readonly TimersSettings _timers = timers;
        private readonly EmailSettings _emailSettings = emailSettings;
        private readonly EmailService _emailService = emailService;
        private readonly MsTeamsService _msTeamsService = msTeamsService;
        private readonly TextBuilderService _textBuilderService = textBuilderService;

        public async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await Task.Delay(_timers.StartUpDelay * 1000, stoppingToken); // 5 Second Delay on Project StartUp.

                while (!stoppingToken.IsCancellationRequested)
                {
                    var currentTime = DateTime.Now.ToString("T");   // Get the current time...
                    _logger.LogInformation("Getting a random joke... {time}", currentTime);
                    await Task.Delay(_timers.GetRandomJokeDelay * 1000, stoppingToken); // 3 Second Delay before getting a random joke.

                    /*
                        Get a random joke from the "HashSet Record Struct" collection.
                        If the joke is null, throw an exception...
                        Log the random joke to the console.
                    */
                    string joke = GetRandomJoke() ?? throw new InvalidOperationException("The joke is null!...");
                    _logger.LogInformation("{Joke}", joke);

                    // Send joke via email
                    _logger.LogInformation("Sending joke via email...");
                    var emailSubject = _emailSettings.Subject + "\tProgramming Joke Incomming!";
                    await Task.Delay(_timers.EmailServiceDelay * 1000, stoppingToken); // 5 Second Delay before sending the email.
                    await _emailService.SendEmailAsync(_emailSettings.ToAddress, emailSubject, joke);

                    // Send joke via Microsoft Teams
                    /*
                    _logger.LogInformation("Sending joke via Microsoft Teams...");
                    await Task.Delay((int)(_timers.MsTeamsServiceDelay! * 1000), stoppingToken); // 5 Second Delay before sending the message.
                    await _msTeamsService.SendTeamsPostAsync("Async MS Teams Post Message");
                    */

                    // Write the joke to a text file...
                    await _textBuilderService.BuildTextAsync(joke);

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the JokeBuilderProcess...");
            }
        }

        /// <summary>
        /// Represents a joke with a setup and punchline.
        /// </summary>
        readonly record struct Joke(string Setup, string Punchline);

        /// <summary>
        /// Gets a random joke from the collection.
        /// </summary>
        /// <returns>A string representing the random joke.</returns>
        public string GetRandomJoke()
        {
            Joke joke = _jokes.ElementAt(Random.Shared.Next(_jokes.Count));
            return $"{joke.Setup}{Environment.NewLine}{joke.Punchline}";
        }

        /// <summary>
        /// Programming jokes borrowed from:
        /// https://github.com/eklavyadev/karljoke/blob/main/source/jokes.json
        /// </summary>
        private readonly HashSet<Joke> _jokes =
        [
            new Joke("What's the best thing about a Boolean?", "Even if you're wrong, you're only off by a bit."),
            new Joke("What's the object-oriented way to become wealthy?", "Inheritance"),
            new Joke("Why did the programmer quit their job?", "Because they didn't get arrays."),
            new Joke("Why do programmers always mix up Halloween and Christmas?", "Because Oct 31 == Dec 25"),
            new Joke("How many programmers does it take to change a lightbulb?", "None that's a hardware problem"),
            new Joke("If you put a million monkeys at a million keyboards, one of them will eventually write a Java program", "the rest of them will write Perl"),
            new Joke("['hip', 'hip']", "(hip hip array)"),
            new Joke("To understand what recursion is...", "You must first understand what recursion is"),
            new Joke("There are 10 types of people in this world...", "Those who understand binary and those who don't"),
            new Joke("Which song would an exception sing?", "Can't catch me - Avicii"),
            new Joke("Why do Java programmers wear glasses?", "Because they don't C#"),
            new Joke("How do you check if a webpage is HTML5?", "Try it out on Internet Explorer"),
            new Joke("A user interface is like a joke.", "If you have to explain it then it is not that good."),
            new Joke("I was gonna tell you a joke about UDP...", "...but you might not get it."),
            new Joke("The punchline often arrives before the set-up.", "Do you know the problem with UDP jokes?"),
            new Joke("Why do C# and Java developers keep breaking their keyboards?", "Because they use a strongly typed language."),
            new Joke("Knock-knock.", "A race condition. Who is there?"),
            new Joke("What's the best part about TCP jokes?", "I get to keep telling them until you get them."),
            new Joke("A programmer puts two glasses on their bedside table before going to sleep.", "A full one, in case they gets thirsty, and an empty one, in case they don’t."),
            new Joke("There are 10 kinds of people in this world.", "Those who understand binary, those who don't, and those who weren't expecting a base 3 joke."),
            new Joke("What did the router say to the doctor?", "It hurts when IP."),
            new Joke("An IPv6 packet is walking out of the house.", "He goes nowhere."),
            new Joke("3 SQL statements walk into a NoSQL bar. Soon, they walk out", "They couldn't find a table.")
        ];
    }
}
