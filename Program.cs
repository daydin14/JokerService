using JokerService;
using Serilog;

var myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    // .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Minute)
    .WriteTo.File(Path.Combine(myDocuments, "Logs", "jokerservicelog.txt"))
    .CreateLogger();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();

host.Run();
