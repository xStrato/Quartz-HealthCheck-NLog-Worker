namespace QuartzPlusWorker.Jobs;

[DisallowConcurrentExecution]
public class CsvReaderFakeJob : JobBase
{
    public CsvReaderFakeJob(ILogger<CsvReaderFakeJob> logger, IMediator mediator) : base(logger, mediator) { }

    public override async Task Execute(IJobExecutionContext context)
    {
        // This task simulates the file reading with 50% chance of failing 
        try
        {
            LogInformation($"[Instance {context.JobDetail.Key.Name[^1]}] - Starting file reading: BASE_CONSOLIDATED_2022_12_32.csv\n");
            await Task.Delay(new Random().Next(1000, 10000));

            if (new Random().Next(0, 10) % 2 == 0)
                throw new Exception($"[Instance {context.JobDetail.Key.Name[^1]}] - could not read the file.\n");

            LogInformation($"[Instance {context.JobDetail.Key.Name[^1]}] - Job completed successfully!\n");
        }
        catch (Exception ex)
        {
            LogError(ex);
        }
        await Task.Delay(new Random().Next(1000, 10000));
    }
}