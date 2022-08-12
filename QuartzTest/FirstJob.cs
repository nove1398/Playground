using Quartz;

namespace QuartzTest
{
    public class FirstJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync($"{DateTime.UtcNow}");
        }
    }
}
