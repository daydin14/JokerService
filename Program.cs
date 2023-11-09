using JokerService;
using Serilog;

//var myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
var myDocuments = Environment.CurrentDirectory;
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    // .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Minute)
    .WriteTo.File(Path.Combine(myDocuments, "Logs", "jokerservicelog.txt"))
    .CreateLogger();

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices(services =>
    {
        services.AddWindowsService(options =>
        {
            options.ServiceName = ".NET Joke Service";
        });
        services.AddSingleton<JokeService>();
        services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();

host.Run();
