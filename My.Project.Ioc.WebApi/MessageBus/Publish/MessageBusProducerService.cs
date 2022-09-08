using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using My.Project.Abstractions;

namespace My.Project.Publish
{
    public class MessageBusProducerService : BackgroundService
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public static Action<IServiceProvider> Bootstrap;

        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider"></param>
        public MessageBusProducerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            int i = 10;
            while(i > 0)
            {
                var messageBus = _serviceProvider.GetService<IMessageBus>();
                await messageBus.Publish(new Message<string>() { Data = $"{DateTime.Now} Producer Test publish." });
                i--;
            };
        }
    }
}
