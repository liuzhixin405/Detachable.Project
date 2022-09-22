using Detachable.Project.Core.Attributes;
using Detachable.Project.Core.IService;
using Detachable.Project.IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detachable.Project.Business
{
    
    public class Test : ITest, ITransientDependency
    {
        [Log("测试","type_1")]
        public string GetValue()
        {
            return "Success";
        }
    }
}
