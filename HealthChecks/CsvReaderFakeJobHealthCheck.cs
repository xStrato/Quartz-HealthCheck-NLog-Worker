namespace QuartzPlusWorker.HealthChecks;

public class CsvReaderFakeJobHealthCheck : IHealthCheck, IRequestHandler<CsvReaderFakeJobHealthEvent, bool>
{
    public static CsvReaderFakeJobHealthEvent HealthyEvent { get; private set; }
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        if (HealthyEvent is not null and { IsHealthy: true })
            return Task.FromResult(HealthCheckResult.Healthy(HealthyEvent.Description));
        return Task.FromResult(HealthCheckResult.Unhealthy(HealthyEvent.Description));
    }

    public Task<bool> Handle(CsvReaderFakeJobHealthEvent message, CancellationToken cancellationToken)
    {
        HealthyEvent = message;
        return Task.FromResult(true);
    }
}