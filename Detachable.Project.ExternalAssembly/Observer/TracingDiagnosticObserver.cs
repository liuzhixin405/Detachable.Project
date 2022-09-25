using Detachable.Project.ExternalAssembly.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detachable.Project.ExternalAssembly
{
    internal class TracingDiagnosticObserver : IObserver<KeyValuePair<string, object>>
    {
        private readonly Dictionary<string, TracingDiagnosticMethod> _methodCollection;
        private readonly ILogger _logger;

        public TracingDiagnosticObserver(ITracingDiagnosticProcessor tracingDiagnosticProcessor,
            ILoggerFactory loggerFactory)
        {

            _methodCollection = new TracingDiagnosticMethodCollection(tracingDiagnosticProcessor)
               .ToDictionary(method => method.DiagnosticName);
            _logger = loggerFactory.CreateLogger(typeof(TracingDiagnosticObserver));
        }


        public bool IsEnabled(string diagnosticName)
        {
            return _methodCollection.ContainsKey(diagnosticName);
        }
        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(KeyValuePair<string, object> value)
        {
            if (!_methodCollection.TryGetValue(value.Key, out var method))
                return;

            try
            {
                method.Invoke(value.Key, value.Value);
            }
            catch (Exception exception)
            {
                _logger.LogError("Invoke diagnostic method exception.", exception);
            }
        }
    }
}
