using Detachable.Project.ExternalAssembly.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detachable.Project.ExternalAssembly
{
    internal class LogHostService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        public LogHostService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            DiagnosticListener.AllListeners.Subscribe(new DiagnosticObserver(serviceProvider.GetRequiredService<ILoggerFactory>(), 500));

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
