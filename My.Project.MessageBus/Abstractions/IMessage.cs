using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Project.Abstractions
{
    /// <summary>
    /// 消息
    /// </summary>
    public interface IMessage
    {

    }

    public class Message<T> : IMessage where T : class
    {
        public Message()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }
        public string EventID { set; get; }
        public Guid Id { get; }
        public DateTime CreationDate { get; }
        public T Data { set; get; }
    }
}
