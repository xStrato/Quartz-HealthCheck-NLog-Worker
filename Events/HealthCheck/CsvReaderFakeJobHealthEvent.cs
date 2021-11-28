namespace QuartzPlusWorker.Events.HealthCheck;
public class CsvReaderFakeJobHealthEvent : HealthCheckBaseEvent
{
    public CsvReaderFakeJobHealthEvent(bool isHealthy, string description) : base(isHealthy, description) { }
}