namespace QuartzPlusWorker.Extensions;
public static class HealthCheckExtensions
{
    public static WebApplication BuildHealthCheckApp(this IHost host,
                        IConfiguration config,
                        IHostEnvironment HostingEnvironment)
    {
        if (bool.Parse(config["HealthCheckApp:Enable"]) is false)
            return null;

        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseConfiguration(config);
        builder.WebHost.UseUrls(config["HealthCheckApp:AccessUrls"]);
        builder.WebHost.UseKestrel();

        builder.Services
                .AddHealthChecks()
                .AddCheck<CsvReaderFakeJobHealthCheck>(nameof(CsvReaderFakeJob), tags: new[] { config[$"Quartz:{nameof(CsvReaderFakeJob)}:Group"] });

        builder.Services.AddHealthChecksUI(o =>
        {
            var appName = Assembly.GetExecutingAssembly().GetName().Name;
            o.SetHeaderText($"{appName} - {HostingEnvironment.EnvironmentName}");
            o.AddHealthCheckEndpoint(appName, "/hc");
        })
        .AddInMemoryStorage();

        var app = builder.Build();
        app.MapHealthChecksUI(o => o.UIPath = "/");
        app.MapHealthChecks("/hc", new()
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        return app;
    }
}