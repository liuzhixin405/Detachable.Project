using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using My.Project.Entity.Options;
using CSRedis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Colder.DistributedLock.Hosting;

namespace My.Project.Core.Extensions
{
    public static partial class HostExtensions
    {
        public static IHostBuilder UseCache(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                var cacheOption = context.Configuration.GetSection("Cache").Get<CacheOptions>();
                switch (cacheOption.CacheType)
                {
                    case Entity.Enum.CacheType.Memory:
                        services.AddMemoryCache();
                        break;
                    case Entity.Enum.CacheType.Redis:
                        /*
                         var csredis = new CSRedis.CSRedisClient("mymaster,password=123,prefix=my_", 
                            new [] { "192.169.1.10:26379", "192.169.1.11:26379", "192.169.1.12:26379" });//哨兵
                         */
                        var csredis = new CSRedisClient(cacheOption.ClusterRedisEndpoint);
                        RedisHelper.Initialization(csredis);
                        services.AddSingleton<IDistributedCache>(new CSRedisCache(RedisHelper.Instance));
                        break;
                    default: throw new ArgumentNullException("缓存类型错误");
                }
                services.AddMemoryCache();
            });
            return hostBuilder;
        }

        /// <summary>
        /// 使用分布式锁
        /// </summary>
        /// <param name="hostBuilder">建造者</param>
        /// <returns></returns>
        public static IHostBuilder UseDistributedLock(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureDistributedLockDefaults();
        }
    }
}
