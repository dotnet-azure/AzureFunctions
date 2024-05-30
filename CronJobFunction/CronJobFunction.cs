using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CronJobFunction;

public class CronJobFunction
{
    private readonly ILogger _logger;

    public CronJobFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<CronJobFunction>();
    }

    [Function("CronJobFunction")]
    public void Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        
        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
        }
    }
}
