using JokerService;
using JokerService.Services;
using JokerService.Settings;
using Serilog;
using Microsoft.Extensions.Options;
using JokerService.Processes;

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

        services.Configure<FileSystemSettings>(hostContext.Configuration.GetSection("FileSystemSettings"));
        services.Configure<TimersSettings>(hostContext.Configuration.GetSection("TimersSettings"));
        services.Configure<EmailSettings>(hostContext.Configuration.GetSection("EmailSettings"));

        // Register Settings Class(s) as a singleton
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<FileSystemSettings>>().Value);
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<TimersSettings>>().Value);
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<EmailSettings>>().Value);

        services.AddSingleton<EmailService>();
        services.AddSingleton<MsTeamsService>();
        services.AddSingleton<JokeService>();
        services.AddSingleton<TextBuilderService>();

        services.AddSingleton<JokeBuilderProcess>();

        services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();

host.Run();
