using Detachable.Project.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detachable.Project
{
    public interface IConsumer
    {
        Type MessageType { get; }
        Type HandlerType { get; }
        string TopicName { get; }
        string Group { get; }
    }
    public class Consumer<TMessage, THandler> : IConsumer where TMessage : IMessage
    {
        public Type MessageType => typeof(TMessage);

        public Type HandlerType => typeof(THandler);
        string _group;
        string _topicName;
        public string TopicName
        {
            set { _topicName = value; }
            get
            {
                if (string.IsNullOrEmpty(this._topicName))
                {
                    return this.MessageType.Name;
                }
                return this._topicName;
            }
        }
        public string Group
        {
            set { _group = value; }
            get
            {
                if (string.IsNullOrEmpty(this._group))
                {
                    return Guid.NewGuid().ToString();
                }
                return this._group;
            }
        }
        public Consumer(string topicName="",string group = "")
        {
            _topicName = topicName;
            _group = group;
        }
    }
}
