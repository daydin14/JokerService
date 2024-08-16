using JokerService;
using JokerService.Services;
using JokerService.Settings;
using Serilog;
using Microsoft.Extensions.Options;

var serilogSettings = new SerilogSettings();
serilogSettings.ConfigureSerilog();

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddWindowsService(options =>
        {
            options.ServiceName = "Jokers Manor";
        });

        services.Configure<EmailSettings>(hostContext.Configuration.GetSection("SmtpSettings"));
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
