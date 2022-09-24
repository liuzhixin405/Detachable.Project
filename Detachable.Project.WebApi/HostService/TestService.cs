 using Detachable.Project.Core.IService;
using Detachable.Project.WebApi.Consistency;
using System;
using System.Threading.Tasks;

namespace Detachable.Project.WebApi.HostService
{
    public class TestService : IJobTask, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;
        public TestService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public string Cron => "0/15 * * * * ? ";

        public Task Invoke()
        {
            Console.WriteLine($"执行中...{DateTimeOffset.UtcNow}");
            return Task.CompletedTask;
        }
    }
}
