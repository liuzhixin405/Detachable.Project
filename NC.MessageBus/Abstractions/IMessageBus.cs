using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Project.Abstractions
{
    /// <summary>
    /// 消息总线接口 发布
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TMessage">消息类型</typeparam>
        /// <param name="message">消息</param>
        /// <param name="topicName">指定消费主题</param>
        /// <returns></returns>
        Task Publish<TMessage>(TMessage message, string topicName = null) where TMessage : class, IMessage;

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <typeparam name="TRequest">请求数据类型</typeparam>
        /// <typeparam name="TResponse">返回数据类型</typeparam>
        /// <param name="message">消息</param>
        /// <param name="topicName">指定消费节点</param>
        /// <returns></returns>
        Task<TResponse> Request<TRequest, TResponse>(TRequest message, string topicName)
           where TRequest : class, IMessage
           where TResponse : class;
    }

    public class MessageBus : IMessageBus
    {
        async Task IMessageBus.Publish<TMessage>(TMessage message, string topicName)
        {
            if (string.IsNullOrEmpty(topicName))
                await SlimMessageBus.MessageBus.Current.Publish(message);
            else
                await SlimMessageBus.MessageBus.Current.Publish(message, topicName);
        }

        async Task<TResponse> IMessageBus.Request<TRequest, TResponse>(TRequest message, string topicName)
        {
            return await SlimMessageBus.MessageBus.Current.Send<TResponse, TRequest>(message, topicName);
        }
    }
}
