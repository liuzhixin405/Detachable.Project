using My.Project.Core.Attributes;
using My.Project.Core.IService;
using My.Project.IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Project.Business
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
