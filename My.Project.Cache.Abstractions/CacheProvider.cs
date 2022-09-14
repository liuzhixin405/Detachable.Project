using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Project.Cache.Abstractions
{
    /// <summary>
    /// 缓存提供器
    /// </summary>
    public enum CacheProvider
    {
        /// <summary>
        /// 内存缓存
        /// </summary>
        [Description("内存缓存")]
        MemoryCache,
        /// <summary>
        /// Redis
        /// </summary>
        [Description("Redis")]
        Redis
    }
}
