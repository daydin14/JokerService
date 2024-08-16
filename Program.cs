using JokerService;
using JokerService.Services;
using JokerService.Settings;
using Serilog;
using Microsoft.Extensions.Options;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddWindowsService(options =>
        {
            options.ServiceName = ".NET Joke Service";
        });
        services.Configure<SmtpSettings>(hostContext.Configuration.GetSection("SmtpSettings"));
        services.Configure<TimersSettings>(hostContext.Configuration.GetSection("TimersSettings"));

        // Register TimersSettings as a singleton
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<TimersSettings>>().Value);

        services.AddSingleton<EmailService>();
        services.AddSingleton<MsTeamsService>();
        services.AddSingleton<JokeService>();
        services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();

host.Run();
