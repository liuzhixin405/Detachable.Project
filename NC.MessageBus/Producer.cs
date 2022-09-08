using My.Project.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Project
{
    public interface IProducer
    {
        Type MessageType { get; }
        string TopicName { get; }
    }
    public class Producer<T> : IProducer where T : IMessage
    {
        public Type MessageType => typeof(T);
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
        public Producer(string topicName ="")
        {
            _topicName = topicName;
        }
    }
}
