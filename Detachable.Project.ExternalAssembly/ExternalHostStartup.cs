using Microsoft.AspNetCore.Hosting;

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
        }
    }
}