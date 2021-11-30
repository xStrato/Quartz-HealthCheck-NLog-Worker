IConfiguration Configuration = default;
IHostEnvironment HostingEnvironment = default;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        HostingEnvironment = context.HostingEnvironment;
        Configuration = new ConfigurationBuilder()
            .SetBasePath(context.HostingEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json")
            .AddEnvironmentVariables()
            .Build();

        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
            q.AddJobAndTrigger<CsvReaderFakeJob>(Configuration);
        })
        .AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        services.AddLogging(logger =>
        {
            logger.ClearProviders();
            logger.SetMinimumLevel(LogLevel.Information);
            logger.AddNLog("NLog.config");
        });
    })
    .UseWindowsService()
    .Build();

var healthApp = host.BuildHealthCheckApp(Configuration, HostingEnvironment);
Task.WaitAll(host.RunAsync(), healthApp?.RunAsync() ?? Task.CompletedTask);
