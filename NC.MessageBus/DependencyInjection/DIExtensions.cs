using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using My.Project.Hosting;
using My.Project.Options;
using My.Project.Utility;
using SlimMessageBus.Host.Config;
using SlimMessageBus.Host.Kafka;
using SlimMessageBus.Host.Kafka.Configs;
using SlimMessageBus.Host.MsDependencyInjection;
using SlimMessageBus.Host.Serialization.Json;
using System;
using System.Collections.Generic;

namespace My.Project.DependencyInjection
{
    /// <summary>
    /// HostBuilder 扩展
    /// </summary>
    public static class DIExtensions
    {
        public static IHostBuilder UseMessageBus(this IHostBuilder hostBuilder,Func<IEnumerable<IProducer>> addProducer=null,Func<IEnumerable<IConsumer>> addConsumer=null)
        {
            hostBuilder.ConfigureServices((host, services) =>
            {
                var configuration = host.Configuration;
                var options = configuration.GetSection("messagebus").Get<MessageBusOptions>();
                if (options == null)
                {
                    throw new Exception("消息总线初始化异常：缺少配置信息");
                }
                foreach (var consumer in addConsumer?.Invoke())
                {
                    services.AddTransient(consumer.HandlerType);
                }
                services.AddHostedService<MessageBusBootstraper>();

                MessageBusBootstraper.Bootstarp += serviceProvier => SlimMessageBus.MessageBus.SetProvider(() => serviceProvier.GetService<SlimMessageBus.IMessageBus>());
                services.AddSingleton(svp => BuildMessageBus(svp,options,addProducer?.Invoke(),addConsumer?.Invoke()));
                services.AddSingleton<My.Project.Abstractions.IMessageBus>(svp => new My.Project.Abstractions.MessageBus());
            });
            return hostBuilder;
        }

         static SlimMessageBus.IMessageBus BuildMessageBus(IServiceProvider serviceProvider,MessageBusOptions options,IEnumerable<IProducer> producers,IEnumerable<IConsumer> consumers)
        {
            void AddSsl(ClientConfig c)
            {
                //c.SecurityProtocol = SecurityProtocol.SaslSsl;
                //c.SaslUsername = options.Username;
                //c.SaslPassword = options.Password;
                //c.SaslMechanism = SaslMechanism.ScramSha256;
                //c.SslCaLocation = "cloudkarafka_2020-12.ca";
            }

            var mbb = MessageBusBuilder.Create()
                .Do(builder =>
                {
                    producers?.ForEach(f => builder.Produce(f.MessageType, x => x.DefaultTopic(f.TopicName)));
                    consumers?.ForEach(f => builder.Consume(f.MessageType, x => x.Topic(f.TopicName).WithConsumer(f.HandlerType).Group(f.Group)));
                    builder.WithSerializer(new JsonMessageSerializer());
                    builder.WithDependencyResolver(new MsDependencyInjectionDependencyResolver(serviceProvider));
                    builder.WithProviderKafka(new KafkaMessageBusSettings(options.Brokers)
                    {
                        ProducerConfig = (config) =>
                        {
                            config.Acks = Acks.All; // All为默认所有消费分区都接收才算消费成功 ,None 只管发布不管消费成功与否,Leader 有一个分区接收到算成功
                            AddSsl(config);
                        },
                        ConsumerConfig = (config) =>
                        {
                            /*
                             * earliest
                                当各分区下有已提交的offset时，从提交的offset开始消费；无提交的offset时，从头开始消费
                                latest
                                当各分区下有已提交的offset时，从提交的offset开始消费；无提交的offset时，消费新产生的该分区下的数据
                                error
                                topic各分区都存在已提交的offset时，从offset后开始消费；只要有一个分区不存在已提交的offset，则抛出异常

                             */
                            //config.AutoOffsetReset = AutoOffsetReset.Latest; //只消费从consumer创建后生产的数据，之前产生的数据不消费。
                            config.AutoOffsetReset = AutoOffsetReset.Earliest; // 建议 ，客户端重启会消费未消费的数据
                            AddSsl(config);
                        }
                    });
                }).Build();

            return mbb;

        }
    }
}
