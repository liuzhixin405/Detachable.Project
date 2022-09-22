using Detachable.Project.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Detachable.Project.MessageBus
{
    public class MessageBusTestHandler : AbsMessageHandler<Message<string>>
    {
        BufferBlock<string> _bufferBlock;
        ActionBlock<string> _actionBlock;
        public MessageBusTestHandler()
        {
            _actionBlock = new ActionBlock<string>(e => PushData(e));
            _bufferBlock = new BufferBlock<string>() { };
            _bufferBlock.LinkTo(_actionBlock);
        }
        public async override Task OnHandle(Message<string> message, string path)
        {
                _bufferBlock.Post(message.Data);
                Console.WriteLine($"Message Handler TopicName:{path}, Time:{message.CreationDate}, Data:{message.Data}");
                await Task.CompletedTask;
        }
        public async Task PushData(string data)
        {
            //处理逻辑

        }
        /*
        Message Handler TopicName:TopicTestName, Time:2021/7/9 14:17:59, Data:2021/7/9 14:17:58 Producer Test publish.
        Message Handler TopicName:TopicTestName, Time:2021/7/9 14:17:59, Data:2021/7/9 14:17:59 Producer Test publish.
        Message Handler TopicName:TopicTestName, Time:2021/7/9 14:17:59, Data:2021/7/9 14:17:59 Producer Test publish.
        Message Handler TopicName:TopicTestName, Time:2021/7/9 14:17:59, Data:2021/7/9 14:17:59 Producer Test publish.
        Message Handler TopicName:TopicTestName, Time:2021/7/9 14:17:59, Data:2021/7/9 14:17:59 Producer Test publish.
        Message Handler TopicName:TopicTestName, Time:2021/7/9 14:17:59, Data:2021/7/9 14:17:59 Producer Test publish.
        Message Handler TopicName:TopicTestName, Time:2021/7/9 14:17:59, Data:2021/7/9 14:17:59 Producer Test publish.
        Message Handler TopicName:TopicTestName, Time:2021/7/9 14:17:59, Data:2021/7/9 14:17:59 Producer Test publish.
        Message Handler TopicName:TopicTestName, Time:2021/7/9 14:17:59, Data:2021/7/9 14:17:59 Producer Test publish.
        Message Handler TopicName:TopicTestName, Time:2021/7/9 14:18:00, Data:2021/7/9 14:17:59 Producer Test publish.
         */
    }
}
