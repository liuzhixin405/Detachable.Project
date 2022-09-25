using Detachable.Project.ExternalAssembly.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Detachable.Project.ExternalAssembly.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAPM(this IServiceCollection services,Action<ApmExtensions> action=null)
        {
            action?.Invoke(services.AddApmExtensions());
            return services;
        }

        public static ApmExtensions AddHosting(this ApmExtensions extensions)
        {
            extensions.Services.AddSingleton<ITracingDiagnosticProcessor, HostingTracingDiagnosticProcessor>();
            extensions.Services.AddSingleton<IHostingDiagnosticHandler, DefaultHostingDiagnosticHandler>();
            extensions.Services.AddSingleton<TracingDiagnosticProcessorObserver>();
            return extensions;
        }

        public static ApmExtensions AddApmExtensions(this IServiceCollection services)
        {
            return new ApmExtensions(services);
        }
    }
}
