using My.Project.Core.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Project.Business
{
    public class TestNoInterface : ITransientDependency
    {

        public string Get()
        {
            return "yeah!";
        }
    }
}
