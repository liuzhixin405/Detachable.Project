using Detachable.Project.ExternalAssembly.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

[assembly: HostingStartup(typeof(Detachable.Project.ExternalAssembly.ExternalHostStartup))]
namespace Detachable.Project.ExternalAssembly
{
    /// <summary>
    /// 预留外部接口
    /// </summary>
    public class ExternalHostStartup:IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            Console.WriteLine("External服务处理中...");
            builder.ConfigureServices((context, service) =>
            {
                service.AddHostedService<LogHostService>();
            });
        }
    }
}