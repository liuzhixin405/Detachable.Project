using SlimMessageBus.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Detachable.Project.Abstractions
{
    /// <summary>
    /// 消费者接口 消费
    /// </summary>
    public interface IMessageHandler<in T> :SlimMessageBus.IConsumer<T> where T:IMessage
    {
    }

    public abstract class AbsMessageHandler<T> : IMessageHandler<T>, IConsumerContextAware where T : IMessage
    {
        public AsyncLocal<ConsumerContext> Context => new AsyncLocal<ConsumerContext>();

        public abstract Task OnHandle(T message, string path);

    }
}
