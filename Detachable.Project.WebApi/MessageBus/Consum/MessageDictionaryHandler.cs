using Detachable.Project.Abstractions;
using Detachable.Project.Entity.EventModel;
using System;
using System.Threading.Tasks;

namespace Detachable.Project.WebApi.MessageBus.Consum
{
    public class MessageDictionaryHandler : AbsMessageHandler<DictionaryMessageEvent>
    {
        public override Task OnHandle(DictionaryMessageEvent message, string path)
        {
            var dicEnumerator = message.Data.GetEnumerator();
            while (dicEnumerator.MoveNext())
            {
                var current = dicEnumerator.Current;
                Console.WriteLine($"key ={current.Key} , value ={current.Value}");
            }

            return Task.CompletedTask;
        }
    }
}
