using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DiagnosticAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detachable.Project.ExternalAssembly
{
    public class HostingTracingDiagnosticProcessor : ITracingDiagnosticProcessor
    {
        public string ListenerName { get; } = "Microsoft.AspNetCore";
        private readonly IEnumerable<IHostingDiagnosticHandler> _diagnosticHandlers;

        public HostingTracingDiagnosticProcessor(IEnumerable<IHostingDiagnosticHandler> diagnosticHandlers)
        {

            _diagnosticHandlers = diagnosticHandlers.Reverse();

        }
        [DiagnosticName("Microsoft.AspNetCore.Hosting.HttpRequestIn.Start")]
        public void BeginRequest(HttpContext HttpContext)
        {
            foreach (var handler in _diagnosticHandlers)
            {
                if (handler.OnlyMatch(HttpContext))
                {
                    handler.BeginRequest(HttpContext);
                    return;
                }
            }
        }

        /// <remarks>
        /// See remarks in <see cref="BeginRequest(HttpContext)"/>.
        /// </remarks>
        [DiagnosticName("Microsoft.AspNetCore.Hosting.HttpRequestIn.Stop")]
        public void EndRequest(HttpContext HttpContext)
        {

            foreach (var handler in _diagnosticHandlers)
            {
                if (handler.OnlyMatch(HttpContext))
                {
                    handler.EndRequest(HttpContext);
                    break;
                }
            }
        }
    }
}
