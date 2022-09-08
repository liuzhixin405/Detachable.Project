using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Project.Core.Abstraction
{
    public abstract class BaseAOPAttribute:Attribute
    {
        public virtual Task Before(IAOPContext context)  
        {
            return Task.CompletedTask; //重写实现aop注入,例如 事务，日志，统计接口调用事件等等 ，都可以通过这种特性方式注入
            //结束事务
        }
        public virtual Task After(IAOPContext context)
        {
            return Task.CompletedTask;//开启事务
        }
    }
}
