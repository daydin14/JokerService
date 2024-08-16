using JokerService;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(Path.Combine(Environment.CurrentDirectory, "Logs", "log-.txt"), rollingInterval: RollingInterval.Day)
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
        services.AddSingleton<EmailService>();
        services.AddSingleton<JokeService>();
        services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();

host.Run();
