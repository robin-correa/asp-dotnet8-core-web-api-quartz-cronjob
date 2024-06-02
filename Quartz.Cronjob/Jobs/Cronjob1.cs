namespace Quartz.Cronjob.Jobs
{
    public class Cronjob1 : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            // Your job logic here
            Console.WriteLine("[Cronjob1] Your cron job successfully executed! - Every 1 minute");
        }
    }
}
