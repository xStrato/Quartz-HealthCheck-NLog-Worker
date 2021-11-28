
namespace Quartz_HealthCheck_NLog_Worker.Common;
public abstract class HealthCheckBaseEvent : IRequest<bool>
{
    public bool IsHealthy { get; set; }
    public string Description { get; set; }
    public HealthCheckBaseEvent() { }
    public HealthCheckBaseEvent(bool isHealthy, string description)
    {
        IsHealthy = isHealthy;
        Description = description;
    }
}