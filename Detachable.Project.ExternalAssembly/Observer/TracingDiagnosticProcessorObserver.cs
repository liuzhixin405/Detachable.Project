using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detachable.Project.ExternalAssembly
{
    public class TracingDiagnosticProcessorObserver : IObserver<DiagnosticListener>
    {
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IEnumerable<ITracingDiagnosticProcessor> _tracingDiagnosticProcessors;

        public TracingDiagnosticProcessorObserver(IEnumerable<ITracingDiagnosticProcessor> tracingDiagnosticProcessors,
            ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(typeof(TracingDiagnosticProcessorObserver));
            _loggerFactory = loggerFactory;
            _tracingDiagnosticProcessors = tracingDiagnosticProcessors ??
                                           throw new ArgumentNullException(nameof(tracingDiagnosticProcessors));
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(DiagnosticListener listener)
        {
            foreach (var diagnosticProcessor in _tracingDiagnosticProcessors.Distinct(x => x.ListenerName))
            {
                if (listener.Name == diagnosticProcessor.ListenerName)
                {
                    Subscribe(listener, diagnosticProcessor);
                    _logger.LogInformation(
                        $"Loaded diagnostic listener [{diagnosticProcessor.ListenerName}].");
                }
            }
        }

        protected virtual void Subscribe(DiagnosticListener listener,
            ITracingDiagnosticProcessor tracingDiagnosticProcessor)
        {
            var diagnosticProcessor = new TracingDiagnosticObserver(tracingDiagnosticProcessor, _loggerFactory);
            listener.Subscribe(diagnosticProcessor, diagnosticProcessor.IsEnabled);
        }
    }
}
