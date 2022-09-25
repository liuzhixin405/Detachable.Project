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
        private readonly TracingDiagnosticProcessorObserver _tracingDiagnosticProcessorObserver;
        private readonly ILogger _logger;
        public LogHostService(IServiceProvider serviceProvider, TracingDiagnosticProcessorObserver observer, ILoggerFactory loggerFactory)
        {
            _serviceProvider = serviceProvider;
            _tracingDiagnosticProcessorObserver = observer;
            _logger = loggerFactory.CreateLogger(typeof(LogHostService));
            DiagnosticListener.AllListeners.Subscribe(_tracingDiagnosticProcessorObserver);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
