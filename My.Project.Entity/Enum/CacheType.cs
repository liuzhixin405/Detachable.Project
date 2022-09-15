using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Project.Entity.Enum
{
    /// <summary>
    /// 缓存类型
    /// </summary>
    public enum CacheType
    {
        /// <summary>
        /// 使用内存缓存(分布式不能用)
        /// </summary>
        Memory,

        /// <summary>
        /// 使用Redis缓存(支持分布式)
        /// </summary>
        Redis
    }
}
