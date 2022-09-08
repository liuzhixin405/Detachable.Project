using My.Project.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Project.Core.Attributes
{
    public class LogAttribute:BaseAOPAttribute
    {
        public LogAttribute(string name,string type)
        {
            this.name = name;
            this.type = type;
        }
        private string name;
        protected string type;
        private DateTimeOffset date;
        
        public override Task After(IAOPContext context)
        {
           
            Console.WriteLine($"after invocation,name={name}, type={type} ,调用时间 {DateTimeOffset.UtcNow.Subtract(date).Milliseconds}ms");
            return Task.CompletedTask;
        }

        public override Task Before(IAOPContext context)
        {
            date = DateTimeOffset.UtcNow;
            Console.WriteLine($"before invocation");
            return Task.CompletedTask;
        }
    }
}
