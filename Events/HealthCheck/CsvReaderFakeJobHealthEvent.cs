namespace Quartz_HealthCheck_NLog_Worker.Events.HealthCheck;
public class CsvReaderFakeJobHealthEvent : HealthCheckBaseEvent
{
    public CsvReaderFakeJobHealthEvent(bool isHealthy, string description) : base(isHealthy, description) { }
}