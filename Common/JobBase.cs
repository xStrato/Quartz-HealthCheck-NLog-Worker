namespace Quartz_HealthCheck_NLog_Worker.Common;
public abstract class JobBase : IJob
{
    protected ILogger<IJob> _logger { get; init; }
    protected IMediator _mediator { get; init; }
    private Type _eventType { get; init; }

    protected JobBase(ILogger<IJob> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;

        var @namespace = $"{GetType().Namespace.Split('.')[0]}.Events.HealthCheck";
        _eventType = Type.GetType($"{@namespace}.{GetType().Name}HealthEvent");
    }

    protected void LogInformation(string message)
    {
        _logger?.LogInformation(message);
        _mediator?.Send(Activator.CreateInstance(_eventType, new object[] { true, message })).GetAwaiter().GetResult();
    }

    protected void LogError(Exception ex)
    {
        _logger?.LogError(ex, ex.Message);
        _mediator?.Send(Activator.CreateInstance(_eventType, new object[] { false, ex.Message })).GetAwaiter().GetResult();
    }

    public abstract Task Execute(IJobExecutionContext context);
}