namespace Quartz.Cronjob.Jobs
{
    public class Cronjob2 : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            // Your job logic here
            Console.WriteLine("[Cronjob2] Your cron job successfully executed!");
        }
    }
}
