using Microsoft.Extensions.Hosting;
using My.Project.Core;
using My.Project.Utility.Helper;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace My.Project.WebApi.Consistency
{
    public class BackServices : BackgroundService
    {
        private static ConcurrentQueue<Type> taskQueue = new ConcurrentQueue<Type>();

        private readonly IServiceProvider _serviceProvider;
        public BackServices(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            var types = GlobalMetaData.AllTypes.Where(x => typeof(IJobTask).IsAssignableFrom(x) && !x.IsInterface);

            foreach (var type in types)
            {
                taskQueue.Enqueue(type);
            }
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested)
            {
                return Task.CompletedTask;
            }
            while (taskQueue.TryDequeue(out var task))
            {
                var val = ((IJobTask)Activator.CreateInstance(task, _serviceProvider ?? throw new Exception("serviceprovider is null")))?? throw new Exception($"未能获取正确类型{task?.FullName??"itest"}");
                JobHelper.SetCronJob(async () => {
                    await val.Invoke();
                }, val.Cron??throw new Exception("cron is null"));
            }
            return Task.CompletedTask;
        }
    }
}
