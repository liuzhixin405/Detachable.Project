using Detachable.Project.Abstractions;
using Detachable.Project.Core.Extensions;
using Detachable.Project.DependencyInjection;
using Detachable.Project.Entity.EventModel;
using Detachable.Project.MessageBus;
using Detachable.Project.Publish;
using Detachable.Project.Utility;
using Detachable.Project.WebApi.Consistency;
using Detachable.Project.WebApi.MessageBus.Consum;
using System.Runtime.CompilerServices;

namespace Detachable.Project.WebApi.Extensions
{
    public static class Extension
    {
        public static WebApplicationBuilder AddCache(this WebApplicationBuilder builder)
        {
            builder.Host.UseDistributedLock();
            builder.Host.UseCache();
            return builder;
        }
        public static WebApplicationBuilder SetMessageBus(this WebApplicationBuilder builder)
        {
            builder.Host.UseMessageBus(
            () => new List<IProducer>() { new Producer<Message<string>>(GlobalConstant.TopicTestName), new Producer<DictionaryMessageEvent>(GlobalConstant.DictionaryMessageEvent) },
            () => new List<IConsumer>() { new Consumer<Message<string>, MessageBusTestHandler>(GlobalConstant.TopicTestName) ,
            new Consumer<DictionaryMessageEvent,MessageDictionaryHandler>(GlobalConstant.DictionaryMessageEvent)});
            builder.Services.AddHostedService<MessageBusProducerService>();
            return builder;
        }

        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddFxServices()
                .AddAutoMapper()
            .AddHostedService<BackServices>();
            return services;
        }
    }
}
