using Detachable.Project.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detachable.Project.Core.Options
{
    public class CacheOptions
    {
        public CacheType CacheType { get; set; }
        public string RedisEndpoint { get; set; } // 单机
        public string ClusterRedisEndpoint { get; set; }  //集群
        public bool UpdateOrderCacheOnServerRestart { set; get; }
        public int MaxDayOfHistoryOrder { set; get; }
    }
}
