namespace Quartz_HealthCheck_NLog_Worker.Common;
public abstract class JobBase : IJob
{
    protected ILogger<IJob> _logger { get; init; }
    protected IMediator _mediator { get; init; }
    private Type _healthEventType { get; init; }

    protected JobBase(ILogger<IJob> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;

        var @namespace = $"{GetType().Namespace.Split('.')[0]}.Events.HealthCheck";
        _healthEventType = Type.GetType($"{@namespace}.{GetType().Name}HealthEvent");
    }

    protected virtual void LogInformation(string message)
    {
        _logger?.LogInformation(message);
        _mediator?.Send(Activator.CreateInstance(_healthEventType, new object[] { true, message })).GetAwaiter().GetResult();
    }

    protected virtual void LogError(Exception ex)
    {
        _logger?.LogError(ex, ex.Message);
        _mediator?.Send(Activator.CreateInstance(_healthEventType, new object[] { false, ex.Message })).GetAwaiter().GetResult();
    }

    public abstract Task Execute(IJobExecutionContext context);
}