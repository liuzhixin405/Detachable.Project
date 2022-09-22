using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Detachable.Project.Hosting
{
    /// <summary>
    /// 消息总线初始化
    /// </summary>
    public class MessageBusBootstraper : BackgroundService
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public static Action<IServiceProvider> Bootstarp;
        readonly IServiceProvider _serviceProvider;
        public MessageBusBootstraper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Bootstarp?.Invoke(_serviceProvider);
            return Task.CompletedTask;
        }
    }
}
